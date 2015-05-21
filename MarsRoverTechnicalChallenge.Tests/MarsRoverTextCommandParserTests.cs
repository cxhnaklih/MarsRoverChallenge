using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarsRoverTechnicalChallenge;

namespace MarsRoverTechnicalChallenge.Tests
{
    [TestFixture]
    public class MarsRoverTextCommandParserTests
    {
        
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void MarsRoverTextCommandParser_ParseSendInvalidCharacters_ThrowsException()
        {
            MarsPlateau plateau = new MarsPlateau(10, 10);
            MarsRoverTextCommandParser parser = new MarsRoverTextCommandParser(new MarsRover(plateau,0,0, MarsRoverDirection.North));
            parser.ParseCommand("LLXXMMM");

        }

        [Test]
        public void MarsRoverTextCommandParser_ParseSendValidCharactersInBounds_Returns13N()
        {
            MarsPlateau plateau = new MarsPlateau(5, 5);
            MarsRoverTextCommandParser parser = new MarsRoverTextCommandParser(new MarsRover(plateau, 1, 2, MarsRoverDirection.North));
            MarsRoverPosition result = parser.ParseCommand("LMLMLMLMM");
            Assert.True(result.Direction == 'N');
            Assert.True(result.X == 1);
            Assert.True(result.Y == 3);

        }

        [Test]
        public void MarsRoverTextCommandParser_TryParseSendInvalidCharacters_ReturnsFalse()
        {
            MarsPlateau plateau = new MarsPlateau(10, 10);
            MarsRoverTextCommandParser parser = new MarsRoverTextCommandParser(new MarsRover(plateau, 0, 0, MarsRoverDirection.North));
            string successfulCommands = "";
            bool result =parser.TryParse("LLXXM", out successfulCommands);
            Assert.True(result == false);
            Assert.True(successfulCommands == "LL");
        }

        [Test]
        public void MarsRoverTextCommandParser_TryParseSendValidCharactersInBounds_ReturnsTrue()
        {
            MarsPlateau plateau = new MarsPlateau(5, 5);
            MarsRoverTextCommandParser parser = new MarsRoverTextCommandParser(new MarsRover(plateau, 1, 2, MarsRoverDirection.North));
            string successfulCommands = "";
            bool result = parser.TryParse("LMLMLMLMM", out successfulCommands);
            Assert.True(result);
            Assert.True(successfulCommands == "LMLMLMLMM");

        }

        [Test]
        public void MarsRoverTextCommandParser_TryParseSendValidCharactersOutOfBoundsLength_ReturnsFalse()
        {
            MarsPlateau plateau = new MarsPlateau(2, 2);
            MarsRoverTextCommandParser parser = new MarsRoverTextCommandParser(new MarsRover(plateau, 0, 0, MarsRoverDirection.North));
            string successfulCommands = "";
            bool result = parser.TryParse("MMM", out successfulCommands);
            Assert.True(result==false);
            Assert.True(successfulCommands == "MM");

        }

        [Test]
        public void MarsRoverTextCommandParser_TryParseSendValidCharactersOutOfBoundsWidth_ReturnsFalse()
        {
            MarsPlateau plateau = new MarsPlateau(2, 2);
            MarsRoverTextCommandParser parser = new MarsRoverTextCommandParser(new MarsRover(plateau, 0, 0, MarsRoverDirection.East));
            string successfulCommands = "";
            bool result = parser.TryParse("MMM", out successfulCommands);
            Assert.True(result == false);
            Assert.True(successfulCommands == "MM");

        }




    }
}
