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
        private Player m_Player1;
        private Player m_Player2;
        private Board m_GameBoard;
        private readonly InputOutputHandler m_InputOutput;  //check that readonly
        private StringBuilder m_Board;
        private PlayerType m_PlayerType;
        private int m_MatrixSideSize;

        public UIManager()
        {
            m_Player1 = new Player(eBoardSigns.X);
            m_Player2 = new Player(eBoardSigns.O);
            m_InputOutput = new InputOutputHandler();
            m_Board = new StringBuilder();
            m_MatrixSideSize = 0;
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
            m_Game = new GameLogic(m_GameBoard, m_Player1, m_Player2);
            m_PlayerType = GetOpponentType();
        }
        
        public void GameManager()
        {
            EndGameStatus gameStatus;
            PlayerAnswer playerAnswer = PlayerAnswer.Yes;

            while (playerAnswer == PlayerAnswer.Yes)
            {
                gameStatus = PlayingGame();
                if (gameStatus == EndGameStatus.UserWon)
                {
                    m_Player1.Score += 1;
                }
                else if (gameStatus == (EndGameStatus.OpponentWon | EndGameStatus.UserQuit))
                {
                    m_Player2.Score += 1;
                }
                m_GameBoard.ClearBoard();
                m_InputOutput.ShowScores(m_Player1, m_Player2);
                playerAnswer = PlayAgain();
            }
            //exit(0);
        }
        public EndGameStatus PlayingGame()
        {
            EndGameStatus status = 0;
            bool endCondition = false;
            PlayerTurnInput playerTurnInput = new PlayerTurnInput();
            CreateBoard();
            while (!endCondition)
            {
                HumanPlayerTurn(eBoardSigns.X, ref playerTurnInput, ref status);
                endCondition = FindEndConditions(playerTurnInput, ref status, eBoardSigns.X);
                if (endCondition) { break; }
                if (m_PlayerType == PlayerType.Human)
                {
                    HumanPlayerTurn(eBoardSigns.O, ref playerTurnInput, ref status);
                }
                else
                {
                    ComputerPlayerTurn(eBoardSigns.O, ref playerTurnInput);
                }
                endCondition = FindEndConditions(playerTurnInput, ref status, eBoardSigns.O);
            }
            return status;
        }

        public void HumanPlayerTurn(eBoardSigns i_Sign, ref PlayerTurnInput io_PlayerTurnInput, ref EndGameStatus io_Status)
        {
            m_InputOutput.AskPlayerForChosenCell(ref io_PlayerTurnInput);
            int numRow = io_PlayerTurnInput.CellRow;
            int numCol = io_PlayerTurnInput.CellColumn;
            if (io_PlayerTurnInput.PlayerWantsToQuit)
            {
                io_Status = EndGameStatus.UserQuit;
            }
            else
            {
                bool valid = false;
                while (!valid)
                {
                    if (eBoardSigns.Blank == m_GameBoard.GetSignOfCell(numCol - 1, numRow - 1))
                    {
                        m_GameBoard.MarkCell(i_Sign, numCol - 1, numRow - 1);
                        Console.Clear(); //*
                        CreateBoard();
                        valid = true;
                    }
                    else
                    {
                        m_InputOutput.OccupiedCell(numCol, numRow);
                    }
                }
            }
        }

        public void ComputerPlayerTurn(eBoardSigns i_Sign, ref PlayerTurnInput io_PlayerTurnInput)
        {
            m_Game.GenerateComputerMove(i_Sign);
            Console.Clear(); //*
            CreateBoard();
        }

        public PlayerAnswer PlayAgain()
        {
            bool valid = false;
            PlayerAnswer playerAnswer = 0;
            while (!valid)
            {
                char answer = m_InputOutput.AskForPlayingAgain();
                if (answer == 'y')
                {
                    playerAnswer = PlayerAnswer.Yes;
                    valid = true;
                }
                else if (answer == 'n')
                {
                    playerAnswer = PlayerAnswer.No;
                    valid = true;
                }
                else
                {
                    m_InputOutput.InvalidInput();
                }
            }

            return playerAnswer;
        }

        public PlayerType GetOpponentType()
        {
            bool valid = false;
            PlayerType opponentType = 0;

            while (!valid)
            {
                char playerInput = m_InputOutput.AskForOpponentType();
                if (playerInput == 'h')
                {
                    opponentType = PlayerType.Human;
                    valid = true;
                }
                else if (playerInput == 'c')
                {
                    opponentType = PlayerType.Computer;
                    valid = true;
                }
                else
                {
                    m_InputOutput.InvalidInput();
                }
            }

            return opponentType;
        }

        public bool FindEndConditions(PlayerTurnInput io_PlayerTurnInput, ref EndGameStatus io_Status, eBoardSigns i_Sign)
        {
            bool endConditionMet = false;
            if (io_PlayerTurnInput.PlayerWantsToQuit)
            {
                m_InputOutput.DeclareGameResult(io_Status);
                io_Status = EndGameStatus.UserQuit;
                endConditionMet = true;
            }
            else if (m_Game.CheckForWinner(io_PlayerTurnInput.CellColumn-1, io_PlayerTurnInput.CellRow-1, i_Sign))
            {
                if (i_Sign == eBoardSigns.X)
                {
                    m_InputOutput.DeclareGameResult(io_Status);
                    io_Status = EndGameStatus.UserWon;
                    endConditionMet = true;
                }
                else if (i_Sign == eBoardSigns.O)
                {
                    m_InputOutput.DeclareGameResult(io_Status);
                    io_Status = EndGameStatus.OpponentWon;
                    endConditionMet = true;
                }
            }
            else if (m_Game.CheckIfBoardFilled())
            {
                m_InputOutput.DeclareGameResult(io_Status);
                io_Status = EndGameStatus.Tie;
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
                    m_Board.AppendFormat("| {0} ", m_GameBoard.GetSignOfCell(i, i_RowNumber));
                }
                else
                {
                    m_Board.AppendFormat(" {0} ", m_GameBoard.GetSignOfCell(i, i_RowNumber));
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
