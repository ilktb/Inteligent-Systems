using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConflict
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter N: ");

            int n = int.Parse(Console.ReadLine());
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Board board = new Board(n);

            //board.PrintBoard();
          
            //Console.WriteLine();

            board.solve(n);
            

           /* int[][] conflicts = board.GetConflicts();

            for (int i = 0; i < conflict.Length; i++)
            {
                for (int j = 0; j < conflicts[i].Length; j++)
                {
                    Console.Write($"{conflicts[i][j]} ");
                }
                Console.WriteLine();
            }*/
            
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"{elapsedMs} Ms"); //1sec = 1000Ms
            
            //board.PrintBoard();
            
            //Console.WriteLine($"{elapsedMs} Ms"); //1sec = 1000Ms
        }
    }
}
