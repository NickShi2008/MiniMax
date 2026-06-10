namespace MiniMax
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TicTacToe ttt = new TicTacToe();
            GameState state = new GameState(ttt, 0);
            MiniMaxTree<GameState> tree = new MiniMaxTree<GameState>(state);
            tree.Minimax(tree.current.state, true);
            ;

            while (ttt.isGameDone == false)
            {
                ttt.GetMove();
                for(int i = 0; i < tree.current.children.Length; i++)
                {
                    if (tree.current.children[i].state == ttt)
                        tree.current = tree.current.children[i];
                }
            }

        }

    }
}
