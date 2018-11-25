using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingManagement
{
    class ContestScore
    {

        private PinfallMeasure _homePinfallMeasure;
        private PinfallMeasure _awayPinfallMeasure;

        private short _scratchPointsToAssign;
        private short _hcapPointsToAssign;

        public short ScratchPointsToAssign
        {
            get { return _scratchPointsToAssign; }
            set { _scratchPointsToAssign = value; }
        }

        public short HcapPointsToAssign
        {
            get { return _hcapPointsToAssign; }
            set { _hcapPointsToAssign = value; }
        }

        public short HomePointsScratch
        {
            get
            {
                if (_homePinfallMeasure.ScratchPinfall > _awayPinfallMeasure.ScratchPinfall)
                {
                    return _scratchPointsToAssign;
                }
                if (_homePinfallMeasure.ScratchPinfall < _awayPinfallMeasure.ScratchPinfall)
                {
                    return 0;
                }
                return (short)(_scratchPointsToAssign / 2);
            }
        }

        public short AwayPointsScratch
        {
            get
            {
                if (_awayPinfallMeasure.ScratchPinfall > _homePinfallMeasure.ScratchPinfall)
                {
                    return _scratchPointsToAssign;
                }
                if (_awayPinfallMeasure.ScratchPinfall < _homePinfallMeasure.ScratchPinfall)
                {
                    return 0;
                }
                return (short)(_scratchPointsToAssign / 2);
            }
        }

        public short HomePointsHcap
        {
            get
            {
                if (_homePinfallMeasure.HcapPinfall > _awayPinfallMeasure.HcapPinfall)
                {
                    return _hcapPointsToAssign;
                }
                if (_homePinfallMeasure.HcapPinfall < _awayPinfallMeasure.HcapPinfall)
                {
                    return 0;
                }
                return (short)(_hcapPointsToAssign / 2);
            }
        }

        public short AwayPointsHcap
        {
            get
            {
                if (_awayPinfallMeasure.HcapPinfall > _homePinfallMeasure.HcapPinfall)
                {
                    return _hcapPointsToAssign;
                }
                if (_awayPinfallMeasure.HcapPinfall < _homePinfallMeasure.HcapPinfall)
                {
                    return 0;
                }
                return (short)(_hcapPointsToAssign / 2);
            }
        }

    }
}
