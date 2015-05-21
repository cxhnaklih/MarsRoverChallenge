using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    class MarsRoverStatePointingSouth : IMarsRoverState
    {
        
        MarsRover _rover;

        public MarsRoverStatePointingSouth(MarsRover rover)
        {
            _rover = rover;
        }

        void IMarsRoverState.GoForward()
        {
            _rover.Grid.Y = _rover.Grid.Y - 1;
        }

        void IMarsRoverState.TurnLeft()
        {
            _rover.setState(_rover.getMarsRoverPointingEastState());
        }

        void IMarsRoverState.TurnRight()
        {
            _rover.setState(_rover.getMarsRoverPointingWestState());
        }

        MarsRoverDirection IMarsRoverState.CurrentDirection
        {
            get { return MarsRoverDirection.South; }
        }
    }
}
