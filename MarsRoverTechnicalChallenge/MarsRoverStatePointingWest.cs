using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    class MarsRoverStatePointingWest :IMarsRoverState
    {
        
        MarsRover _rover;

        public MarsRoverStatePointingWest(MarsRover rover)
        {
            _rover = rover;
        }

        void IMarsRoverState.GoForward()
        {
            _rover.Grid.X = _rover.Grid.X - 1;
        }

        void IMarsRoverState.TurnLeft()
        {
            _rover.setState(_rover.getMarsRoverPointingSouthState());
        }

        void IMarsRoverState.TurnRight()
        {
            _rover.setState(_rover.getMarsRoverPointingNorthState());
        }

        MarsRoverDirection IMarsRoverState.CurrentDirection
        {
            get { return MarsRoverDirection.West; }
        }
    }
}
