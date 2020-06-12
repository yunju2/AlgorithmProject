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

        /*
          void breadth_first_tree_search (tree T); {
                queue_of_node Q;
                node u, v;

                initialize (Q);

                v = root of T;

                visit v;
                enqueue (Q,v);

                while (not empty (Q)) 
                {
                    dequeue (Q,v);
                    for (each child u of v) 
                    {
                        visit u;
                        enqueue (Q,u); 
                    }
                }
            }
         */
        //public void Test()
        //{
        //    int[] arr2 = new int[] { 1, 3, 5, 6, 6, 2, 4, 6, 8 };
        //    int t = 6;

        //    // 한바퀴 돌면서 있는지 없는지 체크를한다.

        //    // 있으면 있다
        //    // 없으면 없다

        //    // t가 arr2안에 몇개 있는지
        //    bool isPresent = false;
        //    for (int k = 0; k < arr2.Length; ++k)
        //    {
        //        if (t == arr2[k])
        //        {
        //            isPresent = true;
        //        }
        //    }

        //    if (isPresent == false)
        //    {
        //        Console.WriteLine("없다");
        //    }
        //    else
        //    {
        //        Console.WriteLine("있다.");
        //    }
        //}


        //public void Test2()
        //{
        //    PriorityQueue<int> pq = new PriorityQueue<int>(10, new MyIntComparer());
        //    pq.Push(10);
        //    pq.Push(1);
        //    pq.Push(5);
        //    pq.Push(7);
        //    pq.Push(2);

        //    while(pq.Count > 0)
        //    {
        //        int value = pq.Top;
        //        pq.Pop();
        //        Console.WriteLine(value);
        //    }
        //}

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
        // 코드 작성
        // 1. 지금 방문한 node 기준으로 갈 수 있는 곳을 집어넣기 (nears 이용)

        // 2. 방문할 곳 중에는 불필요한 것은 넣지 않는다
        //    (ex : 한 번 방문했던 곳 (visit)는 넣어도 의미가 없기 때문에 빼도 무방하다. -> a* 예외)
        // 3.백트래킹을 이용해서 최종 길을 잇는다
        // 3-1. Parent 변수에 이어준다.
        // 3-2. 부모 변수(Parent)를 이용해서 내가 어떤길을 통해서 왔는지 표현한다.
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
                    // 3-2. 부모 변수(Parent)를 이용해서 내가 어떤길을 통해서 왔는지 표현한다.
                    break;
                }

                // 코드 작성
                // 1. 지금 방문한 node 기준으로 갈 수 있는 곳을 집어넣기 (nears 이용)

                // 2. 방문할 곳 중에는 불필요한 것은 넣지 않는다
                //    (ex : 한 번 방문했던 곳 (visit)는 넣어도 의미가 없기 때문에 빼도 무방하다. -> a* 예외)
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
                            //Console.Write(arr2[i][j].Weight + "\t");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
