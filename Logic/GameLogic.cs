using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GameLogic // TODO:Consider changing the class name to TicTacToeGame (It's more like game manager than game logic).
    {
        private Player m_p1;
        private Player m_p2;
        private Board m_GameBoard;

        public GameLogic(Board i_Board, Player i_P1, Player i_P2)
        {
            this.m_GameBoard = i_Board;
            this.m_p1 = i_P1;
            this.m_p2 = i_P2;
        }

        public bool CheckForLoser(int i_ColumnChosen, int i_RowChosen, eBoardSigns i_MarkedSign)
        {
            return (checkForSequenceInColumn(i_ColumnChosen, i_MarkedSign)
                    || checkForSequenceInRow(i_RowChosen, i_MarkedSign) || checkForSequenceInDiagonals(i_MarkedSign));
        }

        private bool checkForSequenceInDiagonals(eBoardSigns i_MarkedSign)
        {
            bool foundSequence = true;
            for (int i = 1; i <= m_GameBoard.MatrixSideSize; i++) // Check main diagonal. 
            {
                if (m_GameBoard.GetBoard()[i, i] != i_MarkedSign)
                {
                    foundSequence = false;
                    break;
                }
            }

            if (!foundSequence) // Check secondary diagonal.
            {
                int cellIndex = m_GameBoard.MatrixSideSize - 1;
                for (int i = 0; i < m_GameBoard.MatrixSideSize; i++)
                {

                    if (m_GameBoard.GetBoard()[i, cellIndex--] != i_MarkedSign)
                    {
                        foundSequence = false;
                        break;
                    }
                }

            }
            return foundSequence;
        }

        private bool checkForSequenceInRow(int i_RowChosen, eBoardSigns i_MarkedSign)
        {
            bool result = true;
            for (int i = 0; i < m_GameBoard.MatrixSideSize; i++)
            {
                if (m_GameBoard.GetBoard()[i_RowChosen, i] != i_MarkedSign)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool checkForSequenceInColumn(int i_ColumnChosen, eBoardSigns i_MarkedSign)
        {
            bool result = true;
            for (int i = 0; i < m_GameBoard.MatrixSideSize; i++)
            {
                if (m_GameBoard.GetBoard()[i, i_ColumnChosen] != i_MarkedSign)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        //TODO: Check if multiple booleans are the correct way. Also check efficiency of the while loop.  
        public bool GenerateComputerMove(eBoardSigns i_ComputerSign)
        {
            bool succeededGeneratingMove = false;
            bool keepTrying = true;
            int generatedRow;
            int generatedColumn;
            Random rand = new Random();

            if (CheckIfBoardFilled())
            {
                keepTrying = false;
            }

            while(keepTrying)
            {
                generatedColumn = rand.Next(0, m_GameBoard.MatrixSideSize);
                generatedRow = rand.Next(0, m_GameBoard.MatrixSideSize);
                if (m_GameBoard.MarkCell(i_ComputerSign, generatedColumn, generatedRow))
                {
                    succeededGeneratingMove = true;
                    keepTrying = false;
                }
            }
            return succeededGeneratingMove;
        }

        public bool CheckIfBoardFilled()
        {
            return m_GameBoard.NumberOfBlankCells > 0;
        }


    }
}