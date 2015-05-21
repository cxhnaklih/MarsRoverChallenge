using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    class MarsRoverStatePointingEast : IMarsRoverState
    {
        
        MarsRover _rover;

        public MarsRoverStatePointingEast(MarsRover rover)
        {
            _rover = rover;
        }

        void IMarsRoverState.GoForward()
        {
            _rover.Grid.X = _rover.Grid.X + 1;
        }

        void IMarsRoverState.TurnLeft()
        {
            _rover.setState(_rover.getMarsRoverPointingNorthState());
        }

        void IMarsRoverState.TurnRight()
        {
            _rover.setState(_rover.getMarsRoverPointingSouthState());
        }

        MarsRoverDirection IMarsRoverState.CurrentDirection
        {
            get { return MarsRoverDirection.East; }
        }
    }
}
