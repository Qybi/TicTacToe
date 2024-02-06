using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.TicTacToe.EventModels
{
    internal class GameEndEventArgs
    {
        public GameEndEventArgs(string playerSymbol, string[,] table, int gameNumber)
        {
            PlayerSymbol = playerSymbol;
            Table = table;
            GameNumber = gameNumber;
        }

        public string PlayerSymbol { get; private set; }
        public string[,] Table { get; private set; }
        public int GameNumber { get; set; }
    }
}
