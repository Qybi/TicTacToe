using CLED.TicTacToe.EventModels;
using Spectre.Console;

namespace CLED.TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var n = AnsiConsole.Ask<int>("Input the number of [yellow]games[/]");
            var t = new TicTacToe(n);

            t.PlayerSet += TicTacToe_PlayerSet;
            t.GameEnd += TicTacToe_GameEnd;
            t.Start();
        }

        static void TicTacToe_PlayerSet(object? sender, PlayerSetEventArgs args)
        {
            AnsiConsole.MarkupLine($"Player [green]'{args.PlayerSymbol}'[/] has set its mark on position {args.Position}");
            TableUtilities.DisplayTable(args.Table);
        }
        static void TicTacToe_GameEnd(object? sender, GameEndEventArgs args)
        {
            if (string.IsNullOrEmpty(args.PlayerSymbol))
                AnsiConsole.MarkupLine($"Game [green]{args.GameNumber}[/] has not been won by anyone");
            else
                AnsiConsole.MarkupLine($"Game [green]{args.GameNumber}[/] has been won by {args.PlayerSymbol}");
            TableUtilities.DisplayTable(args.Table);
        }
    }
}
