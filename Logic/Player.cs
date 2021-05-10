using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic
{
    public class Player
    {
        public eBoardSigns Sign { get; }
        public int Score { get; set; }
        public ePlayerType PlayerType { get; set; }

        public Player(eBoardSigns i_DesiredSign, ePlayerType i_PlayerType)
        {
            this.Sign = i_DesiredSign;
            this.PlayerType = i_PlayerType;
            this.Score = 0;
        }

        public void IncreaseScoreByOne()
        {
            Score += 1;
        }
    }
}