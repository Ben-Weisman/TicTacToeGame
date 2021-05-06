using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Board
    {

        private char[,] m_Board;
        private int m_BoardSideSize;
        public Board(int i_NumColumns, int i_NumRows)
        {
            m_Board = new char[i_NumRows, i_NumColumns];
            for (int i = 0; i < i_NumRows; i++)
            {
                for (int j = 0; j < i_NumColumns; j++)
                {
                    m_Board[i, j] = '%';
                }
            }
        }


        public void SetPlayerSignInCell(char i_Sign, int i_NumColumns, int i_NumRows)
        {
            if (i_Sign == 'O' || i_Sign == 'X')
            {
                this.m_Board[i_NumColumns, i_NumRows] = i_Sign;
            }
            else
            {
                throw new Exception("Illegal sign entered.");
            }
        }

        private void EmptyCell(int i_NumColumns, int i_NumRows)
        {
            this.m_Board[i_NumColumns, i_NumRows] = ' ';
        }

        public char GetSignOfCell(int i_NumColumns, int i_NumRows)
        {
            return this.m_Board[i_NumColumns, i_NumRows];
        }


    }
}