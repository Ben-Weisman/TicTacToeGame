using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public struct PlayerTurnInput
    {
        private int m_CellRow;
        private int m_CellColumn;
        private bool m_PlayerWantsToQuit;

        public int CellRow
        {
            get;
            set;
        }
        public int CellColumn
        {
            get;
            set;
        }
        public Boolean PlayerWantsToQuit
        {
            get;
            set;
        }
    }
}
