using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AlgorithmsLibrary.DynammicComponents;
using AlgorithmsLibrary.GraphAlgorithms;
using AlgorithmsLibrary.IO;
using AlgorithmsLibrary.DataStructures;

namespace AlgorithmsLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           
            //string str = @"tiny.txt";
            //List<int>[] G = IOOperations.ReadGraph(str);
            ///* reverse Graph */
            //List<int>[] GT = GraphHelper.ReverseGraph(G);
            //Kosaraju ks = new Kosaraju();
            ////ks.Run(G, GT, G.Length);
            //ks.Kosaraju_BFSRun(G, GT, G.Length);
            //ks.PrintComponents();
            
            //int[] VertexKey = {5, 4, 3, 2, 1};
            //int[] VertexElements = {0, 1, 2, 3, 4};

            //IndexedPriorityQueue<int, int> pQ = new IndexedPriorityQueue<int, int>(VertexElements, VertexKey);

            //pQ.Decrease_Key(0, -1);
            //while (pQ.Count > 0)
            //{
            //    Console.WriteLine(pQ.Minimum());
            //    pQ.Extract_Minimum();
            //}
            /*
            string str = @"spath.txt";
            AdjacencyList adjList = IOOperations.ReadAdjacencyGraph(str);
            Dijkstra ds = new Dijkstra(adjList, 0);
            ds.Run();
            for (int i = 1; i < 8; ++i)
                Console.WriteLine(ds.DistanceTo(i));
            */
            List<FlowEdge> fdgList = new List<FlowEdge>();
            FlowEdge a = new FlowEdge(0, 1, 10);
            fdgList.Add(a);
            FlowEdge[] fdgarr = new FlowEdge[10];
            fdgarr[5] = fdgList[0];
            fdgarr[5].a = 5;
            fdgarr[5].addResidualFlowTo(5, 4);
            Console.WriteLine(fdgList[0].ToString());
        }
    }
}
