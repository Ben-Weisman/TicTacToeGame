using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GameLogic 
    {
        private Player m_PlayerX;
        private Player m_PlayerO;
        private readonly Board m_GameBoard;

        public GameLogic(Board i_Board, Player i_PlayerX, Player i_PlayerO)
        {
            this.m_GameBoard = i_Board;
            this.m_PlayerX = i_PlayerX;
            this.m_PlayerO = i_PlayerO;
        }

        public bool CheckForLoser(int i_ColumnChosen, int i_RowChosen, eBoardSigns i_MarkedSign)
        {
            return (CheckForSequenceInColumn(i_ColumnChosen, i_MarkedSign)
                    || CheckForSequenceInRow(i_RowChosen, i_MarkedSign) || CheckForSequenceInDiagonals(i_MarkedSign));
        }

        private bool CheckForSequenceInDiagonals(eBoardSigns i_MarkedSign)
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

        private bool CheckForSequenceInRow(int i_RowChosen, eBoardSigns i_MarkedSign)
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

        private bool CheckForSequenceInColumn(int i_ColumnChosen, eBoardSigns i_MarkedSign)
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
        public PlayerTurnInfo GenerateComputerMove(eBoardSigns i_ComputerSign)
        {
            bool keepTrying = true;
            PlayerTurnInfo result = new PlayerTurnInfo();
            Random rand = new Random();

            if (CheckIfBoardFilled())
            {
                keepTrying = false;
                result.CellRow = -1;
                result.CellColumn = -1;
                result.PlayerWantsToQuit = false;
            }

            while (keepTrying)
            {
                int generatedColumn = rand.Next(0, m_GameBoard.MatrixSideSize);
                int generatedRow = rand.Next(0, m_GameBoard.MatrixSideSize);
                if (m_GameBoard.MarkCell(i_ComputerSign, generatedColumn, generatedRow))
                {
                    keepTrying = false;
                    result.CellRow = generatedRow;
                    result.CellColumn = generatedColumn;
                    result.PlayerWantsToQuit = false;
                }
            }
            return result;
        }

        public bool CheckIfBoardFilled()
        {
            return m_GameBoard.NumberOfBlankCells == 0;
        }

    }
}