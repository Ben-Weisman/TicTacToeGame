using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class InputOutputHandler
    {

        public void PrintCellsLine(int i_NumColumns, int i_RowNumber)
        {
            Console.Write("  {0}", i_RowNumber + 1);
            for (int i = 0; i < i_NumColumns; i++)
            {
                if (i != 0)
                {
                    Console.Write("| {0} ", GetCell(i, i_RowNumber));
                }
                else
                {
                    Console.Write(" {0} ", GetCell(i, i_RowNumber));
                }
            }
            Console.Write(Environment.NewLine);
        }

        public void PrintHorizontalSeparatorsLine(int i_NumColumns)
        {
            Console.Write("   ");
            for (int i = 1; i <= i_NumColumns; i++)
            {
                Console.Write("----");
            }
            Console.Write(Environment.NewLine);
        }

        public void PrintUpperLine(int i_NumColumns)
        {
            Console.Write("   ");
            for (int i = 1; i <= i_NumColumns; i++)
            {
                Console.Write(" {0}  ", i);
            }
            Console.Write(Environment.NewLine);
        }

        public void PrintBoard()
        {
            int numColumns = m_Board.GetLength(0);
            int numRows = m_Board.GetLength(1);

            PrintUpperLine(numColumns);

            for (int i = 0; i < numRows; i++)
            {
                PrintCellsLine(numColumns, i);
                if (i != numRows - 1)
                {
                    PrintHorizontalSeparatorsLine(numColumns);
                }
            }
        }
    }
}