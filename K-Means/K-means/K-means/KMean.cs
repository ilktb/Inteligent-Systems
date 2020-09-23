using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means
{
    public class KMean
    {
        private Cluster[] GenerateRandomCentroids(int clusters, List<Point> points)
        {
            var centroids = new Cluster[clusters];
            for (int i = 0; i < clusters; i++)
            {
                centroids[i] = new Cluster();
            }
            var copyList = new List<Point>(points);

            Random random = new Random();
            for (int i = 0; i < clusters; i++)
            {
                int index = random.Next(0, copyList.Count-1);
                centroids[i].X = copyList[index].X;
                centroids[i].Y = copyList[index].Y;
                centroids[i].Id = i;
                copyList.RemoveAt(index);
            }
            return centroids;
        }

        private void AssignCentroidToPoint(Cluster[] clusters, Point point)
        {
            double minDistance = double.MaxValue-1;
            int indexClosestCentroid = -1;
            for (int i = 0; i < clusters.Length; i++)
            {
                if (minDistance > point.Distance(clusters[i]))
                {
                    minDistance = point.Distance(clusters[i]);
                    indexClosestCentroid = i;
                }
            }
            clusters[indexClosestCentroid].AddPoint(point);
            point.PrevClusterId = point.CurCluseterId;
            point.CurCluseterId = clusters[indexClosestCentroid].Id;
        }

        private void ChangeCentroids(Cluster[] centroids)
        {
            for (int i = 0; i < centroids.Length; i++)
            {
                centroids[i].SetNewCoordinates(centroids[i].MeanCoordinates());
            }
        }

        public void K_Mean(List<Point> setAllPoints, int numberClusters)
        {
            var centroids = new Cluster[numberClusters];
            centroids = GenerateRandomCentroids(numberClusters, setAllPoints);

            bool channgedCentoid = true;
            while (channgedCentoid)
            {
                foreach (var point in setAllPoints)
                {
                    channgedCentoid = false;
                    if (point.PrevClusterId != point.CurCluseterId)
                    {
                        for (int i = 0; i < setAllPoints.Count; i++)
                        {
                            AssignCentroidToPoint(centroids, setAllPoints[i]);
                        }

                        ChangeCentroids(centroids);
                        channgedCentoid = true;
                        continue; // if change centroids skip foreach and start again from zero index
                    }                     
                }
            }
        }
    }
}
