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
    public class MarsRoverTests
    {
        MarsPlateau _plateau = new MarsPlateau(10, 10);

        #region "North Tests"
        [Test]
        public void MarsRoverPointingNorth_MoveForwards_IncrementYDirectionBy1()
        {

            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.North);
            rover.GoForward();
            Assert.True(rover.Grid.Y == 1);

        }

        [Test]
        public void MarsRoverPointingNorth_TurnRight_PointsEast()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.North);
            rover.TurnRight();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.East);
        }

        [Test]
        public void MarsRoverPointingNorth_TurnLeft_PointsWest()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.North);
            rover.TurnLeft();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.West);
        }

        #endregion

        #region "South Tests"
        [Test]
        public void MarsRoverPointingSouth_MoveForwards_DecrementYDirectionBy1()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 1, MarsRoverDirection.South);
            rover.GoForward();
            Assert.True(rover.Grid.Y == 0);

        }

        [Test]
        public void MarsRoverPointingSouth_TurnRight_PointsWest()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.South);
            rover.TurnRight();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.West);
        }

        [Test]
        public void MarsRoverPointingSouth_TurnLeft_PointsEast()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.South);
            rover.TurnLeft();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.East);
        }

        #endregion

        #region "East Tests"
        [Test]
        public void MarsRoverPointingEast_MoveForwards_IncrementXDirectionBy1()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.East);
            rover.GoForward();
            Assert.True(rover.Grid.X == 1);
        }

        [Test]
        public void MarsRoverPointingEast_TurnRight_PointsSouth()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.East);
            rover.TurnRight();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.South);
        }

        [Test]
        public void MarsRoverPointingSouth_TurnLeft_PointsNorth()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.East);
            rover.TurnLeft();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.North);
        }
        #endregion

        #region "West Tests"
        [Test]
        public void MarsRoverPointingWest_MoveForwards_DecrementXDirectionBy1()
        {
            MarsRover rover = new MarsRover(_plateau, 1, 0, MarsRoverDirection.West);
            rover.GoForward();
            Assert.True(rover.Grid.X == 0);

        }

        [Test]
        public void MarsRoverPointingWest_TurnRight_PointsNorth()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.West);
            rover.TurnRight();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.North);
        }

        [Test]
        public void MarsRoverPointingWest_TurnLeft_PointsSouth()
        {
            MarsRover rover = new MarsRover(_plateau, 0, 0, MarsRoverDirection.West);
            rover.TurnLeft();
            Assert.True(rover.CurrentDirection == MarsRoverDirection.South);
        }
        #endregion
    }
}
