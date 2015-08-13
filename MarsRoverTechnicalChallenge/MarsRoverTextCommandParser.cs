using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MarsRoverTechnicalChallenge
{
    public class MarsRoverTextCommandParser
    {
        MarsRover _rover;
        StringBuilder _successfulMoves = new StringBuilder();


        public MarsRoverTextCommandParser(MarsRover rover)
        {
            _rover = rover;
        }

        public bool TryParse(string command, out string successfulCommands)
        {
            bool success = true;
            int invalidCharsLocation = LocateInvalidCharacters(command);
            if (invalidCharsLocation >-1 && command.Length >0)
            {
                success = false;
                command = command.Substring(0, invalidCharsLocation);
                invalidCharsLocation = LocateInvalidCharacters(command);
            }

            try
            {
                if(command.Length > 0)
                    ParseCommand(command);
                
            }
            catch(Exception)
            {
                
                success = false;
            }
            
            successfulCommands = _successfulMoves.ToString();
            return success;
            
        }

        private int LocateInvalidCharacters(string command)
        {
            //Create a regular expression to screen possible commands in this case not L, R or M One or more times
            Regex rejectedCharacters = new Regex("[^LRM]+");
            // Make everything upper Case for the regex comparison
            command = command.ToUpper();
            

            if (rejectedCharacters.IsMatch(command))
            {
                return rejectedCharacters.Match(command).Index;
            }
            return -1;
        }

        public MarsRoverPosition ParseCommand(string command)
        {
            int invalidCharsLocation = LocateInvalidCharacters(command);

            if (invalidCharsLocation > -1) // if there are invalid characters in the command throw an argument exception
            {
                string errorPosition = command.Substring(0,invalidCharsLocation+1);
                throw new ArgumentException("The Command to the Rover must Only Contain L, R or M \nYour Command Has an invalid character here "+errorPosition);
            }
            else
            {
                _successfulMoves = new StringBuilder();
                // there are no invalid commands so we can run the rover 
                foreach (char c in command)
                {
                    switch (c)
                    {
                        case 'L':
                            _rover.TurnLeft();
                            break;
                        case 'M':
                            _rover.GoForward();
                            break;
                        case 'R':
                            _rover.TurnRight();
                            break;
                    }
                    _successfulMoves.Append(c);
                }
            }

            return _rover.CurrentPosition;
        }
    }
}
