using System;
using System.Collections.Generic;
using System.Diagnostics;
using SudokuSolver.Models;
using SudokuSolver.UI;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            ISudokuUI ui = new ConsoleDisplay();
            Core core = new Core();

            // Adding test boards to the core
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
            core.AddBoard(boardOne);

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
            core.AddBoard(boardTwo);

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
            core.AddBoard(boardThree);

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
            core.AddBoard(boardFour);

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
            core.AddBoard(boardFive);

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
            core.AddBoard(boardSix);

            #endregion

            Controller c = new Controller(ui, core);
        }
    }
}
