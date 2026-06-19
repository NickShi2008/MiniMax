namespace MiniMax
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            bool lastPlayerOne = false;

            MiniMaxTree<GameState> tree = new MiniMaxTree<GameState>(new GameState(new TicTacToe())); ;
            
            ;
            string stay = "y";
            while (stay == "y")
            {
                
                TicTacToe ttt = new TicTacToe();
                List<int> openIndices = new List<int>();
                for (int i = 0; i < 9; i++)
                {
                    openIndices.Add(i + 1);
                }
                //GameState state = new GameState(ttt, true);
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
                //tree = new MiniMaxTree<GameState>(new GameState(new TicTacToe(), !ttt.isPlayerOneTurn));
                lastPlayerOne = ttt.isPlayerOneTurn;
                tree.current = tree.root;
                //tree.Minimax(tree.current.state, result != 1);
                tree.Minimax(tree.current.state, true);
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
                    if (result == 1 && ttt.isPlayerOneTurn || result == 2 && ttt.isPlayerOneTurn == false)
                    {
                        ttt.GetMove(out moveMade);
                    }
                    else
                    {
                        int moveVal;
                        int index = 0;
                        if (ttt.isPlayerOneTurn)
                        {
                            moveVal = int.MinValue;
                            if (tree.current.Value == int.MinValue) 
                                tree.Minimax(tree.current.state, true);
                            for (int i = 0; i < tree.current.children.Length; i++)
                            {
                                if (tree.current.children[i].Value > moveVal)
                                {
                                    index = i;
                                    moveVal = tree.current.children[i].Value;
                                }
                            }

                        }
                        else
                        {
                            moveVal = int.MaxValue;
                            if (tree.current.Value == int.MinValue) 
                                tree.Minimax(tree.current.state, false);
                            for (int i = 0; i < tree.current.children.Length; i++)
                            {
                                if (tree.current.children[i].Value < moveVal && tree.current.children[i].Value != int.MinValue)
                                {
                                    index = i;
                                    moveVal = tree.current.children[i].Value;
                                }
                            }
                        }
                        if (openIndices.Count > 0)
                        {
                            moveMade = openIndices[index];

                            ttt.CompMove(moveMade);
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 0; i < openIndices.Count; i++)
                    {
                        if (openIndices[i] == moveMade)
                        {
                            moveMade = i;
                            openIndices.RemoveAt(i);
                            break;
                        }
                    }
                    state = new GameState(ttt);
                    tree.current = tree.current.children[moveMade];
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
