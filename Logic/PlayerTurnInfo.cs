using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public struct PlayerTurnInfo
    {
        public int CellRow { get; set; }
        public int CellColumn { get; set; }
        public bool PlayerWantsToQuit { get; set; }
    }
}
