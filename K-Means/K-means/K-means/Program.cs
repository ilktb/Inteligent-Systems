using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_means
{
    class Program
    {
        public static void ReadingInputFromFile(List<Point> input)
        {
            int rowsNumber = int.Parse(Console.ReadLine());
            for (int i = 0; i < rowsNumber; i++)
            {
                Point current = new Point();
                string line = Console.ReadLine();
                string[] splitLine = line.Split();

                double[] p = new double[2];
                for (int j = 0; j < 2; j++)
                {
                    p[j] = Convert.ToDouble(splitLine[j]);
                }

                current.X = p[0];
                current.Y = p[1];
                input.Add(current);
            }
        }

        public static void GenerateOutput(List<Point> setAllPoints)
        {
            Console.WriteLine(setAllPoints.Count);
            foreach (var point in setAllPoints)
            {
                Console.WriteLine(point.X*1000);
                Console.WriteLine(point.Y*1000);
                Console.WriteLine(point.CurCluseterId*1000);
            }
        } 
        static void Main(string[] args)
        {
            var setAllPoints = new List<Point>();
            ReadingInputFromFile(setAllPoints);
            int numberClusters = int.Parse(args[0]);
            //Console.WriteLine(numberClusters);

            KMean solution = new KMean();
            solution.K_Mean(setAllPoints, numberClusters);

            GenerateOutput(setAllPoints);
        }
    }
}
