using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAlgorithm
{
    public class GraphNode
    {
        public GraphNode Parent;    // 내가 어디로부터 왔는가
        public int Weight;
        public int X;
        public int Y;
        public List<GraphNode> Nears; // 근처에 갈 수 있는 노드들
        public bool IsBlock;
        public GraphNode(int weight)
        {
            Weight = weight;
            Nears = new List<GraphNode>();
            Parent = null;
            IsBlock = false;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    public class GoalComparer : IComparer<GraphNode>
    {
        GraphNode goal;
        public GoalComparer(GraphNode goal)
        {
            this.goal = goal;
        }

        public int Compare(GraphNode left, GraphNode right)
        {
            // 오른쪽이 더 작음
            // 왼쪽을 먼저 뽑아야할까, 오른쪽을 먼저 뽑아야할까
            int leftValue = Math.Abs(goal.X - left.X) + Math.Abs(goal.Y - left.Y);
            int rightValue = Math.Abs(goal.X - right.X) + Math.Abs(goal.Y - left.Y);

            if (leftValue == rightValue)
            {
                return 0;
            }
                
            if (leftValue > rightValue)
            {
                return 1;
            }
         
            return -1;
        }
    }


    public class MyIntComparer : IComparer<int>
    {
        // 5 3 1 7 10
        public int Compare(int x, int y)
        {
            if (x == y)
            {
                return 0;
            }
            if (x < y)
            {
                return 1;
            }
            return -1;
        }

        void Sort(int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                for (int j = 0; j < arr.Length; ++j)
                {
                    if (Compare(arr[i], arr[j]) > 0)
                    {
                        // 앞과 뒤를 바꿈
                        var temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }


        }

    }



}
