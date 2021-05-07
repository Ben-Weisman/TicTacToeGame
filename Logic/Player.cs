using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic
{
    class Player
    {
        private readonly char m_Sign;
        private int m_score;
        public int Score
        {
            get;
            set;
        }
        public Player(char i_DesiredSign)
        {
            this.m_Sign = i_DesiredSign;
            Score = 0;
        }
    }
}