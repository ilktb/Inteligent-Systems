using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Board
    {
           private int[][]
        //{ 
        //    new int[]{ 1, -1, 1 }, 
        //    new int[]{ -1, -1, 1 }, 
        //   new int[] { 0, 0, 0 } 
        //}; 
        table = new int[3][].Select(x => x = new int[3]).ToArray(); //0 - free, 1 - first player, -1 - second player

        public int[][] Table
        {
            get => table;
            set
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        table[i][j] = value[i][j];
                    }
                }
            }
        }

        public bool CheckFreePos(int x, int y)
        {
            //check
            if (Table[x - 1][y - 1] == 0)
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Error.WriteLine("This position is not empty");
                return false;
            }
        }

        public void Update(int player, int x, int y)
        {
            Table[x - 1][y - 1] = player;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Table[i][j] == 0)
                    {
                        result.Append(". ");
                    }
                    else if (Table[i][j] == 1)
                    {
                        result.Append("X ");
                    }
                    else
                    {
                        result.Append("O ");
                    }
                }
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }
    }
}
