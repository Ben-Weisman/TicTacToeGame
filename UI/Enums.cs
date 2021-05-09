using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public enum PlayerType
    {
        Computer,
        Human
    }

    public enum EndGameStatus
    {
        UserWon,
        OpponentWon,
        Tie,
        UserQuit
    }

    public enum PlayerAnswer
    {
        Yes,
        No
    }
}
