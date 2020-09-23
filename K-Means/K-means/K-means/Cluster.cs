using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means
{
    public class Cluster
    {
        private double x, y;
        private int id;
        private List<Point> clusters;

        public Cluster()
        {
            X = 0.0;
            Y = 0.0;
            Id = -1;
            Clusters = new List<Point>();
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public int Id { get => id; set => id = value; }
        public List<Point> Clusters { get => clusters; set => clusters = value; }

        public (double, double) MeanCoordinates ()
        {
            double meanX = 0, meanY = 0;
            foreach (var point in clusters)  
            {
                meanX += point.X;
                meanY += point.Y;
            }
            meanX /= clusters.Count;
            meanY /= clusters.Count;
            return (meanX, meanY);
        }

        public void AddPoint(Point a)
        {
            Clusters.Add(a);
        }

        public void DeletePoints()
        {
            Clusters.Clear();
        }

        public void SetNewCoordinates((double, double) coordinates)
        {
            X = coordinates.Item1;
            Y = coordinates.Item2;
        }
    }
}
