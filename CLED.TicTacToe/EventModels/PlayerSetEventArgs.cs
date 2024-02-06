using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.TicTacToe.EventModels
{
    internal class PlayerSetEventArgs
    {
        public PlayerSetEventArgs(string playerSymbol, string[,] table, int position)
        {
            PlayerSymbol = playerSymbol;
            Table = table;
            Position = position;
        }

        public string PlayerSymbol { get; private set; }
        public string[,] Table { get; private set; }

        public int Position { get; private set; }
    }
}
