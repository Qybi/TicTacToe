using CLED.TicTacToe.EventModels;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLED.TicTacToe
{
    internal class TicTacToe
    {
        public event EventHandler<PlayerSetEventArgs> PlayerSet;
        public event EventHandler<GameEndEventArgs> GameEnd;

        private readonly int _gamesCount;
        public string[,] Grid { get; private set; }
        public TicTacToe(int gamesCount)
        {
            _gamesCount = gamesCount;
            Grid = new string[3, 3];
        }
        public void Start()
        {
            for (int i = 0; i < _gamesCount; i++)
            {
                Match(i);
            }
        }
        private void Match(int matchNumber)
        {
            Random rnd = new Random();
            var player = rnd.Next(0, 1) == 0 ? "X" : "O";
            AnsiConsole.MarkupLine($"Starting match {matchNumber + 1}, starting player is [green]{player}[/]");
            TableUtilities.DisplayTemplateTable(Grid);
            for (int i = 0; i < 9; i++)
            {
                int pos = AnsiConsole.Ask<int>($"Player [green]{player}[/] Insert the number of the cell you wish to put your mark");
                (int x, int y) coordinates;
                bool answer = SolvePosition(pos, out coordinates);
                while (!answer)
                {
                    AnsiConsole.WriteLine("Position has already been filled");
                    pos = AnsiConsole.Ask<int>($"Player [green]{player}[/] Insert the number of the cell you wish to put your mark");
                    answer = SolvePosition(pos, out coordinates);
                }

                Grid[coordinates.x, coordinates.y] = player;
                OnPlayerSet(player, pos);
                string winner = CheckWin();
                if (!string.IsNullOrEmpty(winner))
                {
                    OnGameEnd(winner, matchNumber);
                    break;
                }
                else if (string.IsNullOrEmpty(winner) && i == 8) OnGameEnd(winner, matchNumber);

                player = player == "X" ? "O" : "X";
            }
        }

        private string CheckWin()
        {
            // righe uguali
            if (Grid[0, 0] == Grid[0, 1] && Grid[0, 1] == Grid[0, 2] && Grid[0, 0] == Grid[0, 2]) return Grid[0, 0];
            if (Grid[1, 0] == Grid[1, 1] && Grid[1, 1] == Grid[1, 2] && Grid[1, 0] == Grid[1, 2]) return Grid[1, 1];
            if (Grid[2, 0] == Grid[2, 1] && Grid[2, 1] == Grid[2, 2] && Grid[2, 0] == Grid[2, 2]) return Grid[2, 2];
            // colonne uguali
            if (Grid[0, 0] == Grid[1, 0] && Grid[0, 0] == Grid[2, 0] && Grid[1, 0] == Grid[2, 0]) return Grid[0, 0];
            if (Grid[0, 1] == Grid[1, 1] && Grid[1, 1] == Grid[2, 1] && Grid[0, 1] == Grid[2, 1]) return Grid[1, 1];
            if (Grid[0, 2] == Grid[1, 2] && Grid[1, 2] == Grid[2, 2] && Grid[0, 2] == Grid[2, 2]) return Grid[2, 2];
            // diagonali
            if (Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2] && Grid[0, 0] == Grid[2, 2]) return Grid[1, 1];
            if (Grid[0, 2] == Grid[1, 1] && Grid[1, 1] == Grid[2, 0] && Grid[0, 2] == Grid[2, 0]) return Grid[1, 1];
            return string.Empty;
        }

        private bool SolvePosition(int pos, out (int, int) coordinates)
        {
            (int x, int y) coords;
            coordinates = (-1, -1);
            switch (pos)
            {
                case 1: coords = (0, 0); break;
                case 2: coords = (0, 1); break;
                case 3: coords = (0, 2); break;
                case 4: coords = (1, 0); break;
                case 5: coords = (1, 1); break;
                case 6: coords = (1, 2); break;
                case 7: coords = (2, 0); break;
                case 8: coords = (2, 1); break;
                case 9: coords = (2, 2); break;
                default:
                    coords = (0, 0);
                    break;
            }
            if (!string.IsNullOrEmpty(Grid[coords.x, coords.y])) return false;
            else
            {
                coordinates = (coords.x, coords.y);
                return true;
            }

        }
        private void OnPlayerSet(string player, int position)
        {
            PlayerSet?.Invoke(this, new PlayerSetEventArgs(player, Grid, position));
        }
        private void OnGameEnd(string player, int gameNumber)
        {
            GameEnd?.Invoke(this, new GameEndEventArgs(player, Grid, gameNumber));
        }
    }
}
