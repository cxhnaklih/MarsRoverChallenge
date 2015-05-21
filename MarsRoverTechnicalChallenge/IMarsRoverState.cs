using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    interface IMarsRoverState
    {
        void GoForward();
        void TurnLeft();
        void TurnRight();
        MarsRoverDirection CurrentDirection { get; }
    }
}
