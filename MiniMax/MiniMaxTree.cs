using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMax
{
    public class MiniMaxTree<T> where T : IGameState<T>
    {
        int height = 8;
        public MiniMaxNode<T> root;
        public MiniMaxNode<T> current;
        public MiniMaxTree(IGameState<T> start)
        {
            root = new MiniMaxNode<T>(start, 0);
            current = new MiniMaxNode<T>(start, 0);
        }
        public int Minimax(IGameState<T> state, bool isMax)
        {
            //int val = state.Value;
            //MiniMaxNode<T> node = current;


            if (state.isTerminal)
            {
                return state.Value;
            }

            //T[] arr = state.getChildren();
            //T[] test = state.getChildren();
            //MiniMaxNode<T>[] children;
            //children = new MiniMaxNode<T>[arr.Length];
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    children[i] = new MiniMaxNode<T>(arr[i], current.depth + 1);
            //}
            int[] val = new int[current.children.Length];
            int value;
            if (isMax)
            {
                
                for (int i = 0; i < val.Length; i++)
                {
                    //current = current.children[i];
                    MiniMaxNode<T> node = current.children[i];
                    val[i] = Math.Max(Minimax(current.children[i].state, false), val[i]);
                }
                ;
                //for (int i = 0; i < arrNikitaUlianov123.Length; i++)
                //{
                //    val[i] = Math.Max(Minimax(arr[i], false), val[i]);
                //}
                value = val.Max();
            }
            else
            {
                for (int i = 0; i < val.Length; i++)
                {
                    current = current.children[i];
                    val[i] = Math.Min(Minimax(current.children[i].state, true), val[i]);
                }
                ;
                //T[] arr = state.getChildren();
                //for (int i = 0; i < arr.Length; i++)
                //{
                //    val[i] = Math.Min(Minimax(arr[i], true), val[i]);
                //}
                value = val.Min();
            }
            state.Value = value;
            return value;

        }

    }

    public class MiniMaxNode<T> where T : IGameState<T>
    {
        public IGameState<T> state;
        public MiniMaxNode<T>[] children;
        public int depth;
        public MiniMaxNode(IGameState<T> state, int depth)
        {
            this.state = state;
            this.depth = depth;
            T[] test = state.getChildren();
            children = new MiniMaxNode<T>[test.Length];
            for (int i = 0; i < test.Length; i++)
            {
                children[i] = new MiniMaxNode<T>(test[i], depth + 1);
            }
            ;
        }

       


    }
}
