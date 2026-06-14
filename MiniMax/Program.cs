namespace MiniMax
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            
            MiniMaxTree<GameState> tree = new MiniMaxTree<GameState>(new GameState(new TicTacToe()));
            
            ;
            string stay = "y";
            while (stay == "y")
            {
                tree.current = tree.root;
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
                tree.Minimax(tree.current.state, result != 1);
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
                            for (int i = 0; i < tree.current.children.Length; i++)
                            {
                                if (tree.current.children[i].Value < moveVal)
                                {
                                    index = i;
                                    moveVal = tree.current.children[i].Value;
                                }
                            }
                        }
                        moveMade = openIndices[index];

                        ttt.CompMove(moveMade);
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
