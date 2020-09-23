using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA
{
    class Node
    {
        int[,] state;
        Node parent;
        int steps;
        (int x, int y) zero;

        public Node(Node old)
        {
            this.state = old.state;
            this.parent = old.parent;
            this.steps = old.steps;
            this.zero = old.zero;
        }
        public Node(int[,] state, Node parent, int steps, (int x, int y) zero)
        {
            this.state = state;
            this.parent = parent;
            this.steps = steps;
            this.zero = zero;
        }
        public int[,] State { get => state; set => state = value; }
        public int Steps { get => steps; set => steps = value; }
        public (int x, int y) Zero { get => zero; set => zero = value; }
        public Node Parent { get => parent; set => parent = value; }
    }
}