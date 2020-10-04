using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesmanProblem
{
    class HelpingFunctions
    {
        public static double DistanceTwoPoints((int, int) A, (int, int) B)
        {
            double distance;
            distance = Math.Sqrt(Math.Pow((A.Item1 - B.Item1), 2) + Math.Pow((A.Item2 - B.Item2), 2));
            return distance;
        }

        public static List<City> GenerateRandomNPath(int n)
        {
            Random random = new Random();
            List<City> randomList = new List<City>();
            while(randomList.Count != n)
            {
                int randomCoordinateX = random.Next(0, n);
                int randomCoordinateY = random.Next(0, n);
                City city = new City(randomCoordinateX, randomCoordinateY);
                if (!randomList.Contains(city))
                    randomList.Add(city);
            }
            return randomList;
        }

        public static int SixtyPrecent(int n)
        {
            return (int)Math.Round((n * 0.6), 0, MidpointRounding.AwayFromZero);
        }

        public static double Fitness(List<City> path)
        {
            double fitness = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                fitness += path[i].DistanceToCity(path[i + 1]);
            }
            return fitness;
        }

        public static List<City> Crossover(List<City> path1, List<City> path2)
        {
            List<City> newPath = new List<City>();
            for (int i = 0; i < HelpingFunctions.SixtyPrecent(path1.Count); i++)
            {
                newPath.Add(path1[i]);
            }

            //vzimam ot path2 porednostta na ostanalite gradove ot path1
            SortedList<int, City> buff = new SortedList<int, City>();
            for (int i = HelpingFunctions.SixtyPrecent(path1.Count); i < path1.Count; i++)
            {
                buff.Add(path2.IndexOf(path1[i]), path1[i]);
            }

            for (int i = 0; i < buff.Count; i++)
            {
                newPath.Add(buff.Values[i]);
            }

            return newPath;
        }

        public static bool MutationFivePercent(List<City> path)
        {
            Random random = new Random();
            int chance = random.Next(0, 100);

            if (chance <= 5)
            {
                //swap first with last city
                City temp = new City(path[0].X, path[0].Y);
                path[0] = path[path.Count - 1];
                path[path.Count - 1] = temp;

                //swap the middle two
                temp = new City(path[(path.Count - 1) / 2].X, path[(path.Count - 1) / 2].Y);
                path[(path.Count - 1) / 2] = path[(path.Count - 1) / 2 + 1];
                path[(path.Count - 1) / 2 + 1] = temp;

                return true;
            }
            return false;
        }

        public static List<List<City>> GenerateNRandomPopulation(List<City> firstPath, int n)
        {
            List<List<City>> population = new List<List<City>>();
            Random random = new Random();

            for (int k = 0; k < n; k++)
            {
                City[] newRandomPath = new City[firstPath.Count];
                for (int i = 0; i <= firstPath.Count / 2; i++)
                {
                    while (true)
                    {
                        int indexRandom = random.Next(0, firstPath.Count);
                        if (newRandomPath[indexRandom] == null)
                        {
                            newRandomPath[indexRandom] = firstPath[i];
                            break;
                        }
                    }
                }

                for (int i = firstPath.Count / 2 + 1; i < firstPath.Count; i++)
                {
                    for (int j = 0; j < newRandomPath.Length; j++)
                    {
                        if (newRandomPath[j] == null)
                        {
                            newRandomPath[j] = firstPath[i];
                            break;
                        }
                    }
                }
                population.Add(newRandomPath.ToList());
            }

            return population;
        }

        public static void PrintPopulation(List<List<City>> population)
        {

            for (int j = 0; j < population[0].Count; j++)
            {
                population[0][j].print();
            }
            Console.WriteLine();
        }
    }
}
