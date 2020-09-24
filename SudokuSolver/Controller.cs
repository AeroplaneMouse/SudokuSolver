using SudokuSolver.UI;
using SudokuSolver.Models;
using System.Diagnostics;

namespace SudokuSolver
{
    public class Controller
    {
        ISudokuUI UI { get; set; }
        Core Core { get; set; }

        public Controller(ISudokuUI ui, Core c)
        {
            UI = ui;
            Core = c;

            // Setup subscriptions
            UI.ReceivedOption += OptionHandler;
            UI.ReceivedBoard += BoardHandler;

            UI.Start();
        }

        public void OptionHandler(int op)
        {
            if (op == -2)
                UI.LastMessage = "Option MUST be an integer";
            else if (op == -1)
                UI.Stop();
            else if (op == 0)
                UI.GetBoard();
            else if (op > 0 && op <= Core.Boards.Count)
            {
                SudokuBoard b = Core.GetBoardByIndex(op - 1);

                if (b != null)
                {
                    UI.LastMessage = $"Solving board '{ b.Label }'";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Reset();
                    
                    Solver s = Solve(b, stopwatch);
                    UI.LastMessage = $"Board '{ b.Label } has been solved";
                    UI.DisplayBoardFancy(b);
                    UI.DisplayStats(s);
                }
                else
                    UI.LastMessage = $"Error finding board '{ op }'";
            }
            else
                UI.LastMessage = "Invalid option";

        }

        public void BoardHandler(SudokuBoard board)
        {
            Core.AddBoard(board);
        }

        private static Solver Solve(SudokuBoard board, Stopwatch stopwatch)
        {
            // Setup
            Solver solver = new Solver(board);
            
            // Solve board
            stopwatch.Start();
            solver.Run();
            stopwatch.Stop();

            return solver;
        }
    }
}
