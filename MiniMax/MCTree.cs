using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml;

namespace MiniMax
{
    public class MCTree<T> where T : IGameState<T>
    {
        public static MCTSNode<T> current;

        public MCTree(T start)
        {
            current = new MCTSNode<T>(start, null);

        }

        public static T MCTS(int iterations, T startingState, Random random, bool isPlayerOne)
        {
            MCTSNode<T> root = new MCTSNode<T>(startingState);
            //root.GenerateChildren();
            for(int i = 0; i < iterations; i++)
            {
                MCTSNode<T> selectedNode = Select(root);
                current = Expand(selectedNode);
                int value = Simulate(random);
                Backpropagate(current, value);

            }

            var sortedChildren = isPlayerOne ? root.children.OrderByDescending((state) => (state.w/state.n)) : root.children.OrderBy((state) => (state.w / state.n));
            var topChild = sortedChildren.First();
            return topChild.state;

        }

        static MCTSNode<T> Select(MCTSNode<T> node)
        {
            MCTSNode<T> current = node;
            
            while (current.isExpanded)
            {
                MCTSNode<T> best = null;
                double highestUCT = double.NegativeInfinity;
                foreach (var child in current.children)
                {
                    double val = child.UCT();
                    if(val > highestUCT)
                    {
                        highestUCT = val;
                        best = child;
                    }
                }
                if (best == null) break;

                current = best;
            }

            return current;
        }

        static MCTSNode<T> Expand(MCTSNode<T> node)
        {
            node.GenerateChildren();

            if (node.children.Length == 0) return node;
            return node.children[0];
        }

        static int Simulate(Random random)
        {
            while(!current.state.isTerminal)
            {
                current.GenerateChildren();
                int randomIndex = random.Next(0, current.children.Length);
                current = current.children[randomIndex];
            }

            if (current.state.isWin) return 1;
            else if (current.state.isLoss) return -1;
            else return 0;
        }

        static void Backpropagate(MCTSNode<T> node, int value)
        {
            MCTSNode<T> current = node;
            while (current != null)
            {
                value = -value; //check whether should after switch of parent
                current.n++;
                current.w += value;
                current = current.parent;
                
            }
        }

    }

    public class MCTSNode<T> where T : IGameState<T>
    {
        public T state;
        public MCTSNode<T>[] children;
        public MCTSNode<T> parent;

        public bool isExpanded => children != null && children.Length > 0;

        public double w = 0; //wins - losses
        public double n = 0; //number of simulations
        private double c = 2.5; //constant for UCB1 formula
        public MCTSNode(T state, MCTSNode<T> parent = null)
        {
            this.state = state;
            this.parent = parent;
            
        }

        public void GenerateChildren()
        {
            T[] test = state.getChildren();
            children = new MCTSNode<T>[test.Length];
            for(int i = 0; i < test.Length; i++)
            {
                children[i] = new MCTSNode<T>(test[i], this);
            }
        }

        public double UCT()
        {
            if (n == 0) return int.MaxValue;
            return w / n + c * Math.Sqrt(Math.Log(parent.n) / n);
        }




    }
}
