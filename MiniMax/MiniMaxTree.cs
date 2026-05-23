using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMax
{
    public class MiniMaxTree<T> where T : IGameState<T>
    {
        int height = 8;
        MiniMaxNode<T> root;
        public MiniMaxTree(IGameState<T> start)
        {
            root = new MiniMaxNode<T>(start, 0);
        }
    }

    public class MiniMaxNode<T> where T : IGameState<T>
    {
        public IGameState<T> state;
        public MiniMaxNode<T>[] children;
        int depth;
        public MiniMaxNode(IGameState<T> state, int depth)
        {
            this.state = state;
            this.depth = depth;
            GameState[] children = state.getChildren();
            for (
            children = 
        }

        public int Minimax(IGameState<T> state, bool isMax)
        {
            if (state.isTerminal)
            {
                if (state.isWin)
                {
                    return 1;
                }
                else if (state.isLoss)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            if (isMax)
            {
                for (int i = 0; i < children.Count; i++)
                {
                   Minimax(children[i].state, false);
                }
            }
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    Minimax(children[i].state, true);
                }
            }

        }


    }
}
