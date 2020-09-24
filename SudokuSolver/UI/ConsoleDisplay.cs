using System;
using System.Collections.Generic;

namespace SudokuSolver.UI
{
    public class ConsoleDisplay
    {
        string _lastMessage = string.Empty;

        public string LastMessage { 
            get => _lastMessage; 
            set => _lastMessage = value; 
        }


        public ConsoleDisplay()
        {

        }

        public void Start(List<SudokuBoard> boards)
        {
            // Create UI thread



            bool quit = false;
            while (!quit)
            {
                DisplayBoardList(boards);
                int op = GetOption();


            }
        }

        /// <summary>
        /// Returns the option number inputted by the user
        /// </summary>
        /// <returns>Option number. -2=NotANumber</returns>
        int GetOption()
        {
            Console.SetCursorPosition(10, 19);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result))
                return result;
            else
                return -2;
        }

        void DisplayBoardList(List<SudokuBoard> boards)
        {
            //Console.Clear();
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
    }
}
