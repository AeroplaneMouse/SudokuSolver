using System;

namespace SudokuSolver
{
    public class Solver
    {
        SudokuBoard board { get; set; }
        int missingNumbers { get; set; }
        int totalMissingNumbers { get; }
        int numbersPlaced { get; set; }

        public Solver(SudokuBoard board)
        {
            this.board = board;
            totalMissingNumbers = CountMissingNumbers();
            missingNumbers = totalMissingNumbers;
        }

        public bool Run()
        {
            bool numberPlaced = true;
            while (missingNumbers != 0 && numberPlaced)
            {
                numberPlaced = false;
                for (int num = 1; num < 10; num++)
                {
                    // Horizontally
                    numberPlaced = SolveHorizontallyFor(num);

                    // Vertically
                    if (!numberPlaced)
                        numberPlaced = SolveVerticallyFor(num);

                    // In the squares
                    if (!numberPlaced)
                        numberPlaced = SolveSquaresFor(num);

                    if (numberPlaced)
                        break;
                    //Thread.Sleep(500);
                }
            }
            return false;
        }

        public void PrintData()
        {
            Console.WriteLine($"Total missing numbers:  {totalMissingNumbers}");
            Console.WriteLine($"Missing numbers:        {missingNumbers}");
            Console.WriteLine($"Numbers placed:         {numbersPlaced}");
        }

        public bool SolveHorizontallyFor(int num)
        {
            Pos p = new Pos();
            Pos savedPos = new Pos();
            int placeablePositions;
            bool numberPlaced = false;

            for (p.Row = 0; p.Row < board.nRows; p.Row++)
            {
                placeablePositions = 0;
                for (p.Col = 0; p.Col < board.nCols; p.Col++)
                {
                    if (IsNumberPlaceable(num, p))
                    {
                        placeablePositions++;
                        p.CopyTo(savedPos);
                    }
                }

                if (placeablePositions == 1)
                {
                    PlaceNumber(num, savedPos);
                    numberPlaced = true;
                }
            }

            return numberPlaced;
        }

        public bool SolveVerticallyFor(int num)
        {
            Pos p = new Pos();
            Pos savedPos = new Pos();
            int placeablePositions;
            bool numberPlaced = false;

            for(p.Col = 0; p.Col < board.nCols; p.Col++)
            {
                placeablePositions = 0;
                for(p.Row = 0; p.Row < board.nRows; p.Row++)
                {
                    if(IsNumberPlaceable(num, p))
                    {
                        placeablePositions++;
                        p.CopyTo(savedPos);
                    }
                }

                if(placeablePositions == 1)
                {
                    PlaceNumber(num, savedPos);
                    numberPlaced = true;
                }
            }

            return numberPlaced;
        }

        public bool SolveSquaresFor(int num)
        {
            bool numberPlaced = false;
            Pos[] startPositions = new Pos[] {
                new Pos(0,0),
                new Pos(0,3),
                new Pos(0,6),
                new Pos(3,0),
                new Pos(3,3),
                new Pos(3,6),
                new Pos(6,0),
                new Pos(6,3),
                new Pos(6,6),
            };

            // Go through every square.
            foreach(Pos startPos in startPositions)
            {
                Pos pos = new Pos();
                Pos savedPos = new Pos();
                int placeablePositions = 0;

                // For every position in square, check if the number can be placed.
                for(pos.Row = startPos.Row; pos.Row < startPos.Row + 3; pos.Row++)
                {
                    for(pos.Col = startPos.Col; pos.Col < startPos.Col + 3; pos.Col++)
                    {
                        // If the number is placeable, save it's position.
                        if(IsNumberPlaceable(num, pos))
                        {
                            placeablePositions++;
                            pos.CopyTo(savedPos);
                        }
                    }
                }

                // If there is only one placeable position, the number is placed at that.
                if (placeablePositions == 1)
                {
                    PlaceNumber(num, savedPos);
                    numberPlaced = true;
                }
            }

            // Return whether or not a number has been placed.
            return numberPlaced;
        }

        private void PlaceNumber(int num, Pos savedPos)
        {
            board.Number[savedPos.Row, savedPos.Col] = num;
            missingNumbers--;
            numbersPlaced++;
        }

        private int CountMissingNumbers()
        {
            Pos p = new Pos();
            int missingNumbers = 0;
            for (; p.Row < board.nRows; p.Row++)
            {
                for (p.Col = 0; p.Col < board.nCols; p.Col++)
                {
                    if (board.GetNumber(p) == 0)
                        missingNumbers++;
                }
            }
            return missingNumbers;
        }

        /// <summary>
        /// Checks wether or not a given position is empty and if
        /// the given number can be legaly placed at pos.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pos"></param>
        /// <returns>True, if 'num' can be placed at 'pos'</returns>
        public bool IsNumberPlaceable(int num, Pos pos)
        {
            // Return false if the position is already occupied
            if (board.GetNumber(pos) != 0)
                return false;

            // Checking row
            Pos checkPos = new Pos(pos.Row, 0);
            for(; checkPos.Col < board.nCols; checkPos.Col++)
            {
                if (num == board.GetNumber(checkPos))
                    return false;
            }

            // Checking col
            checkPos = new Pos(0, pos.Col);
            for(; checkPos.Row < board.nRows; checkPos.Row++)
            {
                if (num == board.GetNumber(checkPos))
                    return false;
            }

            // Check square
            pos.CopyTo(checkPos);
            int distanceToRowStart = pos.Row % 3;
            int distanceToColStart = pos.Col % 3;
            int rowEnd = checkPos.Row + 3 - distanceToRowStart;
            int colEnd = checkPos.Col + 3 - distanceToColStart;

            for(checkPos.Row -= distanceToRowStart; checkPos.Row < rowEnd; checkPos.Row++)
            {
                for(checkPos.Col = pos.Col - distanceToColStart; checkPos.Col < colEnd; checkPos.Col++)
                {
                    if (num == board.GetNumber(checkPos))
                        return false;
                }
            }

            // If the number wasn't found in any checked position. It would be legal to place the number.
            // Though, it is not guarenteed to be the correct number to be placed in that position.
            return true;
        }
    }
}
