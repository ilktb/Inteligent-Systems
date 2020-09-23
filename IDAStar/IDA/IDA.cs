using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace IDA
{
    class IDA
    {
        public static void copyMatrix(int[,] dst, int[,] src)
        {
            for (int i = 0; i < src.GetLength(0); i++)
            {
                for (int j = 0; j < src.GetLength(1); j++)
                {
                    dst[i, j] = src[i, j];
                }
            }
        }

        public static bool IsEquals(int[,] first, int[,] second)
        {
            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < first.GetLength(1); j++)
                {
                    if (first[i, j] != second[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static int[,] GenerateGoalState(int indexZero, int numberBlocks)
        {
            int[,] goalState = new int[(int)Math.Sqrt(numberBlocks + 1), (int)Math.Sqrt(numberBlocks + 1)];
            int counter = 0;
            int number = 1;

            for (int i = 0; i < (int)Math.Sqrt(numberBlocks + 1); i++)
            {
                for (int j = 0; j < (int)Math.Sqrt(numberBlocks + 1); j++)
                {
                    if (indexZero != -1)
                    {
                        counter++;
                        if (indexZero == counter)
                        {
                            goalState[i, j] = 0;
                            continue;
                        }
                        goalState[i, j] = number;
                        number++;
                    }
                    else
                    {
                        if (number == numberBlocks + 1)
                        {
                            goalState[i, j] = 0;
                            number++;
                            continue;
                        }
                        goalState[i, j] = number;
                        number++;
                    }
                }
            }
            return goalState;
        }

        public static string Move(Node child)
        {
            if (child.Zero.x < child.Parent.Zero.x)
            {
                return "down";
            }
            else if (child.Zero.x > child.Parent.Zero.x)
            {
                return "up";
            }
            else if (child.Zero.y < child.Parent.Zero.y)
            {
                return "right";
            }
            else if (child.Zero.y > child.Parent.Zero.y)
            {
                return "left";
            }
            return "error with Move function";
        }

        public static void PrintPath(Node salution)
        {
            Console.WriteLine($"Steps: {salution.Steps}");
            Stack<string> path = new Stack<string>();
            while (salution.Parent != null)
            {
                path.Push(Move(salution));
                salution = salution.Parent;
            }
            while (path.Count > 0)
            {
                Console.WriteLine(path.Pop());
            }
        }

        public static int Heuristic(Node curState, int[,] goalState)
        {
            int manhattanDist = 0;
            for (int i = 0; i < curState.State.GetLength(0); i++)
            {
                for (int j = 0; j < curState.State.GetLength(1); j++)
                {
                    (int x, int y) index = IDA.GetIndexofNumber(curState.State[i, j], goalState);
                    manhattanDist += Math.Abs(i - index.x) + Math.Abs(j - index.y);
                }
            }
            return manhattanDist;
        }

        public static int FValue(Node curState, int[,] goalState)
        {
            int fValue = 0;
            int manhattanDist = Heuristic(curState, goalState);
            fValue = curState.Steps + manhattanDist;
            return fValue;
        }

        public static (int, int) GetIndexofNumber(int number, int[,] matrix)
        {
            (int x, int y) index = (-1, -1);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == number)
                    {
                        index.x = i;
                        index.y = j;
                        continue;
                    }
                }
            }
            return index;
        }

        public static int[,] Swap((int x, int y) zero, string direction, int[,] state)
        {
            int[,] newState = new int[state.GetLength(0), state.GetLength(1)];
            copyMatrix(newState, state);
            if (direction == "top")
            {
                int temp;
                temp = newState[zero.x - 1, zero.y];
                newState[zero.x - 1, zero.y] = 0;
                newState[zero.x, zero.y] = temp;
            }
            else if (direction == "bottom")
            {
                int temp;
                temp = newState[zero.x + 1, zero.y];
                newState[zero.x + 1, zero.y] = 0;
                newState[zero.x, zero.y] = temp;
            }
            else if (direction == "left")
            {
                int temp;
                temp = newState[zero.x, zero.y - 1];
                newState[zero.x, zero.y - 1] = 0;
                newState[zero.x, zero.y] = temp;
            }
            else if (direction == "right")
            {
                int temp;
                temp = newState[zero.x, zero.y + 1];
                newState[zero.x, zero.y + 1] = 0;
                newState[zero.x, zero.y] = temp;
            }
            return newState;
        }

        public static List<Node> GetChildren(Node curState)
        {
            List<Node> children = new List<Node>();

            //top
            if (curState.Zero.x > 0)
            {
                Node child = new Node(curState);
                child.State = IDA.Swap(child.Zero, "top", child.State);
                child.Zero = IDA.GetIndexofNumber(0, child.State);
                child.Steps = curState.Steps + 1;
                child.Parent = curState;

                if (child.Parent == null || child.Parent.Parent == null)
                {
                    children.Add(child);
                }
                else if (!IsEquals(child.State, child.Parent.Parent.State))
                {
                    children.Add(child);
                }
            }
            //bot
            if (curState.Zero.x < curState.State.GetLength(0) - 1)
            {
                Node child = new Node(curState);
                child.State = IDA.Swap(child.Zero, "bottom", child.State);
                child.Zero = IDA.GetIndexofNumber(0, child.State);
                child.Steps = curState.Steps + 1;
                child.Parent = curState;

                if (child.Parent == null || child.Parent.Parent == null)
                {
                    children.Add(child);
                }
                else if (!IsEquals(child.State, child.Parent.Parent.State))
                {
                    children.Add(child);
                }
            }

            //left
            if (curState.Zero.y > 0)
            {
                Node child = new Node(curState);
                child.State = IDA.Swap(child.Zero, "left", child.State);
                child.Zero = IDA.GetIndexofNumber(0, child.State);
                child.Steps = curState.Steps + 1;
                child.Parent = curState;

                if (child.Parent == null || child.Parent.Parent == null)
                {
                    children.Add(child);
                }
                else if (!IsEquals(child.State, child.Parent.Parent.State))
                {
                    children.Add(child);
                }
            }

            //right
            if (curState.Zero.y < curState.State.GetLength(1) - 1)
            {
                Node child = new Node(curState);
                child.State = IDA.Swap(child.Zero, "right", child.State);
                child.Zero = IDA.GetIndexofNumber(0, child.State);
                child.Steps = curState.Steps + 1;
                child.Parent = curState;

                if (child.Parent == null || child.Parent.Parent == null)
                {
                    children.Add(child);
                }
                else if (!IsEquals(child.State, child.Parent.Parent.State))
                {
                    children.Add(child);
                }
            }

            return children;
        }

        public static void IDAStar(Node startState, int[,] finalState)
        {
            int treshhold = Heuristic(startState, finalState);
            SimplePriorityQueue<Node> pq = new SimplePriorityQueue<Node>();
            PrioritySet<int> ps = new PrioritySet<int>();

            //int minTreshhold = FValue(startState, finalState);

            ps.Add(FValue(startState, finalState), FValue(startState, finalState));
            while (true)
            {
                pq.Enqueue(startState, FValue(startState, finalState));
                while (pq.Count > 0)
                {
                    Node current = pq.Dequeue();
                    int curHeuristic = Heuristic(current, finalState);
                    if (curHeuristic == 0)
                    {
                        // print final state
                        PrintPath(current);
                        /*for (int i = 0; i < current.State.GetLength(0); i++)
                        {
                            for (int j = 0; j < current.State.GetLength(1); j++)
                            {
                                Console.Write(current.State[i, j] + " ");
                            }
                            Console.WriteLine();
                        }*/
                        return;
                    }

                    List<Node> curChildren = IDA.GetChildren(current);
                    foreach (var child in curChildren)
                    {
                        if (FValue(child, finalState) <= treshhold)
                        {
                            pq.Enqueue(child, FValue(child, finalState));
                        }
                        else
                        {
                            /*if (minTreshhold > FValue(child, finalState))
                            {
                                minTreshhold = FValue(child, finalState);
                            }*/
                            ps.Add(FValue(child, finalState), FValue(child, finalState));
                        }
                    }
                    /* // print current state
                     Console.WriteLine($"\nStep {current.Steps}:");
                     for (int i = 0; i < current.State.GetLength(0); i++)
                     {
                         for (int j = 0; j < current.State.GetLength(1); j++)
                         {
                             Console.Write(current.State[i, j] + " ");
                         }
                         Console.WriteLine();
                     }*/
                }
                //treshhold = minTreshhold;
                treshhold = ps.Dequeue();
            }
        }
    }
}
