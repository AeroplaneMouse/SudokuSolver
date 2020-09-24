using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver;
using System.Collections.Generic;


namespace SudokuSolverTests
{
    [TestClass]
    public class Solver_Tests
    {
        Pos z = new Pos(1, 1);
        Pos p = new Pos(3, 1);
        Pos q = new Pos(2, 4);
        Pos r = new Pos(5, 6);
        Pos s = new Pos(7, 0);

        [TestMethod]
        public void IsNumberPlaceableInRow_Test()
        {
            SudokuBoard board = GenerateBoard();
            int numberToPlace = 3;
            

            Solver solver = new Solver(board);

            Assert.IsTrue(solver.IsNumberPlaceable(numberToPlace, z), $"{numberToPlace} is not placeable at {z}.");
            Assert.IsFalse(solver.IsNumberPlaceable(numberToPlace, p), $"{numberToPlace} is placeable at {p}.");
            Assert.IsTrue(solver.IsNumberPlaceable(numberToPlace, r), $"{numberToPlace} is not placeable at {r}.");
        }

        [TestMethod]
        public void IsNumberPlaceableInCol_Test()
        {
            SudokuBoard board = GenerateBoard();
            Solver solver = new Solver(board);

            Assert.IsTrue(solver.IsNumberPlaceable(3, s), $"3 is not placeable at {s}.");
            Assert.IsFalse(solver.IsNumberPlaceable(4, s), $"4 is placeable at {s}.");
        }

        [TestMethod]
        public void IsNumberPlaceableInSquare_Test()
        {
            SudokuBoard board = GenerateBoard();
            Solver solver = new Solver(board);

            Assert.IsTrue(solver.IsNumberPlaceable(5, z), $"5 is not placeable at {z}.");
            Assert.IsFalse(solver.IsNumberPlaceable(9, z), $"9 is placeable at {z}.");
        }

        private SudokuBoard GenerateBoard()
        {
            int[,] board = new int[,]
            {
                { 1, 2, 0, 0, 3, 0, 0, 0, 0 },
                { 4, 0, 6, 0, 0, 0, 0, 0, 0 },
                { 7, 8, 9, 0, 0, 0, 0, 3, 0 },
                { 0, 0, 0, 0, 0, 3, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 3 }
            };
            return new SudokuBoard(9, 9, board);
        }
    }
}
