using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class UIManager
    {
    }


    /////////////////////////////////////
    /*class GameManager
    {
        private TicTacToeGame.TicTacToeGame m_Game;
        private int m_UserScore;
        private int m_OpponentScore;
        private enum OpponentType
        {
            Computer,
            Human
        }

        private OpponentType m_OpponentType;

        private int m_MatrixSideSize;
        public int MatrixSideSize
        {
            get { return m_MatrixSideSize; }
            set
            {
                if (value >= 3 && value <= 9)
                {
                    m_MatrixSideSize = value;
                }
                else Console.WriteLine("Illegal Matrix size entered.");
            }
        }// Always square number of cells.
        public void StartGame()
        {
            //Show menu
            // Get the matrix size from the user.
            // Decide who to play against (human \ computer).
            // Call StartTicTacToeGame().

            // m_Game.BuildBoard(m_MatrixSideSize);
        }

        private void ShowMenu()
        {
            Boolean valid = false;
            while (!valid)
            {
                valid = SetMatrixSize();
            }
            valid = false;
            while (!valid)
            {
                valid = SetOpponentType();
            }
            //ask ben if show menu has to print menu itself **
        }

        private Boolean SetMatrixSize()
        {
            Boolean valid;
            Console.WriteLine("Choose Board size (Enter digit Between 3-9)");
            if (int.TryParse(Console.ReadLine(), out int res))
            {
                if (res >= 3 && res <= 9)
                {
                    MatrixSideSize = res;
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        private Boolean SetOpponentType()
        {
            Boolean valid;
            Console.WriteLine("Who do you want to play against? Enter 'c' for computer and 'h' for human");
            if (char.TryParse(Console.ReadLine(), out char opponent))
            {
                if (opponent == 'c')
                {
                    m_OpponentType = OpponentType.Computer;
                    valid = true;
                }
                else if (opponent == 'h')
                {
                    m_OpponentType = OpponentType.Human;
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }
    }*/
    ///////////////////////////////////
}
