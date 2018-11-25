using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
    class PlayerSeries : PinfallMeasure
    {
        private Player _player;

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
    }
}
