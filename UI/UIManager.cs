using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace UI
{
    class UIManager
    {
        private GameLogic m_Game;
        private readonly Player m_PlayerX;
        private Player m_PlayerO;
        private Board m_GameBoard;
        private readonly InputOutputHandler m_InputOutput;  //check that readonly
        private readonly StringBuilder m_Board;
        private int m_MatrixSideSize;

        public UIManager()
        {
            this.m_PlayerX = new Player(eBoardSigns.X, ePlayerType.Human);
            this.m_InputOutput = new InputOutputHandler();
            this.m_Board = new StringBuilder();
            this.m_MatrixSideSize = 0;
        }

        public void StartGame()
        {
            ShowMenu();
            GameManager();
        }

        private void ShowMenu()
        {
            m_MatrixSideSize = m_InputOutput.AskForMatrixSize();
            m_GameBoard = new Board(m_MatrixSideSize, m_MatrixSideSize);
            ePlayerType PlayerOType = GetOpponentType();
            m_PlayerO = new Player(eBoardSigns.O, PlayerOType);
            m_Game = new GameLogic(m_GameBoard, m_PlayerX, m_PlayerO);
        }

        public void GameManager()
        {
            ePlayerAnswer playerAnswer = ePlayerAnswer.Yes;

            while (playerAnswer.Equals(ePlayerAnswer.Yes))
            {
                eEndGameStatus gameStatus = PlayingGame();
                if (gameStatus.Equals(eEndGameStatus.PlayerXWon))
                {
                    m_PlayerX.IncreaseScoreByOne();
                }
                else if (gameStatus.Equals(eEndGameStatus.PlayerOWon) ||
                         gameStatus.Equals(eEndGameStatus.PlayerXQuit))
                {
                    m_PlayerO.IncreaseScoreByOne();
                }
                m_GameBoard.ClearBoard();
                m_InputOutput.ShowScores(m_PlayerX, m_PlayerO);
                playerAnswer = PlayAgain();
            }
        }
        public eEndGameStatus PlayingGame()
        {
            eEndGameStatus status = 0;
            bool endCondition = false;
            PlayerTurnInfo playerTurnInfo = new PlayerTurnInfo();
            Ex02.ConsoleUtils.Screen.Clear();
            CreateBoard();
            while (!endCondition)
            {
                PlayerTurn(m_PlayerX, ref status, ref playerTurnInfo);
                endCondition = FindEndConditions(playerTurnInfo, ref status, eBoardSigns.X);
                if (!endCondition)
                {
                    PlayerTurn(m_PlayerO, ref status, ref playerTurnInfo);
                    endCondition = FindEndConditions(playerTurnInfo, ref status, eBoardSigns.O);
                }
            }
            return status;
        }

        public void PlayerTurn(Player i_Player, ref eEndGameStatus io_Status, ref PlayerTurnInfo io_PrevTurnInfo)
        {
            PlayerTurnInfo currentTurnInfo = new PlayerTurnInfo();
            if (i_Player.PlayerType.Equals(ePlayerType.Human))
            {
                HumanPlayerTurn(i_Player.Sign, ref currentTurnInfo, ref io_Status);
                io_PrevTurnInfo = currentTurnInfo;
            }
            if (i_Player.PlayerType.Equals(ePlayerType.Computer))
            {
                ComputerPlayerTurn(i_Player.Sign, ref currentTurnInfo, io_PrevTurnInfo);
            }

        }

        private void HumanPlayerTurn(eBoardSigns i_Sign, ref PlayerTurnInfo io_PlayerTurnInfo, ref eEndGameStatus io_Status)
        {
            bool valid = false;
            while (!valid)
            {
                m_InputOutput.AskPlayerForChosenCell(ref io_PlayerTurnInfo);
                int numRow = io_PlayerTurnInfo.CellRow;
                int numCol = io_PlayerTurnInfo.CellColumn;
                if (io_PlayerTurnInfo.PlayerWantsToQuit)
                {
                    io_Status = eEndGameStatus.PlayerXQuit;
                    valid = true;
                }
                else
                {
                    if (m_GameBoard.CheckCoordinates(numCol, numRow))
                    {
                        if (eBoardSigns.Blank == m_GameBoard.GetSignOfCell(numCol, numRow))
                        {
                            m_GameBoard.MarkCell(i_Sign, numCol, numRow);
                            Ex02.ConsoleUtils.Screen.Clear();
                            CreateBoard();
                            valid = true;
                        }
                        else
                        {
                            m_InputOutput.OccupiedCell(numCol, numRow);
                        }
                    }
                    else
                    {
                        m_InputOutput.InvalidChosenCoordinates();
                    }
                }
            }
        }

        private void ComputerPlayerTurn(eBoardSigns i_Sign, ref PlayerTurnInfo io_CurrentTurnInfo, PlayerTurnInfo i_PrevTurnInfo)
        {
            io_CurrentTurnInfo = m_Game.GenerateComputerMove(i_Sign, i_PrevTurnInfo);
            Ex02.ConsoleUtils.Screen.Clear();
            CreateBoard();
        }

        public ePlayerAnswer PlayAgain()
        {
            bool valid = false;
            ePlayerAnswer playerAnswer = 0;
            while (!valid)
            {
                char answer = m_InputOutput.AskForPlayingAgain();
                if (answer == 'y')
                {
                    playerAnswer = ePlayerAnswer.Yes;
                    valid = true;
                }
                else if (answer == 'n')
                {
                    playerAnswer = ePlayerAnswer.No;
                    valid = true;
                }
                else
                {
                    m_InputOutput.InvalidInput();
                }
            }

            return playerAnswer;
        }

        public ePlayerType GetOpponentType()
        {
            bool valid = false;
            ePlayerType opponentType = 0;

            while (!valid)
            {
                char playerInput = m_InputOutput.AskForPlayerOType();
                if (playerInput == 'h')
                {
                    opponentType = ePlayerType.Human;
                    valid = true;
                }
                else if (playerInput == 'c')
                {
                    opponentType = ePlayerType.Computer;
                    valid = true;
                }
                else
                {
                    m_InputOutput.InvalidInput();
                }
            }

            return opponentType;
        }

        public bool FindEndConditions(PlayerTurnInfo io_PlayerTurnInfo, ref eEndGameStatus io_Status, eBoardSigns i_Sign)
        {
            bool endConditionMet = false;
            if (io_PlayerTurnInfo.PlayerWantsToQuit)
            {
                io_Status = eEndGameStatus.PlayerXQuit;
                m_InputOutput.DeclareGameResult(io_Status);
                endConditionMet = true;
            }
            else if (m_Game.CheckForLoser(io_PlayerTurnInfo.CellColumn, io_PlayerTurnInfo.CellRow, i_Sign))
            {
                if (i_Sign.Equals(eBoardSigns.X))
                {
                    io_Status = eEndGameStatus.PlayerOWon;
                    m_InputOutput.DeclareGameResult(io_Status);
                    endConditionMet = true;
                }
                else if (i_Sign.Equals(eBoardSigns.O))
                {
                    io_Status = eEndGameStatus.PlayerXWon;
                    m_InputOutput.DeclareGameResult(io_Status);
                    endConditionMet = true;
                }
            }
            else if (m_Game.CheckIfBoardFilled())
            {
                io_Status = eEndGameStatus.Tie;
                m_InputOutput.DeclareGameResult(io_Status);
                endConditionMet = true;
            }

            return endConditionMet;
        }

        public void CreateBoard()
        {
            m_Board.Clear();
            int numColumns = m_MatrixSideSize;
            int numRows = m_MatrixSideSize;

            BoardUpperLine(numColumns);

            for (int i = 0; i < numRows; i++)
            {
                BoardCellsLine(numColumns, i);
                if (i != numRows - 1)
                {
                    BoardHorizontalSeparatorsLine(numColumns);
                }
            }

            m_InputOutput.PrintBoard(m_Board);
        }

        private void BoardUpperLine(int i_NumColumns)
        {
            m_Board.Append("   ");
            for (int i = 1; i <= i_NumColumns; i++)
            {
                m_Board.AppendFormat(" {0}  ", i);
            }

            m_Board.Append(Environment.NewLine);
        }

        private void BoardCellsLine(int i_NumColumns, int i_RowNumber)
        {
            m_Board.AppendFormat("  {0}", i_RowNumber + 1);
            for (int i = 0; i < i_NumColumns; i++)
            {
                if (i != 0)
                {
                    char value = (char)m_GameBoard.GetSignOfCell(i, i_RowNumber);
                    m_Board.AppendFormat("| {0} ", value);
                }
                else
                {
                    char value = (char)m_GameBoard.GetSignOfCell(i, i_RowNumber);
                    m_Board.AppendFormat(" {0} ", value);
                }
            }

            m_Board.Append(Environment.NewLine);
        }

        private void BoardHorizontalSeparatorsLine(int i_NumColumns)
        {
            m_Board.Append("   ");
            for (int i = 1; i <= i_NumColumns; i++)
            {
                m_Board.Append("----");
            }

            m_Board.Append(Environment.NewLine);
        }
    }
}
