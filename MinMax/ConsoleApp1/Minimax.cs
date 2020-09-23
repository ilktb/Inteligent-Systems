using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Minimax
    {
        // Checks if the game has ended and returns the winner in each case
        // 1 - first player win, -1 - second win, 0 - toe, 2 - there are free spaces
        public static int Is_end(Board board)
        {
            // Vertical win
            for (int i = 0; i < 3; i++)
            {
                if (board.Table[0][i] != 0 &&
                    board.Table[0][i] == board.Table[1][i] &&
                    board.Table[1][i] == board.Table[2][i])
                    return board.Table[0][i];
            }

            // Horizontal win
            for (int i = 0; i < 3; i++)
            {
                if (board.Table[i][0] != 0 &&
                   board.Table[i][0] == board.Table[i][1] &&
                   board.Table[i][1] == board.Table[i][2])
                    return board.Table[i][0];
            }

            // Main diagonal win
            if (board.Table[0][0] != 0 &&
                board.Table[0][0] == board.Table[1][1] &&
                board.Table[0][0] == board.Table[2][2])
                return board.Table[0][0];

            // Second diagonal win
            if (board.Table[0][2] != 0 &&
                board.Table[0][2] == board.Table[1][1] &&
                board.Table[0][2] == board.Table[2][0])
                return board.Table[0][2];

            // Is whole board full?
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // There's an empty field, we continue the game
                    if (board.Table[i][j] == 0)
                        return 2;
                }

            }

            // It's a tie!
            return 0;
        }

        public static int MiniMax(Board board, int depth, bool isMax, int alpha, int beta)
        {
            
            int result = Is_end(board);

            // If the game came to an end, the function needs to return
            // the evaluation function of the end. That can be:
            // -1 - loss
            // 0  - a tie
            // 1  - win

            if (result == 1)
                return 1;
            else if (result == -1)
                return -1;
            else if (result == 0)
                return 0;

            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {

                        if (board.Table[i][j] == 0)
                        {
                            // On the empty field player 'O' makes a move and calls Min
                            // That's one branch of the game tree.
                            board.Table[i][j] = 1;
                            int val = Math.Max(best, MiniMax(board, depth + 1, !isMax, alpha, beta));
                            best = Math.Max(best, val);
                            alpha = Math.Max(alpha, best);

                            // Setting back the field to empty
                            board.Table[i][j] = 0;

                            //Alpha Beta Prunning
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return best;
            }

            else
            {
                int best = 1000;

                // Traverse all cells 
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty 
                        if (board.Table[i][j] == 0)
                        {
                            // Make the move 
                            board.Table[i][j] = -1;

                            // Call minimax recursively and choose 
                            // the minimum value 
                            int val = Math.Min(best, MiniMax(board, depth + 1, isMax, alpha, beta));
                            best = Math.Min(val, best);
                            beta = Math.Min(beta, best);

                            // Undo the move 
                            board.Table[i][j] = 0;

                            //Alpha Beta Pruning
                            if (beta <= alpha)
                                break;
                         }
                    }
                }
                return best;
            }
        }

        public static (int x, int y) FindBestMove(Board board)
        {

            int bestVal = -1000;
            (int x, int y) bestMove;
            bestMove.x = -1;
            bestMove.y = -1;

            // Traverse all cells, evaluate minimax function for 
            // all empty cells. And return the cell with optimal 
            // value. 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board.Table[i][j] == 0)
                    {
                        // Make the move 
                        board.Table[i][j] = 1;

                        // compute evaluation function for this 
                        // move. 
                        int moveVal = MiniMax(board, 0, false, -1000, 1000);

                        // Undo the move 
                        board.Table[i][j] = 0;

                        // If the value of the current move is 
                        // more than the best value, then update 
                        // best/ 
                        if (moveVal > bestVal)
                        {
                            bestMove.x = i;
                            bestMove.y = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }

            //Console.WriteLine($"The value of the best Move is: {bestVal}");

            return bestMove;
        }
    }
}
