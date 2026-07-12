using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml;
using System.Linq;

namespace MiniMax
{
    public class MCTree<T> where T : IGameState<T>
    {
        private MCTSNode<T> root;

        public MCTree(T start)
        {
            root = new MCTSNode<T>(start, null);

        }

        public void SetCurrent(T state)
        {

            root.GenerateChildren();
            for (int i = 0; i < root.children.Length; i++)
            {
                if (root.children[i].state.Equals(state))
                {
                    root = root.children[i];
                    return;
                }
                else if (root.state.Equals(state))
                {
                    return;
                }
            }
            root = new MCTSNode<T>(state);
        }

        public static T MCTS(int iterations, T startingState, Random random, bool isPlayerOne)
        {
            MCTSNode<T> node = new MCTSNode<T>(startingState);
            //root.GenerateChildren();
            for(int i = 0; i < iterations; i++)
            {
                MCTSNode<T> selectedNode = Select(node);
                var current = Expand(selectedNode, random);
                var backProp = current;
                int value = Simulate(random, current, out backProp, isPlayerOne);
                Backpropagate(backProp, value);

            }
            //current problem going 5 to 1 the bot doen't block the win all ame run and win?
            //maybe don't explore every node?
            //try by number not win?
            //var sortedChildren = isPlayerOne ? node.children.OrderByDescending((state) => (state.w)) : node.children.OrderBy((state) => (state.w));
            //var sortedChildren = isPlayerOne ? node.children.OrderByDescending((state) => (state.n)) : node.children.OrderBy((state) => (state.n));
            //var sortedChildren = node.children.OrderByDescending((state) => (state.n));
            // Choose the most-visited child (robust final selection after simulations)
            var sortedChildren = node.children.OrderByDescending(c => c.n);
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

        static MCTSNode<T> Expand(MCTSNode<T> node, Random random)
        {
            node.GenerateChildren();
            node.isExpanded = true;

            if (node.children.Length == 0) return node;

            // Prefer unvisited children chosen uniformly at random to avoid index bias.
            var unvisited = node.children.Where(c => c.n == 0).ToArray();
            if (unvisited.Length > 0)
            {
                return unvisited[random.Next(unvisited.Length)];
            }

            // All children visited: pick one at random to continue exploration.
            return node.children[random.Next(0, node.children.Length)];
        }
           

        static int Simulate(Random random, MCTSNode<T> current, out MCTSNode<T> node, bool isPlayerOne)
        {
            while(!current.state.isTerminal)
            {
                current.GenerateChildren();
                int randomIndex = random.Next(0, current.children.Length);
                current = current.children[randomIndex];
            }
            node = current;
            int value;
            //xxo
            //oxx
            //xoo
            if (current.state.isWin) value = 1;
            else if (current.state.isLoss) value = -1;
            else value = 0;
            return isPlayerOne ? value : -value;
        }

        static void Backpropagate(MCTSNode<T> node, int value)
        {
            MCTSNode<T> current = node;
            while (current != null)
            {
               
                current.n++;
                current.w += value;
                value = -value; //check whether should after switch of parent
                current = current.parent;
                
            }
        }

    }

    public class MCTSNode<T> where T : IGameState<T>
    {
        public T state;
        public MCTSNode<T>[] children;
        public MCTSNode<T> parent;

        public bool isExpanded = false;
        //public bool isExpanded => children != null && children.Length > 0;

        public double w = 0; //wins - losses
        public double n = 0; //number of simulations
        private double c = 1.5; //constant for UCB1 formula
        public MCTSNode(T state, MCTSNode<T> parent = null)
        {
            this.state = state;
            this.parent = parent;
            
        }

        public void GenerateChildren()
        {
            if (children != null)
                return;

            T[] test = state.getChildren();
            children = new MCTSNode<T>[test.Length];
            for(int i = 0; i < test.Length; i++)
            {
                children[i] = new MCTSNode<T>(test[i], this);
            }
        }

        public double UCT()
        {
            if (n == 0) return double.PositiveInfinity;
            return w / n + c * Math.Sqrt(Math.Log(parent.n) / n);
        }




    }
}
