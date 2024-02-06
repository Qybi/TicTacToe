using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.TicTacToe
{
    internal static class TableUtilities
    {
        public static void DisplayTable(string[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                AnsiConsole.Markup($"{grid[i, 0] ?? " "} | {grid[i, 1] ?? " "} | {grid[i, 2] ?? " "}");
                AnsiConsole.WriteLine();
            }
        }
        public static void DisplayTemplateTable(string[,] grid)
        {
            int baseCounter = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                AnsiConsole.Markup($"[yellow]{baseCounter + 1} | {baseCounter + 2} | {baseCounter + 3}[/]");
                AnsiConsole.WriteLine();
                baseCounter += 3;
            }
        }
    }
}
