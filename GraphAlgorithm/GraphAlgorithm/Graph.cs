using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithm
{
    public class Graph
    {
        public GraphNode[][] arr2;
        public GraphNode start = null;
        public GraphNode end = null;
        public List<GraphNode> visits;
        public Graph(string[][] arr)
        {
            visits = new List<GraphNode>();
            Create(arr);
            Link();
        }

        public void Create(string[][] arr)
        {
            // 노드를 만듭니다
            arr2 = new GraphNode[arr.Length][];

            for (int i = 0; i < arr.Length; ++i)
            {
                arr2[i] = new GraphNode[arr[i].Length];
                for (int j = 0; j < arr[i].Length; ++j)
                {
                    string a;
                    a = arr[i][j];

                    int b;
                    if (int.TryParse(a, out b))
                    {
                        arr2[i][j] = new GraphNode(b);
                    }

                    if (arr2[i][j] == null)
                    {
                        arr2[i][j] = new GraphNode(0);
                    }

                    arr2[i][j].X = i;
                    arr2[i][j].Y = j;

                    if (arr[i][j] == "@")
                    {
                        start = arr2[i][j];
                    }

                    if (arr[i][j] == "*")
                    {
                        end = arr2[i][j];
                    }

                    if (arr[i][j].ToLower() == "x")
                    {
                        arr2[i][j].IsBlock = true;
                    }
                }
            }

        }

        public void Link()
        {
            // 노드를 이어줍니다.
            for (int i = 0; i < arr2.Length; ++i)
            {
                for (int j = 0; j < arr2[i].Length; ++j)
                {
                    GraphNode n = arr2[i][j];
                    if (n.IsBlock)
                    {
                        continue;
                    }

                    if (arr2[i].Length - 1 > j)
                    {
                        GraphNode temp = arr2[i][j + 1];
                        if (temp.IsBlock == false)
                        {
                            n.Nears.Add(temp);
                        }
                    }

                    if (j > 0)
                    {
                        GraphNode temp = arr2[i][j - 1];
                        if (temp.IsBlock == false)
                        {
                            n.Nears.Add(temp);
                        }
                    }

                    if (i > 0)
                    {
                        GraphNode temp = arr2[i - 1][j];
                        if (temp.IsBlock == false)
                        {
                            n.Nears.Add(temp);
                        }
                    }

                    if (arr2.Length - 1 > i)
                    {
                        GraphNode temp = arr2[i + 1][j];
                        if (temp.IsBlock == false)
                        {
                            n.Nears.Add(temp);
                        }
                    }
                }
            }
        }

        public void Reset()
        {
            visits.Clear();
            for (int i = 0; i < arr2.Length; ++i)
            {
                for (int j = 0; j < arr2[i].Length; ++j)
                {
                    arr2[i][j].Parent = null;
                }
            }
        }

     
        public void BFS()
        {
            // 너비 우선 검색
            Queue<GraphNode> queue = new Queue<GraphNode>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                GraphNode node = queue.Dequeue();
                visits.Add(node);

                if (node == end)
                {
                    break;
                }

               
                for (int i = 0; i < node.Nears.Count; ++i)
                {
                    GraphNode temp = node.Nears[i];
                    if (queue.Contains(temp) == false
                        && visits.Contains(temp) == false)
                    {
                        temp.Parent = node;
                        queue.Enqueue(temp);
                    }
                }
                
            }
        }
 
        public void DFS()
        {
            // 깊이 우선 검색
            Stack<GraphNode> stack = new Stack<GraphNode>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                GraphNode node = stack.Pop();
                visits.Add(node);

                if (node == end)
                {
                    break;
                }

                
                for (int i = 0; i < node.Nears.Count; ++i)
                {
                    GraphNode temp = node.Nears[i];
                    if (stack.Contains(temp) == false
                        && visits.Contains(temp) == false)
                    {
                        temp.Parent = node;
                        stack.Push(temp);
                    }
                }
            }
        }

        public void BestFristSearch()
        {
            GoalComparer comparer = new GoalComparer(end);
            PriorityQueue<GraphNode> queue = new PriorityQueue<GraphNode>(10000, comparer);

            queue.Push(start);

            while (queue.Count > 0)
            {
                GraphNode node = queue.Top;
                queue.Pop();
                visits.Add(node);

                if (node == end)
                {
                    
                    break;
                }

                
                for (int i = 0; i < node.Nears.Count; ++i)
                {
                    GraphNode temp = node.Nears[i];
                    if (queue.Contains(temp) == false
                        && visits.Contains(temp) == false)
                    {
                        temp.Parent = node;
                        queue.Push(temp);
                    }
                }
            }
        }

        public void PrintResult()
        {
            Console.WriteLine($"도착. 탐색 수 : {visits.Count}");
            Stack<GraphNode> stack = new Stack<GraphNode>();
            GraphNode temp = end;
            while (temp != null)
            {
                stack.Push(temp);
                temp = temp.Parent;
            }

            List<GraphNode> list = new List<GraphNode>();
            while (stack.Count > 0)
            {
                temp = stack.Pop();
                list.Add(temp);
                Console.WriteLine($"{temp.X}, {temp.Y}");
            }

            for (int i = 0; i < arr2.Length; ++i)
            {
                for (int j = 0; j < arr2[i].Length; ++j)
                {
                    if (i == start.X && j == start.Y)
                    {
                        Console.Write("@\t" );
                    }
                    else if (i == end.X && j == end.Y)
                    {
                        Console.Write("*\t");
                    }
                    else if (arr2[i][j].IsBlock)
                    {
                        Console.Write("X\t");
                    }
                    else
                    {
                        int index = list.IndexOf(arr2[i][j]);
                        if (index != -1)
                        {
                            Console.Write(index + "\t");
                        }
                        else if (visits.Contains(arr2[i][j]))
                        {
                            Console.Write("♡\t");
                        }
                        else
                        {
                            Console.Write("_\t");
                            
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
