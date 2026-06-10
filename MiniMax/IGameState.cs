using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMax
{
    public interface IGameState<T> where T : IGameState<T>
    {
        int Value { get; set; }
        bool isWin { get; }
        bool isLoss { get; }
        bool isTerminal { get; }
        bool isTie { get; }
        T[] getChildren();
    }
}
