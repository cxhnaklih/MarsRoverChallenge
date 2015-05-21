using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    public class MarsRoverGrid
    {
        MarsPlateau _plateau;
        private int _y;
        private int _x;

        public MarsRoverGrid(MarsPlateau plateau)
        {
            _plateau = plateau;
        }

        
        public int X
        {
            get { return _x; }
            set {
                if (value <= _plateau.Width && value >= 0)
                    { 
                        _x = value;
                    }
                    else
                    {
                        throw new OutOfGridBoundsException("You have gone outside the bounds of the rover area in the X Direction");
                    }
                }
        }

        
        public int Y
        {
            get { return _y; }
            set {
                if (value <= _plateau.Length && value >= 0)
                    {
                        _y = value;
                    }
                    else
                    {
                        throw new OutOfGridBoundsException("You have gone outside the bounds of the rover area in the Y Direction");
                    }
                }
        }
        
        

    }
}
