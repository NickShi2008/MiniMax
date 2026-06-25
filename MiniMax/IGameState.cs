using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMax
{
    public interface IGameState<T> : IEquatable<T> where T : IGameState<T>
    {
        bool isWin { get; }
        bool isLoss { get; }
        bool isTerminal { get; }
        bool isTie { get; }
        
        T[] getChildren();
    }
}
