using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Board
    {

        public readonly eBoardSigns[,] m_Board;

        public eBoardSigns[,] GetBoard()
        {
            return m_Board;
        }

        public int MatrixSideSize { get; set; } 
        public int NumberOfBlankCells { get; set; }

        public Board(int i_NumColumns, int i_NumRows)
        {
            this.m_Board = new eBoardSigns[i_NumRows, i_NumColumns];
            for (int i = 0; i < i_NumRows; i++)
            {
                for (int j = 0; j < i_NumColumns; j++)
                {
                    m_Board[i, j] = eBoardSigns.Blank;
                }
            }

            this.MatrixSideSize = i_NumColumns;
            this.NumberOfBlankCells = i_NumColumns * i_NumRows;
        }

        public bool MarkCell(eBoardSigns i_Sign, int i_NumColumns, int i_NumRows)
        {

            bool res = false;

            if (m_Board[i_NumRows, i_NumColumns].Equals(eBoardSigns.Blank))
            {
                m_Board[i_NumRows, i_NumColumns] = i_Sign;
                res = true;
                NumberOfBlankCells--;
            }

            return res;
        }

        public void ClearBoard()
        {
            int sideSize = MatrixSideSize;
            for (int i = 0; i < sideSize; i++)
            {
                for (int j = 0; j < sideSize; j++)
                {
                    ClearCell(i, j);
                }
            }

            NumberOfBlankCells = MatrixSideSize * MatrixSideSize;
        }

        public void ClearCell(int i_NumColumns, int i_NumRows)
        {
            this.m_Board[i_NumRows, i_NumColumns] = eBoardSigns.Blank;
            NumberOfBlankCells++;
        }

        public bool CheckCoordinates(int i_ChosenColumn, int i_ChosenRow)
        {
            i_ChosenColumn += 1; 
            i_ChosenRow += 1;
            bool legalRowNumber = (i_ChosenRow <= MatrixSideSize && i_ChosenRow >= 1) ? true : false;
            bool legalColumnNumber = (i_ChosenColumn <= MatrixSideSize && i_ChosenColumn >= 1) ? true : false;
            return legalColumnNumber && legalRowNumber;
        }


        public eBoardSigns GetSignOfCell(int i_NumColumns, int i_NumRows)
        {
            return this.m_Board[i_NumRows, i_NumColumns];
        }
    }
}