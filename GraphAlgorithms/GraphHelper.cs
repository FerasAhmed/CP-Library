using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    public class Edge
    {
        public int a, b;
        public double edgeWeight;
        public Edge(int _a, int _b) { a = _a; b = _b; }
        public Edge(int _a, int _b, double _edgeWeigth) : this(_a, _b)
        {
            edgeWeight = _edgeWeigth;
        }
    }

    // code was originally written by ideas from https://algs4.cs.princeton.edu/64maxflow/FlowEdge.java.html
    public class FlowEdge : Edge
    {
        double _capacity = 0;
        double _flow = 0;

        public FlowEdge(Edge e) : this(e.a, e.b, e.edgeWeight)
        {

        }
        
        public FlowEdge(int from, int to, double capacity) : base(from, to, capacity)
        {
            _capacity = capacity;
        }

        public int other(int vertex)
        {
            if (vertex == base.a)
                return base.b;
            else if (vertex == base.b)
                return base.a;

            throw new Exception("Invalid given vertex");
        }
        public double flow() { return _flow; }
        public int from() { return base.a; }
        public int to() { return base.b; }
        public double residualCapacityTo(int vertex)
        {
            if (vertex == base.b) return _flow;              // backward edge
            else if (vertex == base.a) return _capacity - _flow;   // forward edge
            else throw new Exception("invalid vertex given as an endpoint.");
        }

        /// <summary>
        /// adding residual flow to edge from one of the two vertices, it can automatically detect +/- flow
        /// </summary>
        /// <param name="vertex"> the vertex that we want to push flow from</param>
        /// <param name="delta"> amount of flow to push always > 0</param>
        public void addResidualFlowTo(int vertex, double delta)
        {
            if (delta <= 0.0)
                throw new Exception($"Adding delta to edge with invalid value {delta}");

            if (vertex == base.b) _flow -= delta;           // backward edge
            else if (vertex == base.a) _flow += delta;      // forward edge
            else throw new Exception("invalid vertices were given.");

            // catch rounding issues and round flow to nearst zero or full capacity
            if (Math.Abs(_flow) <= float.Epsilon)
                _flow = 0;
            if (Math.Abs(_flow - _capacity) <= float.Epsilon)
                _flow = _capacity;

            // validate flow after updates
            if (_flow < 0.0)
                throw new Exception("Flow is negative");
            if (_flow > _capacity)
                throw new Exception("Flow exceeds capacity");
        }
        public override string ToString()
        {
            return $" {base.a} -> {base.b} with {_flow} / {_capacity}";
        }
    }

    class GraphHelper
    {
        static public List<int>[] ReverseGraph(List<int>[] G)
        {
            List<int>[] GT = new List<int>[G.Length];
            for (int i = 0; i < G.Length; ++i)
                GT[i] = new List<int>();

            for (int from = 0; from < G.Length; ++from)
                for (int to = 0; to < G[from].Count; ++to)
                    GT[G[from][to]].Add(from);

            return GT;
        }
    }
}
