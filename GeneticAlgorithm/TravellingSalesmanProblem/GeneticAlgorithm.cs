using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace TravellingSalesmanProblem
{
    class GeneticAlgorithm
    {
        private int n;
        private int populationSize;
        private List<City>[] finalPrint;
        private int generationInARow;
        Random random;

        public GeneticAlgorithm(int n)
        {
            this.n = n;
            this.populationSize = 20;
            this.finalPrint = new List<City>[5];
            for (int i = 0; i < 5; i++)
            {
                finalPrint[i] = new List<City>();
            }
            this.generationInARow = 0;
            this.random = new Random();
        }

        public int GenerationInARow { get => generationInARow; set => generationInARow = value; }
        internal List<City>[] FinalPrint { get => finalPrint; set => finalPrint = value; }

        public void solve()
        {
            List<List<City>> currentPopulation = new List<List<City>>();
            List<City> path = HelpingFunctions.GenerateRandomNPath(n);

            //Generate first population
            //Population = 20 paths
            //Path = n cities
            currentPopulation = HelpingFunctions.GenerateNRandomPopulation(path, populationSize);

            SimplePriorityQueue<List<City>> purgatory = new SimplePriorityQueue<List<City>>();

            while (GenerationInARow <= n)
            {
                for (int i = 0; i < currentPopulation.Count; i++)
                {
                    purgatory.Enqueue(currentPopulation[i], (float)HelpingFunctions.Fitness(currentPopulation[i]));
                }

                if (GenerationInARow == 0 ||
                    GenerationInARow == 5 ||
                    GenerationInARow == 10 ||
                    GenerationInARow == 15 ||
                    GenerationInARow == 20)
                {
                    if (GenerationInARow == 10)
                    {
                        for (int i = 0; i < currentPopulation[0].Count; i++)
                        {
                            FinalPrint[0].Add(currentPopulation[0][i]);
                        }
                    }
                    else if (GenerationInARow == 0)
                    {
                        for (int i = 0; i < currentPopulation[0].Count; i++)
                        {
                            FinalPrint[1].Add(currentPopulation[0][i]);
                        }
                    }
                    else if (GenerationInARow == 5)
                    {
                        for (int i = 0; i < currentPopulation[0].Count; i++)
                        {
                            FinalPrint[2].Add(currentPopulation[0][i]);
                        }
                    }
                    else if (GenerationInARow == 15)
                    {
                        for (int i = 0; i < currentPopulation[0].Count; i++)
                        {
                            FinalPrint[3].Add(currentPopulation[0][i]);
                        }
                    }
                    else if (GenerationInARow == 20)
                    {
                        for (int i = 0; i < currentPopulation[0].Count; i++)
                        {
                            FinalPrint[4].Add(currentPopulation[0][i]);
                        }
                    }
                }

                for (int i = 0; i < currentPopulation.Count; i++)
                {
                    int firstParentIndex = random.Next(0, HelpingFunctions.SixtyPrecent(currentPopulation.Count));
                    int secondParentIndex = random.Next(0, HelpingFunctions.SixtyPrecent(currentPopulation.Count));
                    //Console.WriteLine($"({firstParentIndex}, {secondParentIndex})");
                    List<City> child = HelpingFunctions.Crossover(currentPopulation[firstParentIndex], currentPopulation[secondParentIndex]);

                    float fitness = (float)HelpingFunctions.Fitness(child);
                    //Console.WriteLine($"Child: {string.Join("->", child)}, Fitness: {fitness}");
                    purgatory.Enqueue(child, fitness);

                }

                //Selection
                currentPopulation.Clear();
                for (int i = 0; i < populationSize; i++)
                {
                    currentPopulation.Add(purgatory.Dequeue());
                    HelpingFunctions.MutationFivePercent(currentPopulation[i]);
                }

                purgatory.Clear();
                GenerationInARow++;
            }

            //print
            Console.Write($"2. First Generation:\t");
            //Console.WriteLine((finalPrint[1].ToString()));
            for (int i = 0; i < finalPrint[1].Count; i++)
            {
                Console.Write(finalPrint[1][i].ToString());
            }
            Console.WriteLine(HelpingFunctions.Fitness(finalPrint[1]));

            Console.Write($"1. 10th Generation:\t");
            //Console.WriteLine((finalPrint[0].ToString()));
            for (int i = 0; i < finalPrint[0].Count; i++)
            {
                Console.Write(finalPrint[0][i].ToString());
            }
            Console.WriteLine(HelpingFunctions.Fitness(finalPrint[0]));

            Console.Write($"3. 5th Generation:\t");
            //Console.WriteLine((finalPrint[2].ToString()));
            for (int i = 0; i < finalPrint[2].Count; i++)
            {
                Console.Write(finalPrint[2][i].ToString());
            }
            Console.WriteLine(HelpingFunctions.Fitness(finalPrint[2]));

            Console.Write($"4. 15th Generation:\t");
            //Console.WriteLine((finalPrint[3].ToString()));
            for (int i = 0; i < finalPrint[3].Count; i++)
            {
                Console.Write(finalPrint[3][i].ToString());
            }
            Console.WriteLine(HelpingFunctions.Fitness(finalPrint[3]));

            Console.Write($"5. Final Generation:\t");
            //Console.WriteLine((finalPrint[4].ToString()));
            for (int i = 0; i < finalPrint[4].Count; i++)
            {
                Console.Write(finalPrint[4][i].ToString());
            }
            Console.WriteLine(HelpingFunctions.Fitness(finalPrint[4]));
        }

    }
}
