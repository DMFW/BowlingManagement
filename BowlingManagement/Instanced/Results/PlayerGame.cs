using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
    class PlayerGame : PinfallMeasure
    {
        private short _gameNo;
        private Player _player;

        public short GameNo
        {
            get { return _gameNo; }
            set { _gameNo = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

    }
}
