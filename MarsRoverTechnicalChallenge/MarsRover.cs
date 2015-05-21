using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    public class MarsRover
    {
        MarsRoverGrid _grd;
        MarsRoverTextCommandParser _parser;
        IMarsRoverState _currentState;
        IMarsRoverState _pointingNorth;
        IMarsRoverState _pointingSouth;
        IMarsRoverState _pointingEast;
        IMarsRoverState _pointingWest;

        

        public MarsRover(MarsPlateau plateau, int xpos, int ypos, MarsRoverDirection initialDirection )
        {
            _grd = new MarsRoverGrid(plateau);
            _grd.X = xpos;
            _grd.Y = ypos;
            _pointingNorth = new MarsRoverStatePointingNorth(this);
            _pointingSouth = new MarsRoverStatePointingSouth(this);
            _pointingEast = new MarsRoverStatePointingEast(this);
            _pointingWest = new MarsRoverStatePointingWest(this);
            setDirection(initialDirection);
            _parser = new MarsRoverTextCommandParser(this);
           

        }

        public MarsRoverTextCommandParser Commands
        {
            get
            {
                return _parser;
            }
        }

        private void setDirection(MarsRoverDirection direction)
        {
            switch (direction)
            {
                case MarsRoverDirection.North:
                    setState(_pointingNorth);
                    break;
                case MarsRoverDirection.South:
                    setState(_pointingSouth);
                    break;
                case MarsRoverDirection.East:
                    setState(_pointingEast);
                    break;
                case MarsRoverDirection.West:
                    setState(_pointingWest);
                    break;
            }
        }

        public MarsRoverGrid Grid
        {
            get
            {
                return _grd;
            }
        }
        
        public void TurnRight()
        {
            _currentState.TurnRight();
        }

        public void TurnLeft()
        {
            _currentState.TurnLeft();
        }

        public void GoForward()
        {
            _currentState.GoForward();
        }

        public MarsRoverDirection CurrentDirection
        {
            get
            {
                return _currentState.CurrentDirection;
            }
            set
            {
                setDirection(value);
            }
        }

        public MarsRoverPosition CurrentPosition
        {
            get
            {
                return new MarsRoverPosition { X = this.Grid.X, Y = this.Grid.Y, Direction = Constants.MarsRoverDirectionInText[(int)this.CurrentDirection] };
            }
        }

        #region "State Design Pattern Implementation"
        internal IMarsRoverState getMarsRoverPointingNorthState()
        {
            return _pointingNorth;
        }

        internal IMarsRoverState getMarsRoverPointingEastState()
        {
            return _pointingEast;
        }

        internal IMarsRoverState getMarsRoverPointingWestState()
        {
            return _pointingWest;
        }

        internal IMarsRoverState getMarsRoverPointingSouthState()
        {
            return _pointingSouth;
        }

        internal void setState(IMarsRoverState state)
        {
            _currentState = state;
        }
        #endregion
    }
}
