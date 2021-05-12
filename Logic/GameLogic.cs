﻿using System;
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
            for (int i = 0; i < m_GameBoard.MatrixSideSize; i++) // Check main diagonal. 
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

        
        public PlayerTurnInfo GenerateComputerMove(eBoardSigns i_ComputerSign, PlayerTurnInfo i_PrevTurnInfo)
        {
            
            PlayerTurnInfo result = new PlayerTurnInfo();
            int boardSideSize = m_GameBoard.MatrixSideSize;
            bool isRowTheSymmetricalLine;

            if (boardSideSize % 2 == 0)
            {
                isRowTheSymmetricalLine = true;
                int symmetricalLine = boardSideSize / 2;
                if (i_PrevTurnInfo.CellRow < symmetricalLine)
                {
                    result = SymmetricMove(i_PrevTurnInfo, isRowTheSymmetricalLine);
                }
                else
                {
                    result = SymmetricMove(i_PrevTurnInfo, isRowTheSymmetricalLine);
                }

                m_GameBoard.MarkCell(i_ComputerSign, result.CellColumn, result.CellRow);
            }
            else
            {
                isRowTheSymmetricalLine = true;
                int splittingLine = boardSideSize / 2;
                int symmetricalLine = splittingLine + 1;
                if (i_PrevTurnInfo.CellRow < splittingLine)
                {
                    result = SymmetricMove(i_PrevTurnInfo, isRowTheSymmetricalLine);
                    if (!m_GameBoard.GetSignOfCell(result.CellColumn, result.CellRow).Equals(eBoardSigns.Blank))
                    {
                        result = SmartChooseOfCell(i_ComputerSign);
                    }
                    else
                    {
                        m_GameBoard.MarkCell(i_ComputerSign, result.CellColumn, result.CellRow);
                    }
                }
                else if (i_PrevTurnInfo.CellRow > splittingLine)
                {
                    result = SymmetricMove(i_PrevTurnInfo, isRowTheSymmetricalLine);
                    if (!m_GameBoard.GetSignOfCell(result.CellColumn, result.CellRow).Equals(eBoardSigns.Blank))
                    {
                        result = SmartChooseOfCell(i_ComputerSign);
                    }
                    else
                    {
                        m_GameBoard.MarkCell(i_ComputerSign, result.CellColumn, result.CellRow);
                    }
                }
                else if (i_PrevTurnInfo.CellRow == splittingLine)
                {
                    isRowTheSymmetricalLine = false;
                    if (i_PrevTurnInfo.CellColumn < splittingLine)
                    {
                        result = SymmetricMove(i_PrevTurnInfo, isRowTheSymmetricalLine);
                        if (!m_GameBoard.GetSignOfCell(result.CellColumn, result.CellRow).Equals(eBoardSigns.Blank))
                        {
                            result = SmartChooseOfCell(i_ComputerSign);
                        }
                        else
                        {
                            m_GameBoard.MarkCell(i_ComputerSign, result.CellColumn, result.CellRow);
                        }
                    }
                    else if (i_PrevTurnInfo.CellColumn > splittingLine)
                    {
                        result = SymmetricMove(i_PrevTurnInfo, isRowTheSymmetricalLine);
                        if (!m_GameBoard.GetSignOfCell(result.CellColumn, result.CellRow).Equals(eBoardSigns.Blank))
                        {
                            result = SmartChooseOfCell(i_ComputerSign);
                        }
                        else
                        {
                            m_GameBoard.MarkCell(i_ComputerSign, result.CellColumn, result.CellRow);
                        }
                    }
                    else if (i_PrevTurnInfo.CellColumn == splittingLine)
                    {
                        result = SmartChooseOfCell(i_ComputerSign);
                    }
                }
            }

            return result;
        }

        private PlayerTurnInfo SymmetricMove(PlayerTurnInfo i_PrevTurnInfo, bool i_IsRowTheSymmetricalLine)
        {
            int boardSideSize = m_GameBoard.MatrixSideSize;
            PlayerTurnInfo result = new PlayerTurnInfo();
            if (i_IsRowTheSymmetricalLine)
            {
                result.CellColumn = i_PrevTurnInfo.CellColumn;
                result.CellRow = boardSideSize - i_PrevTurnInfo.CellRow - 1;
            }
            else
            {
                result.CellColumn = boardSideSize - i_PrevTurnInfo.CellColumn - 1;
                result.CellRow = i_PrevTurnInfo.CellRow;
            }
            return result;
        }

        private PlayerTurnInfo SmartChooseOfCell(eBoardSigns i_ComputerSign)
        {
            bool valid = false;
            PlayerTurnInfo result=new PlayerTurnInfo();

            while (!valid)
            {
                result = ChooseRandomCell();
                if(m_GameBoard.MarkCell(i_ComputerSign, result.CellColumn, result.CellRow))
                {
                    if(CheckForLoser(result.CellColumn, result.CellRow, i_ComputerSign))
                    {
                        m_GameBoard.ClearCell(result.CellColumn, result.CellRow);
                        result = ChooseRandomCell();
                    }
                    else
                    {
                        valid = true;
                    }
                }
            }

            return result;
        }

        private PlayerTurnInfo ChooseRandomCell()
        {
            PlayerTurnInfo result = new PlayerTurnInfo();
            Random rand = new Random();
            result.CellColumn = rand.Next(0, m_GameBoard.MatrixSideSize);
            result.CellRow = rand.Next(0, m_GameBoard.MatrixSideSize);
            result.PlayerWantsToQuit = false;
            return result;
        }

        public bool CheckIfBoardFilled()
        {
            return m_GameBoard.NumberOfBlankCells == 0;
        }

    }
}