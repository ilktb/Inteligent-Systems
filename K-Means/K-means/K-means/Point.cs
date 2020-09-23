using System;

namespace K_means
{
    public class Point
    {
        private double x, y;
        private int curCluseterId, prevClusterId;

        public Point()
        {
            x = 0.0;
            y = 0.0;
            curCluseterId = -2;
            prevClusterId = -1;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public int CurCluseterId { get => curCluseterId; set => curCluseterId = value; }
        public int PrevClusterId { get => prevClusterId; set => prevClusterId = value; }


        public double Distance(Cluster v2)
        {
            double x_power = Math.Pow((X - v2.X), 2);
            double y_power = Math.Pow((Y - v2.Y), 2);

            double distance = Math.Sqrt((x_power + y_power));

            return distance;
        }
        
    }
}