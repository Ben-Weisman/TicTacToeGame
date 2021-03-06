using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Board
    {

        private eBoardSigns[,] m_Board;
        private int m_BoardSideSize;
        private int m_NumberOfBlankCells;

        public Board(int i_NumColumns, int i_NumRows)
        {
            m_Board = new eBoardSigns[i_NumRows, i_NumColumns];
            for (int i = 0; i < i_NumRows; i++)
            {
                for (int j = 0; j < i_NumColumns; j++)
                {
                    m_Board[i, j] = eBoardSigns.Blank;
                }
            }

            this.MatrixSideSize = i_NumColumns;
            m_NumberOfBlankCells = i_NumColumns * i_NumRows;
        }
        
        public int MatrixSideSize
        {
            get;
            set;
        }
        public int NumberOfBlankCells
        {
            get;
            set;
        }


        public bool MarkCell(eBoardSigns i_Sign, int i_NumColumns, int i_NumRows)
        {

            bool res = false;

            if (m_Board[i_NumColumns, i_NumRows].Equals(eBoardSigns.Blank))
            {
                this.m_Board[i_NumColumns, i_NumRows] = i_Sign;
                res = true;
                m_NumberOfBlankCells--;
            }

            return res;


            /*if (i_Sign == 'O' || i_Sign == 'X')
            {
                this.m_Board[i_NumColumns, i_NumRows] = i_Sign;
            }
            else
            {
                throw new Exception("Illegal sign entered.");
            }*/
        }

        public void ClearBoard()
        {
            int sideSize = MatrixSideSize;
            for(int i = 0; i < sideSize; i++)
            {
                for(int j = 0; j < sideSize; j++)
                {
                    clearCell(i,j);
                }
            }
        }

        private void clearCell(int i_NumColumns, int i_NumRows)
        {
            this.m_Board[i_NumColumns, i_NumRows] = eBoardSigns.Blank;
            m_NumberOfBlankCells++;
        }

        public eBoardSigns GetSignOfCell(int i_NumColumns, int i_NumRows)
        {
            return this.m_Board[i_NumColumns, i_NumRows];
        }


        public eBoardSigns[,] GetBoard()
        {
            return m_Board;
        }
    }
}