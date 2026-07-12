using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiniMax
{
    public class GameState : IGameState<GameState>
    {
        //public int Value { get; set; }
        public bool isWin { get; }
        public bool isLoss { get; }
        public bool isTerminal { get; }
        public bool isTie { get; }
        public TicTacToe game;
        //public bool isPlayerOne;
        


        public GameState(TicTacToe game)
        {

            this.game = game;
            isWin = !game.isPlayerOneTurn && game.isGameDone;
            isLoss = game.isPlayerOneTurn && game.isGameDone;
            isTie = game.movesMade >= 9 && !isWin && !isLoss;
            isTerminal = game.isGameDone || isTie;

        }

        public bool Equals(GameState other)
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (game.board[i][j] != other.game.board[i][j])
                        return false;
                }
            }

            return true;
        }

        GameState[] IGameState<GameState>.getChildren()
        {
            List<GameState> children = new();
            if (isTerminal) return children.ToArray();

            for (int i = 1; i <= 9; i++)
            {
                TicTacToe newGame = new TicTacToe(game);

                if (!newGame.CanMakeMove(newGame.GetMoveFromIndex(i)))
                    continue;

                children.Add(new GameState(newGame));
            }

            return children.ToArray();
        }
    }


public class TicTacToe
{
        public string[][] board;
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
        }

        public TicTacToe(TicTacToe game)
        {
            //this.board = game.GetBoard();
            board = new string[3][];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new string[3];
                for(int j = 0; j < board.Length; j++)
                    board[i][j] = game.GetBoard()[i][j];
            }
           this.keyBoard = game.keyBoard;
           isPlayerOneTurn = game.isPlayerOneTurn;
            movesMade = game.movesMade;
            isGameDone = game.isGameDone;
        }

        public void GetMove(out int result)
        {
            string input = "";
            
            Console.WriteLine();
            //if (isPlayerOneTurn) Console.WriteLine("Player 1's turn");
            //else Console.WriteLine("Player 2's turn");
            PrintBoard();
            PrintKeyBoard();


            do
            {
                result = -1;
                while (result == -1)
                {

                    input = Console.ReadLine();
                    
                    if (!int.TryParse(input, out result))
                    {

                        Console.WriteLine("Please enter a number");
                        result = -1;
                    }
                    else if (result < 1 || result > 9)
                    {
                        Console.WriteLine("Please enter a number between 1 and 9");
                        result = -1;
                    }

                }
            } while (!CanMakeMove(GetMoveFromIndex(result)));
        }
        public void CompMove(int result)
        {
            Console.WriteLine();
            int[] index = GetMoveFromIndex(result);
            CanMakeMove(index);
        }

        public void CompMove(int[] index)
        {
            Console.WriteLine();
            CanMakeMove(index);
        }

        public int[] GetMoveFromIndex(int index)
        {
            int row = (index - 1) / 3;
            int col = (index - 1) % 3;
            return new int[] { row, col };
        }


        public bool CanMakeMove(int[] index)
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
                
                isPlayerOneTurn = !isPlayerOneTurn;
                movesMade++;
                isGameDone = IsFinished();
                
                return true;
            }

            return false;
        }

        private bool IsFinished()
        {
            for(int i = 0; i < board.Length; i++)
            {
                if (CheckDir(new int[] { 0, 1 }, [i, 0]))
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
                int x = start[0] + dir[0] * i;
                int y = start[1] + dir[1] * i;
                if (!board[x][y].Equals(board[start[0]][start[1]]))
                    return false;
            }
            return true;
        }

        private bool CheckDiag()
        {
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

        public void PrintBoard()
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
