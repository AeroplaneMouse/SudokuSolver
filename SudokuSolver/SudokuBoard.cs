using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class SudokuBoard
    {
        public int[,] Number { get; set; }
        public string Label { get; set; }
        public int nRows { get; }
        public int nCols { get; }

        public SudokuBoard(int nRows, int nCols, int[,] board, string label="")
        {
            this.nRows = nRows;
            this.nCols = nCols;
            Number = board;
            Label = label;
        }

        public int GetNumber(Pos p)
        {
            return Number[p.Row, p.Col];
        }

        public void Print()
        {
            int i = 0;
            foreach(int number in Number)
            {
                Console.Write($"{ number }");
                i++;

                if (i == 9)
                {
                    Console.WriteLine();
                    i = 0;
                }
            }
        }
        public void FancyPrint()
        {
            string middleLine = ConstructLine("-");
            for (int row = 0; row < 9; row++)
            {
                if (row % 3 == 0)
                    Console.WriteLine(middleLine);
                Console.WriteLine(ConstructRow(row));
            }
            Console.WriteLine(middleLine);
        }

        private string ConstructLine(string pattern)
        {
            string output = " ";
            for(int i = 0; i < nCols * 2 + 3 * 2 + 1; i++)
            {
                output += pattern;
            }
            return output;
        }

        private string ConstructRow(int row)
        {
            string output = string.Empty;
            for (int col = 0; col < 9; col++)
            {
                if (col % 3 == 0)
                    output += " | ";
                else
                    output += " ";

                int number = Number[row, col];
                if (number == 0)
                    output += " ";
                else
                    output += number;
            }
            output += " |";
            return output;
        }

        private int[,] ContructBoard()
        {
            int[,] board = new int[nRows, nCols];
            Pos p = new Pos();
            for (; p.Row < nRows; p.Row++)
            {
                for (p.Col = 0; p.Col < nCols; p.Col++)
                {
                    Number[p.Row, p.Col] = 0;
                }
            }
            return board;
        }
    }
}
