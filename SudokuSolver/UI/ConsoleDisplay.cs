using System;
using SudokuSolver.Models;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.UI
{
    public class ConsoleDisplay : ISudokuUI
    {
        const int LOG_L_START = 7;
        const int LOG_T_START = 20;
        const int LIST_SIZE = 15;
        const int LIST_WIDTH = 50;
        const int LIST_START = 0;
        const int LIST_END = 21;

        string _lastMessage = string.Empty;

        public event NotifyOption ReceivedOption;
        public event NotifyBoard ReceivedBoard;

        public string LastMessage { 
            get => _lastMessage;
            set
            {
                _lastMessage = value;
                WriteLastMessage();
            }
        }

        public ConsoleDisplay()
        {

        }

        /// <summary>
        /// Starts the UI thread.
        /// </summary>
        public void Start()
        {
            throw new NotImplementedException();            
        }

        /// <summary>
        /// Request the UI thread to stop
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the option number inputted by the user
        /// </summary>
        /// <returns>Option number. -2=NotANumber</returns>
        public int GetOption()
        {
            Console.SetCursorPosition(10, 19);
            string input = Console.ReadLine();

            // Converts input to int
            if (int.TryParse(input, out int result))
                return result;
            else
                return -2;
        }

        /// <summary>
        /// Requests the user to enter a new board and returns the new board.
        /// </summary>
        /// <returns>New board received from the user.</returns>
        public SudokuBoard GetBoard(int nRows, int nCols)
        {
            int[,] board = new int[nRows, nCols];

            // Each line is a row in the board
            for (int row = 0; row < nRows; row++)
            {
                // Each char in the input string, is a number in the current row
                int col = 0;
                string input = Console.ReadLine();
                foreach (char c in input)
                {
                    int num = c;
                    // If the input char is '.', make it zero.
                    if (c == '.')
                        num = '0';
                    // Convert from char number to integer
                    num -= '0';

                    // Return if input is not a number
                    if (num < 0 || num > 9)
                        return null;

                    // Save in number in the board
                    board[row, col] = num;
                    col++;
                }
            }

            Console.WriteLine("Done. Enter a label for the board:");
            string label = Console.ReadLine();

            return new SudokuBoard(nRows, nCols, board, label);
        }

        public void DisplayBoardSimple(SudokuBoard board)
        {
            Console.SetCursorPosition(0, LIST_END);
            string output = String.Empty;
            
            // Gets retrives all the numbers
            Pos p = new Pos(0, 0);
            for(; p.Row < board.nRows; p.Row++)
            {
                for (p.Col = 0; p.Col < board.nCols; p.Col++)
                    output += board.GetNumber(p).ToString();

                // Write output
                Console.Write($"{0,-LIST_WIDTH:s}", output);
            }
        }

        public void DisplayBoardFancy(SudokuBoard board)
        {
            throw new NotImplementedException();
        }

        public void DisplayStats(Solver s)
        {
            throw new NotImplementedException();
        }

        void WriteBoardList(List<SudokuBoard> boards)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(String.Join(
                Environment.NewLine,
                "**************************************************",
                "* Loaded boards                                  *",
                "**************************************************",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "*                                                *",
                "**************************************************",
                "* Option:                                        *",
                "* Log:                                           *",
                "**************************************************"
            ));

            // Print sudoku boards labels
            int i = 3;
            foreach (SudokuBoard b in boards)
            {
                Console.SetCursorPosition(2, i);
                Console.Write($"[{(i++) - 2,2}] { b.Label }");
            }

            // Print exit option
            Console.SetCursorPosition(2, 16);
            Console.Write("[-1] Quit");

            // Print load option
            Console.SetCursorPosition(2, 17);
            Console.Write("[ 0] Load new board");

            // Print last message
            //UpdateLastMessage(lastMessage);
        }

        void ClearWorkspace()
        {
            Console.SetCursorPosition(0, 22);
            for (int r = 0; r < 50; r++)
            {
                for (int c = 0; c < 50; c++)
                    Console.Write(' ');
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Updates display with the latest last message
        /// </summary>
        void WriteLastMessage()
        {
            Console.SetCursorPosition(LOG_L_START, LOG_T_START);
            Console.Write($"{0,-42}", _lastMessage);
        }
    }
}
