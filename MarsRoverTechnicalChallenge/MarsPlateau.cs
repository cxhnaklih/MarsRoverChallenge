using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    public class MarsPlateau
    {
        private uint _length;
        private uint _width;

        public MarsPlateau(uint width, uint length)
        {
            _width = width;
            _length = length;
            
        }

        public uint Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public uint Width
        {
            get { return _width; }
            set { _width = value; }
        }
    }
}
