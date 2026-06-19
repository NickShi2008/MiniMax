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
        int count = 0;
        //255,168
        float c = 1.5f; //constant for UCB1 formula


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
        public int Minimax(IGameState<T> state, bool isMax, int alpha = int.MinValue, int beta = int.MaxValue)
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

                    //if(alpha != int.MinValue)
                    //{
                    //    val[i] = Minimax(current.children[i].state, false, alpha);
                    //}
                    if (alpha >= beta) break;
                    val[i] = Minimax(current.children[i].state, false, alpha, beta);
                    
                    SetCurrent(state);
                    alpha = Math.Max(alpha, val[i]);
                    count++;
                }
                ;
                //for (int i = 0; i < arrNikitaUlianov123.Length; i++)
                //{
                //    val[i] = Math.Max(Minimax(arr[i], false), val[i]);
                //}
                value = alpha;

                
            }
            else
            {
                for (int i = 0; i < current.children.Length; i++)
                {
                    if (alpha >= beta) break;
                    val[i] = Minimax(current.children[i].state, true, alpha, beta);
                    
                    SetCurrent(state);
                    beta = Math.Min(beta, val[i]);
                    count++;
                }
                ;
                //T[] arr = state.getChildren();
                //for (int i = 0; i < arr.Length; i++)
                //{
                //    val[i] = Math.Min(Minimax(arr[i], true), val[i]);
                //}
                value = beta;
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

        public int w; //wins - losses
        public int n; //number of simulations
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
            else if(state.isTie)
            {
                Value = 0;
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
                throw new Exception("No happen");
            }
            
        }

       


    }
}
