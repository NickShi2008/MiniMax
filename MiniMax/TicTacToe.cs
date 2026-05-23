using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMax
{
    public class GameState : IGameState<TicTacToe>
    {
        public bool isWin { get; }
        public bool isLoss { get; }
        public bool isTerminal { get; }
        public bool isTie { get; }
        public TicTacToe game;

        public GameState(TicTacToe game)
        {
            this.game = game;
            isWin = !game.isPlayerOneTurn && game.isGameDone;
            isLoss = game.isPlayerOneTurn && game.isGameDone;
            isTerminal = game.isGameDone;
            isTie = game.movesMade >= 9 && !isWin && !isLoss;
        }
        // returnn array of tictacotoe game state
        public TicTacToe[] getChildren()
        {
            GameState[] children = new GameState[9 - game.movesMade];
            int count = 0;
            for(int i = 1; i <= 9; i++)
            {
                TicTacToe newGame = new TicTacToe(game.GetBoard());
                newGame.GetMoveFromIndex(i);
                children[count] = new GameState(newGame);
                count++;
                if (count >= children.Length) break;
            }
            return children;
        }
    }

    public class TicTacToe
    {
        string[][] board;
        string[][] keyBoard;
        public bool isPlayerOneTurn = true;
        public bool isGameDone = false;
        public int movesMade = 0;


        public TicTacToe()
        {
            board = new string[3][]
            {
                [ "?", "?", "?" ],
                [ "?", "?", "?" ],
                [ "?", "?", "?" ]
            };

            keyBoard = new string[3][]
            {
                [ "1", "2", "3" ],
                [ "4", "5", "6" ],
                [ "7", "8", "9" ]
            };

            PrintBoard();

        }

        public TicTacToe(string[][] board)
        {
            this.board = board;

            keyBoard = new string[3][]
            {
                [ "1", "2", "3" ],
                [ "4", "5", "6" ],
                [ "7", "8", "9" ]
            };

            PrintBoard();

        }

        public void GetMove()
        {
            string input = "";
            int result = 0;
            PrintKeyBoard();
            if(isPlayerOneTurn) Console.WriteLine("Player 1's turn");
            else Console.WriteLine("Player 2's turn");
            while (result == 0)
            {

                input = Console.ReadLine();
                int.TryParse(input, out result);
                if (result == 0)
                {

                    Console.WriteLine("Please enter a number");
                }
                else if (result < 1 || result > 9)
                {
                    Console.WriteLine("Please enter a number between 1 and 9");
                    result = 0;
                }
            }
            CanMakeMove(GetMoveFromIndex(result));
        }

        public int[] GetMoveFromIndex(int index)
        {
            int row = (index - 1) / 3;
            int col = (index - 1) % 3;
            return new int[] { row, col };
        }


        private bool CanMakeMove(int[] index)
        {
            if (board[index[0]][index[1]] == "?")
            {
                if (isPlayerOneTurn)
                {
                    board[index[0]][index[1]] = "X";
                }
                else
                {
                    board[index[0]][index[1]] = "O";
                   
                }
                PrintBoard();
                
                isPlayerOneTurn = !isPlayerOneTurn;
                movesMade++;
                isGameDone = IsFinished(index) || movesMade >= 9;
                return true;
            }

            return false;
        }

        private bool IsFinished(int[] index)
        {
            for(int i = 0; i < board.Length; i++)
            {
                if (CheckDir(new int[] { 0, 1 }, new int[] { i, 0 }))
                    return true;
                if (CheckDir(new int[] { 1, 0 }, new int[] { 0, i }))
                    return true;
            }
            return CheckDiag();
        }

        private bool CheckDir(int[] dir, int[] start)
        {
            if (board[start[0]][start[1]] == "?") return false;
            for (int i = 0; i < 3; i++)
            {
                int x = dir[0] * i;
                int y = dir[1] * i;
                if (!board[x][y].Equals(board[start[0]][start[1]]))
                    return false;
            }
            return true;
        }

        private bool CheckDiag()
        {
            int x = 1;
            int y = 1;
            if (board[1][1] == "?") return false;
            if(board[0][0].Equals(board[1][1]) && board[1][1].Equals(board[2][2]))
            {
                return true;
            }

            if (board[0][2].Equals(board[1][1]) && board[1][1].Equals(board[2][0]))
            {
                return true;
            }
            return false;
        }

        private void PrintBoard()
        {
            for(int i = 0; i < board.Length; i++)
            {
                for(int j = 0; j < board[i].Length; j++)
                    Console.Write(board[i][j] + " ");

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintKeyBoard()
        {
            for (int i = 0; i < keyBoard.Length; i++)
            {
                for (int j = 0; j < keyBoard[i].Length; j++)
                    Console.Write(keyBoard[i][j] + " ");

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public string[][] GetBoard()
        {
            return board;
        }


    }
}
