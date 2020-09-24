using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Pos
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Pos()
            : this(0, 0)
        { }

        public Pos(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return $"({Row},{Col})";
        }

        public void CopyTo(Pos oth)
        {
            oth.Row = Row;
            oth.Col = Col;
        }
    }
}
