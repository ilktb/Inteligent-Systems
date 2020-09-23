using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Game
    {
        public static void Play(Board board)
        {
            Console.WriteLine("Which turn do you want to start? \n1 or 2");

            int human = int.Parse(Console.ReadLine());
            int playerTurn = human == 1 ? 1 : -1;
            int counter = 0;
            while (true)
            {
                Console.WriteLine(board);
                if (playerTurn == -1 && counter == 0 || counter == 1)
                {

                    board.Table[1][1] = -1;
                    playerTurn = 1;
                    counter++;
                    continue;
                }
                int result = Minimax.Is_end(board);

                // Print the message if the game has ended
                if (result != 2)
                {
                    if (result == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You are the winner!");
                        SoundPlayer soundplayer = new SoundPlayer(@"D:\СУ\AI\HW4\ConsoleApp1\ConsoleApp1\Sounds\winner.wav");
                        soundplayer.Play();
                    }
                    else if (result == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You lost!");
                        SoundPlayer soundplayer = new SoundPlayer(@"D:\СУ\AI\HW4\ConsoleApp1\ConsoleApp1\Sounds\loser.wav");
                        soundplayer.Play();
                    }
                    else if (result == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("It's a tie");
                    }
                    return;
                }

                // If it's player's turn
                if (playerTurn == 1)
                {
                    while (true)
                    {
                        (int qx, int qy) = Minimax.FindBestMove(board);
                        Console.WriteLine($"Recommended move: X = {qx + 1}, Y = {qy + 1}");

                        Console.WriteLine("Enter coordinates in format - \"X Y\"");
                        var input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int px = int.Parse(input[0]);
                        int py = int.Parse(input[1]);

                        while(!board.CheckFreePos(px, py))
                        {
                            Console.WriteLine("Enter coordinates in format - \"X Y\"");
                            input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            px = int.Parse(input[0]);
                            py = int.Parse(input[1]);
                        }
                        board.Update(playerTurn, px, py);
                        counter++;
                        playerTurn = -1;
                        Console.ResetColor();
                        break;
                    }
                }

                else
                {
                    (int qx, int qy) = Minimax.FindBestMove(board);
                    board.Update(playerTurn, qx + 1, qy + 1);
                    counter++;
                    playerTurn = 1;
                    Console.ResetColor();
                }
            }
        }
    }
}
