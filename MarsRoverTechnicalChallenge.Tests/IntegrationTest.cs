using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarsRoverTechnicalChallenge.Tests
{
    [TestFixture]
    class IntegrationTest
    {
        MarsPlateau _plateau = new MarsPlateau(5, 5);

        [Test]
        public void MarsRover_FirstTestInput_13N()
        {
            
            MarsRover rover = new MarsRover(_plateau, 1, 2, MarsRoverDirection.North);
            rover.TurnLeft(); //L
            rover.GoForward();//M
            rover.TurnLeft(); //L
            rover.GoForward();//M
            rover.TurnLeft(); //L
            rover.GoForward();//M
            rover.TurnLeft(); //L
            rover.GoForward();//M
            rover.GoForward();//M
            Assert.True(rover.Grid.X == 1);
            Assert.True(rover.Grid.Y ==3);
            Assert.True(rover.CurrentDirection == MarsRoverDirection.North);
        }

        [Test]
        public void MarsRover_SecondTestInput_51E()
        {
            
            MarsRover rover = new MarsRover(_plateau, 3, 3, MarsRoverDirection.East);
            rover.GoForward();//M
            rover.GoForward();//M
            rover.TurnRight();//R
            rover.GoForward();//M
            rover.GoForward();//M
            rover.TurnRight();//R
            rover.GoForward();//M
            rover.TurnRight();//R
            rover.TurnRight();//R
            rover.GoForward();//M

            Assert.True(rover.Grid.X == 5);
            Assert.True(rover.Grid.Y == 1);
            Assert.True(rover.CurrentDirection == MarsRoverDirection.East);
        }
    }
}
