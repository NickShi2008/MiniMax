namespace MiniMax
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            bool lastPlayerOne = false;

            //MiniMaxTree<GameState> tree = new MiniMaxTree<GameState>(new GameState(new TicTacToe())); 
            MCTree<GameState> mcTree = new MCTree<GameState>(new GameState(new TicTacToe()));
            mcTree.SetCurrent(new GameState(new TicTacToe()));
            //TicTacToe bleh = new TicTacToe();
            //int it;
            //while(true)
            //{
            //    bleh.GetMove(out it);
            //}
            Random random = new Random();

            string stay = "y";
            while (stay == "y")
            {
                
                TicTacToe ttt = new TicTacToe();
                List<int> openIndices = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    openIndices.Add(i + 1);
                }
                GameState state = new GameState(ttt);
                string input = "";
                int result = 0;
                Console.WriteLine();
                while (result == 0)
                {
                    Console.WriteLine("Choose 1 for Player 1 and 2 for Player 2");
                    input = Console.ReadLine();
                    int.TryParse(input, out result);
                    if (result == 1 || result == 2)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Please enter a number of  1 or  2");
                        result = 0;
                    }

                }

                lastPlayerOne = ttt.isPlayerOneTurn;
                //tree.current = tree.root;
                //tree.Minimax(tree.current.state, true);

                int moveMade = 0;
                while (ttt.isGameDone == false)
                {
                    //if (ttt.isPlayerOneTurn)
                    //{
                    //    for(int i = 0; i < tree.current.children.Length; i++)
                    //    {
                    //        Console.WriteLine($"{i + 1}: {tree.current.children[i].Value}");
                    //    }
                    //}
                    //mcTree.SetCurrent(state);
                    if ((result == 1 && ttt.isPlayerOneTurn) || (result == 2 && ttt.isPlayerOneTurn == false))
                    {
                        ttt.GetMove(out moveMade);
                    }
                    else
                    {
                        mcTree.SetCurrent(state);
                        if (openIndices.Count == 0)
                        {
                            break;
                        }
                        
                        int moveVal;
                        int index = 0;
                        bool isPlayerOne = result == 2;
                        GameState mcts =  MCTree<GameState>.MCTS(1600, state, random, isPlayerOne);
                        bool doubleBreak = false;
                        for(int i = 0; i < mcts.game.board.Length; i++)
                        {
                            for (int j = 0; j < mcts.game.board[i].Length; j++)
                            {
                                if (mcts.game.board[i][j] != ttt.board[i][j])
                                {
                                    ttt.CompMove([i, j]);
                                    moveMade = i * 3 + j + 1;
                                    doubleBreak = true;
                                    break;
                                }
                            }
                            if (doubleBreak) break;
                        }
                        

                        //if (ttt.isPlayerOneTurn)
                        //{
                        //    //moveVal = int.MinValue;
                        //    //if (tree.current.Value == int.MinValue) 
                        //    //    tree.Minimax(tree.current.state, true);
                        //    //for (int i = 0; i < tree.current.children.Length; i++)
                        //    //{
                        //    //    if (tree.current.children[i].Value > moveVal)
                        //    //    {
                        //    //        index = i;
                        //    //        moveVal = tree.current.children[i].Value;
                        //    //    }
                        //    //}
                            
                            
                        //}
                        //else
                        //{
                        //    //moveVal = int.MaxValue;
                        //    //if (tree.current.Value == int.MinValue) 
                        //    //    tree.Minimax(tree.current.state, false);
                        //    //for (int i = 0; i < tree.current.children.Length; i++)
                        //    //{
                        //    //    if (tree.current.children[i].Value < moveVal && tree.current.children[i].Value != int.MinValue)
                        //    //    {
                        //    //        index = i;
                        //    //        moveVal = tree.current.children[i].Value;
                        //    //    }
                        //    //}
                        //}
                        //if (openIndices.Count > 0)
                        //{
                        //    moveMade = openIndices[index];

                        //    ttt.CompMove(moveMade);
                        //}
                        //else
                        //{
                        //    break;
                        //}
                    }
                    bool hasFound = false;
                    for (int i = 0; i < openIndices.Count; i++)
                    {
                        if (openIndices[i] == moveMade)
                        {
                            moveMade = i;
                            openIndices.RemoveAt(i);
                            hasFound = true;
                            break;
                        }
                    }
                    if (!hasFound) throw new Exception("Indice wrong");
                    state = new GameState(ttt);
                    //tree.current = tree.current.children[moveMade];
                }
                ttt.PrintBoard();
                Console.WriteLine("Play Again? (y/n)");
                while (true)
                {
                    //Console.WriteLine("Enter Below: ");
                    stay = Console.ReadLine();
                    if (stay == "y" || stay == "n") break;
                }
               
            }
        }

    }
}
