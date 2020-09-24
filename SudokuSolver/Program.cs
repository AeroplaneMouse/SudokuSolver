using System;
using System.Collections.Generic;
using System.Diagnostics;
using SudokuSolver.Models;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            bool quit = false;
            List<SudokuBoard> sudokuTestBoards = new List<SudokuBoard>();

            #region Test boards
            // Normal #14
            SudokuBoard boardOne = new SudokuBoard(9, 9, new int[,]
            {
                {3,0,0,0,6,0,8,0,1 },
                {0,0,0,0,0,4,0,0,0 },
                {0,0,1,8,0,0,0,0,5 },
                {0,0,2,0,5,6,0,7,0 },
                {8,0,0,1,0,9,0,0,4 },
                {0,7,0,2,4,0,5,0,0 },
                {2,0,0,0,0,1,3,0,0 },
                {0,0,0,7,0,0,0,0,0 },
                {1,0,8,0,3,0,0,0,9 }
            }, "TestBoard_1");
            sudokuTestBoards.Add(boardOne);

            // Easy #2 
            SudokuBoard boardTwo = new SudokuBoard(9, 9, new int[,]
            {
                {3,0,1,8,4,5,6,0,9},
                {0,0,0,0,6,0,0,0,0},
                {5,0,0,3,0,9,0,0,2},
                {1,0,9,0,0,0,3,0,5},
                {6,3,0,0,0,0,0,9,1},
                {8,0,7,0,0,0,2,0,4},
                {2,0,0,5,0,4,0,0,3},
                {0,0,0,0,1,0,0,0,0},
                {4,0,3,2,9,7,8,0,6}
            }, "TestBoard_2");
            sudokuTestBoards.Add(boardTwo);

            // Easy #3
            SudokuBoard boardThree = new SudokuBoard(9, 9, new int[,]
            {
                {0,4,8,0,0,0,9,2,0},
                {0,0,3,8,1,5,4,0,0},
                {5,0,0,0,0,0,0,0,1},
                {0,0,1,5,0,6,8,0,0},
                {0,7,0,2,0,9,0,1,0},
                {0,0,2,1,0,8,5,0,0},
                {7,0,0,0,0,0,0,0,3},
                {0,0,9,7,5,1,2,0,0},
                {0,1,4,0,0,0,7,5,0}
            }, "TestBoard_3");
            sudokuTestBoards.Add(boardThree);

            // Normal #10
            SudokuBoard boardFour = new SudokuBoard(9, 9, new int[,]
            {
                {0,0,0,0,0,2,0,1,0},
                {3,0,8,0,9,1,0,0,0},
                {0,0,0,6,3,0,0,5,0},
                {4,2,0,0,0,0,5,0,0},
                {0,3,9,0,0,0,1,6,0},
                {0,0,1,0,0,0,0,7,2},
                {0,9,0,0,4,3,0,0,0},
                {0,0,0,5,2,0,7,0,9},
                {0,6,0,9,0,0,0,0,0}
            }, "TestBoard_4");
            sudokuTestBoards.Add(boardFour);

            // Hard #2
            SudokuBoard boardFive = new SudokuBoard(9, 9, new int[,]
            {
                {0,4,0,0,1,0,2,0,6},
                {0,3,0,5,9,0,8,0,1},
                {0,0,0,4,0,0,0,0,0},
                {6,1,8,0,0,0,0,0,0},
                {0,0,0,0,2,0,0,0,0},
                {0,0,0,0,0,0,1,6,3},
                {0,0,0,0,0,5,0,0,0},
                {1,0,6,0,7,9,0,8,0},
                {7,0,4,0,6,0,0,3,0}
            }, "TestBoard_5");
            sudokuTestBoards.Add(boardFive);

            // 1. Hackerakademi
            SudokuBoard boardSix = new SudokuBoard(9, 9, new int[,]
            {
                {2,0,3,0,0,0,7,0,4},
                {0,9,1,2,7,4,5,0,3},
                {0,6,7,3,5,9,2,0,1},
                {7,0,0,6,0,3,0,4,5},
                {5,3,4,0,1,7,6,2,8},
                {0,8,0,0,2,0,9,0,7},
                {9,1,0,5,3,6,4,7,2},
                {3,4,5,0,9,0,0,0,6},
                {0,0,2,0,0,1,0,0,9}
            }, "TestBoard_6");
            sudokuTestBoards.Add(boardSix);

            #endregion

            Stopwatch stopwatch = new Stopwatch();

            // Select option
            string lastMessage = "";
            while (!quit)
            {
                PrintBoardOptions(sudokuTestBoards, lastMessage);
                int op = GetOption();

                if (op == -2)
                    lastMessage = "Input MUST be a number!";
                if (op == -1)
                    quit = true;
                else if (op == 0)
                {
                    // Load new board from user
                    lastMessage = String.Empty;
                    UpdateLastMessage("Loading new board from user");
                    ClearWorkSpace();
                    Console.SetCursorPosition(0, 25);

                    SudokuBoard b = GetBoard(9, 9);
                    if (b != null)
                    {
                        sudokuTestBoards.Add(b);
                        lastMessage = "New board loaded";
                    }
                    else
                        lastMessage = "Error loading new board";
                }
                else if (op > 0 && op <= sudokuTestBoards.Count)
                {
                    // Solve board from list
                    SudokuBoard b = sudokuTestBoards[op - 1];
                    lastMessage = $"Solving board '{ b.Label }'";
                    ClearWorkSpace();

                    // Print board
                    Console.SetCursorPosition(0, 25);
                    Solve(b, stopwatch, b.Label);
                }
                else
                    lastMessage = "Invalid option";

                UpdateLastMessage(lastMessage);
            }
        }

        private static SudokuBoard GetBoard(int nRows, int nCols)
        {
            int[,] board = new int[nRows, nCols];

            // Each line is a row in the board
            for(int row = 0; row < nRows; row++)
            {
                // Each char in the input string, is a number in the current row
                int col = 0;
                string input = Console.ReadLine();
                foreach(char c in input)
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

        private static void Solve(SudokuBoard board, Stopwatch stopwatch, string number)
        {
            // Setup
            Solver solver = new Solver(board);

            // Printing
            Console.WriteLine("\n**********************************");
            Console.WriteLine($"*** Board {number}");
            board.FancyPrint();
            
            // Stopwatch
            stopwatch.Reset();
            stopwatch.Start();

            // Solve
            solver.Run();

            // Stopwatch
            stopwatch.Stop();

            // Printing
            //board.Print();
            board.FancyPrint();
            solver.PrintData();
            Console.WriteLine($"Time used: {stopwatch.ElapsedMilliseconds} milliseconds.");
        }
    }
}
