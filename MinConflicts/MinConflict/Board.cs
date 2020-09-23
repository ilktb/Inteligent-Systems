using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConflict
{
    class Board
    {
        Random random = new Random();
        int[] configurationRows;
        int[][] conflicts;

        public Board(int n)
        {
            initialize(n);
        }

        void initialize(int n)
        {
            configurationRows = new int[n];
            conflicts = new int[n][];
            for (int k = 0; k < n; k++)
            {
                conflicts[k] = new int[n];
            }

            int i = 0;
            int j = random.Next(0, conflicts.Length);
            configurationRows[i] = j;
            calculateConflictsNewQ(i, configurationRows[i], conflicts);

            for (i = 1; i < configurationRows.Length; i++)
            {

                /*for (int g = 0; g < conflicts.Length; g++)
                {
                    for (int p = 0; p < conflicts[g].Length; p++)
                    {
                        Console.Write($"{conflicts[g][p]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                */

                List<int> minConflictsList = indexMinConflicts(conflicts[i]);
                j = random.Next(0, minConflictsList.Count);
                configurationRows[i] = minConflictsList[j];
                calculateConflictsNewQ(i, configurationRows[i], conflicts);
            }
        }

        public void solve(int n)
        {
            List<int> candidates = new List<int>();
            int moves = 0;
            int randomRestarts = 0;
            while (true)
            {
                int maxConflictsOfQueen = 0; // Queens with max conflicts
                candidates.Clear();

                //Put in list worst Queens
                for (int c = 0; c < configurationRows.Length; c++)
                {
                    int conflictsOfQueen = conflicts[c][configurationRows[c]];
                    if (conflictsOfQueen == maxConflictsOfQueen)
                    {
                        candidates.Add(c);
                    }
                    else if (conflictsOfQueen > maxConflictsOfQueen)
                    {
                        maxConflictsOfQueen = conflictsOfQueen;
                        candidates.Clear();
                        candidates.Add(c);
                    }
                }

                //Check if all Queens doesn't have conflicts
                if(maxConflictsOfQueen == 0)
                {
                    Console.WriteLine($"Moves: {moves}");
                    Console.WriteLine($"Random Restarts: {randomRestarts}");
                    return;
                }

                //Choose which queen to move
                List<int> maxProfitList = MaxProfitList(configurationRows, conflicts);
                var bestCandidates = maxProfitList.Intersect(candidates).ToList();

                //Chupi se ako nqma bestCandidates
                if (bestCandidates.Count == 0)
                {
                    randomRestarts++;
                    initialize(n);
                    moves = 0;
                }
            
                int randomIndex = random.Next(0, bestCandidates.Count()-1);
                
                int worstRow = bestCandidates.ToArray()[randomIndex];

                move(worstRow, configurationRows, conflicts);

                //PrintBoard();
                //Console.WriteLine();

                moves++;
                //Random Restart
                if(moves == 150)
                {
                    randomRestarts++;
                    initialize(n);
                    moves = 0;
                }
            }
        }

        //Move the queen of curPosition to random or min conflicts and recalculate coflicts
        public void move(int row, int[] config, int[][] conflicts)
        {
            List<int> indexesMinConflicts = indexMinConflicts(conflicts[row]);
            int randomIndex = random.Next(0, indexesMinConflicts.Count - 1);
            int newQueenY = indexesMinConflicts[randomIndex];

            calcualteConflictsSwapedQ(row, config[row], conflicts);

            config[row] = newQueenY;

            calculateConflictsNewQ(row, config[row], conflicts);
        }

        //Indexes of min conflicts
        public List<int> MinConflictsRowList(int[] row)
        {
            List<int> listMinIndexes = new List<int>();
            int minConflict = row.Min();

            for (int i = 0; i < row.Length; i++)
            {
                if (minConflict == row[i])
                {
                    listMinIndexes.Add(i);
                }
            }

            return listMinIndexes;
        }

        //Return List of all indexes of max profits
        public List<int> MaxProfitList(int[] config, int[][] conflicts)
        {
            int maxProfit = profit(config[0], conflicts[0]);

            List<int> indexesMaxProfit = new List<int>();

            for (int i = 1; i < conflicts.Length; i++)
            {
                if (maxProfit < profit(config[i], conflicts[i]))
                {
                    maxProfit = profit(config[i], conflicts[i]);
                }
            }

            for (int i = 0; i < conflicts.Length; i++)
            {
                if (maxProfit == profit(config[i], conflicts[i]))
                {
                    indexesMaxProfit.Add(i);
                }
            }

            return indexesMaxProfit;
        }

        //Calculate the profit in one row (Max conflict - Min conflict)
        public int profit(int queenIndex, int[] row)
        {
            return row[queenIndex] - row.Min();
        }

        //Return list of all indexes of min values
        public List<int> indexMinConflicts(int[] conflicts)
        {
            List<int> indexes = new List<int>();
            int min = conflicts.Min();

            for (int i = 0; i < conflicts.Length; i++)
            {
                if (min == conflicts[i])
                {
                    indexes.Add(i);
                }
            }
            return indexes;
        }

        //Increase conflicts when we put new queen
        public void calculateConflictsNewQ(int queenX, int queenY, int[][] conflicts)
        {
            int tempX = new int();
            int tempY = new int();

            //down
            tempX = queenX + 1;
            tempY = queenY;
            while (tempX < conflicts.Length)
            {
                conflicts[tempX][tempY]++;
                tempX++;
            }

            //up
            tempX = queenX - 1;
            tempY = queenY;
            while (tempX >= 0)
            {
                conflicts[tempX][tempY]++;
                tempX--;
            }

            //up left diagonal
            tempX = queenX - 1;
            tempY = queenY - 1;
            while (tempY >= 0 && tempX >= 0)
            {
                conflicts[tempX][tempY]++;
                tempX--;
                tempY--;
            }

            //up right diagonal
            tempX = queenX - 1;
            tempY = queenY + 1;
            while (tempY < conflicts.Length && tempX >= 0)
            {
                conflicts[tempX][tempY]++;
                tempX--;
                tempY++;
            }

            //down left diagonal
            tempX = queenX + 1;
            tempY = queenY - 1;
            while (tempY >= 0 && tempX < conflicts.Length)
            {
                conflicts[tempX][tempY]++;
                tempX++;
                tempY--;
            }

            //down right diagonal
            tempX = queenX + 1;
            tempY = queenY + 1;
            while (tempY < conflicts.Length && tempX < conflicts.Length)
            {
                conflicts[tempX][tempY]++;
                tempX++;
                tempY++;
            }
        }

        //Reduce conflicts when we move queen. Generally used in combination with CalculateConflictsNewQ
        public void calcualteConflictsSwapedQ(int oldQueenX, int oldQueenY, int[][] conflicts)
        {
            int tempX = new int();
            int tempY = new int();

            //down
            tempX = oldQueenX + 1;
            tempY = oldQueenY;
            while (tempX < conflicts.Length)
            {
                conflicts[tempX][tempY]--;
                tempX++;
            }

            //up
            tempX = oldQueenX - 1;
            tempY = oldQueenY;
            while (tempX >= 0)
            {
                conflicts[tempX][tempY]--;
                tempX--;
            }

            //up left diagonal
            tempX = oldQueenX - 1;
            tempY = oldQueenY - 1;
            while (tempY >= 0 && tempX >= 0)
            {
                conflicts[tempX][tempY]--;
                tempX--;
                tempY--;
            }

            //up right diagonal
            tempX = oldQueenX - 1;
            tempY = oldQueenY + 1;
            while (tempY < conflicts.Length && tempX >= 0)
            {
                conflicts[tempX][tempY]--;
                tempX--;
                tempY++;
            }

            //down left diagonal
            tempX = oldQueenX + 1;
            tempY = oldQueenY - 1;
            while (tempY >= 0 && tempX < conflicts.Length)
            {
                conflicts[tempX][tempY]--;
                tempX++;
                tempY--;
            }

            //down right diagonal
            tempX = oldQueenX + 1;
            tempY = oldQueenY + 1;
            while (tempY < conflicts.Length && tempX < conflicts.Length)
            {
                conflicts[tempX][tempY]--;
                tempX++;
                tempY++;
            }
        }

        //Getter - pls use properties
        public int[] GetConfigurationRows()
        {
            return configurationRows;
        }

        //Getter - pls use properties
        public int[][] GetConflicts()
        {
            return conflicts;
        }

        //Print - pls use ToString()
        public void PrintBoard()
        {
            for (int i = 0; i < configurationRows.Length; i++)
            {
                for (int j = 0; j < configurationRows[i]; j++)
                {
                    Console.Write("_ ");
                }

                Console.Write("* ");

                for (int j = configurationRows[i]; j < configurationRows.Length - 1; j++)
                {
                    Console.Write("_ ");
                }
                Console.WriteLine();
            }
        }
    }
}
