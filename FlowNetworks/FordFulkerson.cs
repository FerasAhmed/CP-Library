using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.GraphAlgorithms;

namespace AlgorithmsLibrary.FlowNetworks
{
    // This code was written with help from: https://algs4.cs.princeton.edu/64maxflow/FordFulkerson.java.html
    class FordFulkerson
    {
        int edgeCount = 0;
        int vertexCount = 0;
        double flowValue = 0;
        List<FlowEdge> [] flowGraph;
        FlowEdge[] edgeTo;

        FordFulkerson(List<Edge>[] adjList)
        {
            // build Flow Network graph from adjacency list graph
            BuildFlowGraph(adjList);
        }

        private void BuildFlowGraph(List<Edge>[] adjList)
        {
            // initiate flow graph size
            flowGraph = new List<FlowEdge>[adjList.Length];

            for (int i = 0; i < adjList.Length; ++i)
            {
               List<FlowEdge> toList = new List<FlowEdge>();
               foreach (Edge ed in adjList[i])
               {
                    edgeCount++; // increment current edge count
                    int from = ed.a;
                    int to = ed.b;
                    flowGraph[from].Add(new FlowEdge(ed));
                    flowGraph[to].Add(new FlowEdge(ed));
               }
            }
        }
        public double Run(int source, int sink)
        {
            if (source == sink)
                throw new Exception($"Invalid source={source} and sink={sink} values.");

            flowValue = excess(source);

            while (hasAugmentingPath(source, sink))
            {
                double minFlow = double.MaxValue;
                for(int from = sink; from != source; from = edgeTo[from].other(from))
                {
                    minFlow = Math.Min(minFlow, edgeTo[from].residualCapacityTo(from));
                }

                // since we are setting edgeTo by reference it will automatically update the original graph flow/capacity
                for (int from = sink; from != source; from = edgeTo[from].other(from))
                {
                    edgeTo[from].addResidualFlowTo(from, minFlow);
                }

                flowValue += minFlow;
            }

            return flowValue;
        }

        private double excess(int v)
        {
            double excess = 0.0;
            foreach (FlowEdge e in flowGraph[v])
            {
                if (v == e.from()) excess -= e.flow();
                else excess += e.flow();
            }
            return excess;
        }

        public void addEdge(FlowEdge e)
        {
            int from = e.from();
            int to = e.to();
            flowGraph[from].Add(new FlowEdge(e));
            flowGraph[to].Add(new FlowEdge(e));
            edgeCount++;
        }

        private bool hasAugmentingPath(int source, int sink)
        {
            int vertexCount = flowGraph.Length;

            edgeTo = new FlowEdge[vertexCount];
            bool[] marked = new bool[vertexCount];

            // breadth-first search
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);
            marked[source] = true;
            while (queue.Count() > 0 && !marked[sink])
            {
                int v = queue.Dequeue();

                foreach (FlowEdge e in flowGraph[v])
                {
                    int w = e.other(v);

                    // if residual capacity from v to w
                    if (e.residualCapacityTo(w) > 0)
                    {
                        if (!marked[w])
                        {
                            edgeTo[w] = e;
                            marked[w] = true;
                            queue.Enqueue(w);
                        }
                    }
                }
            }

            // is there an augmenting path?
            return marked[sink];
        }

        /// <summary>
        /// returns maximum flow possible to be pushed
        /// </summary>
        /// <returns></returns>
        public double Flow()
        {
            return flowValue;
        }

        public int EdgeCount()
        {
            return edgeCount;
        }
    }
}
