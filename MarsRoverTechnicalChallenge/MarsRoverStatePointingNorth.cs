using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    public class MarsRoverStatePointingNorth: IMarsRoverState
    {
        MarsRover _rover;

        public MarsRoverStatePointingNorth(MarsRover rover)
        {
            _rover = rover;
        }


        void IMarsRoverState.GoForward()
        {
            _rover.Grid.Y = _rover.Grid.Y + 1;
        }

        void IMarsRoverState.TurnLeft()
        {
            _rover.setState(_rover.getMarsRoverPointingWestState());
            
        }

        void IMarsRoverState.TurnRight()
        {
            _rover.setState(_rover.getMarsRoverPointingEastState());
        }

        MarsRoverDirection IMarsRoverState.CurrentDirection
        {
            get { return MarsRoverDirection.North; }
        }
    }
}
