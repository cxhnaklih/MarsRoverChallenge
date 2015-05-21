using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverTechnicalChallenge
{
    public class OutOfGridBoundsException: SystemException
    {
        string _message;

        public OutOfGridBoundsException(string Message)
        {
            _message = Message;
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
