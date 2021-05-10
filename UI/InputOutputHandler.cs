using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace UI
{
    class InputOutputHandler
    {
        public int AskForMatrixSize()
        {
            bool valid = false;
            int userInput = 0;
            while (!valid)
            {
                Console.WriteLine("Choose Board size (Enter a single digit between 3-9): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out userInput))
                {
                    if (userInput >= 3 && userInput <= 9)
                    {
                        valid = true;
                    }
                    else
                    {
                        Console.WriteLine("Illegal board Size.");
                    }
                }
                else
                {
                    Console.WriteLine("Illegal board Size.");
                }
            }

            return userInput;
        }

        public char AskForPlayerOType()
        {
            char opponentType = '\0';
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Who do you want to play against? Enter 'c' for computer and 'h' for human: ");
                string input = Console.ReadLine();
                if (char.TryParse(input, out opponentType))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Illegal character entered.");
                }

            }

            return opponentType;
        }

        public void PrintBoard(StringBuilder board)
        {
            Console.WriteLine(board);
        }

        public char AskForPlayingAgain()
        {
            bool valid = false;
            char answer = '\0';
            while (!valid)
            {
                Console.WriteLine("Do you want to play another round? y/n");
                string input = Console.ReadLine();
                if (char.TryParse(input, out answer))
                {
                    if (answer == 'y' || answer == 'n')
                        valid = true;
                }
            }

            return answer;
        }

        public void AskPlayerForChosenCell(ref PlayerTurnInfo io_PlayerTurnInput)
        {
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine("Enter the number of row, then press enter, and enter column of the chosen cell. You can quit anytime by pressing 'q'");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int cellRow))
                {
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int cellColumn))
                    {
                        valid = true;
                        io_PlayerTurnInput.CellRow = cellRow-1;
                        io_PlayerTurnInput.CellColumn = cellColumn-1;
                    }
                    else if (char.TryParse(input, out char quitGameMark))
                    {
                        if (quitGameMark == 'q')
                        {
                            valid = true;
                            io_PlayerTurnInput.PlayerWantsToQuit = true;
                        }
                        else
                        {
                            InvalidInput();
                        }
                    }
                    else
                    {
                        InvalidInput();
                    }
                }
                else if (char.TryParse(input, out char quitGameMark))
                {
                    if (quitGameMark == 'q')
                    {
                        valid = true;
                        io_PlayerTurnInput.PlayerWantsToQuit = true;
                    }
                    else
                    {
                        InvalidInput();
                    }
                }
                else
                {
                    InvalidInput();
                }

            }
        }

        public void DeclareGameResult(eEndGameStatus i_Status)
        {
            if(i_Status.Equals(eEndGameStatus.PlayerXQuit))
            {
                Console.WriteLine("You decided to quit. Player O's score grew by one.");
            }
            else if(i_Status.Equals(eEndGameStatus.PlayerXWon))
            {
                Console.WriteLine("Player X Won!");
            }
            else if(i_Status.Equals(eEndGameStatus.PlayerOWon))
            {
                Console.WriteLine("Player O Won!");
            }
            else if(i_Status.Equals(eEndGameStatus.Tie))
            {
                Console.WriteLine("Its a tie!");
            }
        }

        public void OccupiedCell(int i_NumCol, int i_NumRow)
        {
            Console.WriteLine("The cell in row: {0}, column: {1} is occupied.", i_NumRow, i_NumCol);
        }

        public void InvalidChosenCoordinates()
        {
            Console.WriteLine("Chosen coordinates are out of the board borders.");
        }

        public void ShowScores(Player i_PlayerX, Player i_PlayerO)
        {
            Console.WriteLine("Score Board: Player X-> {0} , Player O-> {1}",i_PlayerX.Score, i_PlayerO.Score);
        }
        public void InvalidInput()
        {
            Console.WriteLine("Illegal input entered.");
        }
    }

}

