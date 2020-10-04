using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class City
    {
        private int x;
        private int y;

        public City(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                City p = (City)obj;
                return (x == p.x) && (y == p.y);
            }
        }
        public double DistanceToCity(City other)
        {
            return HelpingFunctions.DistanceTwoPoints((this.X, this.Y), (other.X, other.Y));
        }

        public void print()
        {
            Console.Write($"({X},{Y}) ");
        }

        public override string ToString()
        {
            return $"({X},{Y}) ";
        }
    }
}
