using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiniMax
{
    public class MiniMaxTree<T> where T : IGameState<T>
    {
        int height = 8;
        public MiniMaxNode<T> current;
        public Stack<MiniMaxNode<T>> stack;
        public MiniMaxNode<T> root => stack.FirstOrDefault() ?? current;

        public MiniMaxTree(IGameState<T> start)
        {
            current = new MiniMaxNode<T>(start, 0);
            stack = new Stack<MiniMaxNode<T>>();
            

        }

        public void SetCurrent(IGameState<T> state)
        {
            for (int i = 0; i < current.children.Length; i++)
            {

                if (current.children[i].state.Equals(state))
                {
                    current = current.children[i];
                    return;
                }
                else if (current.state.Equals(state))
                {
                    return;
                }
            }
            stack.Pop();
            current = stack.Peek();
        }
        public int Minimax(IGameState<T> state, bool isMax)
        {
            //int val = state.Value;
            //MiniMaxNode<T> node = current;
            SetCurrent(state);
            stack.Push(current);


            if (state.isTerminal)
            {
                return current.Value;
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
                
                for (int i = 0; i < current.children.Length; i++)
                {
                    
                    val[i] = Minimax(current.children[i].state, false);
                    SetCurrent(state);
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
                for (int i = 0; i < current.children.Length; i++)
                {
                    
                    val[i] = Minimax(current.children[i].state, true);
                    SetCurrent(state);
                }
                ;
                //T[] arr = state.getChildren();
                //for (int i = 0; i < arr.Length; i++)
                //{
                //    val[i] = Math.Min(Minimax(arr[i], true), val[i]);
                //}
                value = val.Min();
            }
            current.Value = value;
            return value;

        }

    }

    public class MiniMaxNode<T> where T : IGameState<T>
    {
        public IGameState<T> state;
        public MiniMaxNode<T>[] children;
        public int depth;
        public int Value;
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
            if(!state.isTerminal)
            {
                Value = int.MinValue;
            }
            else if (state.isWin)
            {
                Value = 1;
            }
            else if (state.isLoss)
            {
                Value = -1;
            }
            else
            {
                Value = 0;
            }
        }

       


    }
}
