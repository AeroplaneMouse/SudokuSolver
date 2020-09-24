using SudokuSolver.Models;
namespace SudokuSolver.UI
{
    public delegate void NotifyOption(int op);
    public delegate void NotifyBoard(SudokuBoard board);

    public interface ISudokuUI
    {
        string LastMessage { get; set; }

        event NotifyOption ReceivedOption;
        event NotifyBoard ReceivedBoard;

        void Start();
        void Stop();
        int GetOption();
        SudokuBoard GetBoard();

        void DisplayBoardFancy(SudokuBoard board);
        void DisplayBoardSimple(SudokuBoard board);
        void DisplayStats(Solver solver);
    }
}
