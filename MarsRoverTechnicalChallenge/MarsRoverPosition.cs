using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    public struct MarsRoverPosition
    {
        public int X{get;set;}
        public int Y{get;set;}
        public char Direction { get; set; }

        public override string  ToString()
        {
            return string.Format("{0},{1}{2}", this.X, this.Y, this.Direction);
        }
    }
}
