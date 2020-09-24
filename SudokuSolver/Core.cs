using System;
using SudokuSolver.Models;
using System.Collections.Generic;

namespace SudokuSolver
{
    public class Core
    {
        public List<SudokuBoard> Boards { get; }
        
        public Core()
        {
            // Initialize list of boards
            Boards = new List<SudokuBoard>();
        }

        /// <summary>
        /// Adds a new board to the system.
        /// </summary>
        /// <param name="board">The new board to be added.</param>
        public void AddBoard(SudokuBoard board)
        {
            Boards.Add(board);
        }

        /// <summary>
        /// Returns a board from the list of board by its index
        /// </summary>
        /// <param name="index">The index of the board</param>
        /// <returns>Board at the given index</returns>
        public SudokuBoard GetBoardByIndex(int index)
        {
            if (Boards == null || Boards.Count == 0 || index < 0 || index >= Boards.Count)
                return null;
            else
                return Boards[index];
        }
    }
}
