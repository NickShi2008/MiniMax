namespace MiniMax
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TicTacToe ttt = new TicTacToe();

            while(ttt.isGameDone == false)
            {
                ttt.GetMove();
            }

        }

    }
}
