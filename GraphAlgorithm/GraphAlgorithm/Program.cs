using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithm
{
    class Program
    {
        // 1. GraphNode
        // 2. Graph
        // 3. Algorithm

        static void Main(string[] args)
        {
            Console.ReadKey();
            Console.WriteLine() ;
            string[][] arr = LoadFile("input.txt");

            // 객체지향
            Graph graph = new Graph(arr);
            DateTime time;
            double deltaTime;

            graph.Reset();
            time = DateTime.Now;
            graph.DFS();
            deltaTime = (DateTime.Now - time).TotalMilliseconds;
            Console.WriteLine($"DFS spent time : {deltaTime}ms. visits:{graph.visits.Count}");

            //graph.Reset();
            //time = DateTime.Now;
            //graph.BFS();
            //deltaTime = (DateTime.Now - time).TotalMilliseconds;
            //Console.WriteLine($"BFS spent time : {deltaTime}ms. visits:{graph.visits.Count}");

            //graph.Reset();
            //time = DateTime.Now;
            //graph.BestFristSearch();
            //deltaTime = (DateTime.Now - time).TotalMilliseconds;
            //Console.WriteLine($"Best First Search spent time : {deltaTime}ms. visits:{graph.visits.Count}");
            graph.PrintResult();

            // 1. 시작점
            // 2. 끝점
            // 3. 노드

            //GraphNode[][] nodes
            Console.ReadLine();
        }

        static string[][] LoadFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var datas = new List<string>();
            int index = 0;
            string[][] results = new string[lines.Length][];
            foreach (var line in lines)
            {
                datas.Clear();
                datas.AddRange(line.Split());
                results[index] = datas.ToArray();
                ++index;
            }

            return results;
        }
    }
}
