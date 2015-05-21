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
    class MarsRoverGridTests 
    {
        MarsPlateau _plateau = new MarsPlateau(10, 10);

        [Test]
        [ExpectedException(typeof(OutOfGridBoundsException))]
        public void MarsRoverGrid_SetOutOfBoundsYDirection_ThrowsOutOfGridBoundsException()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.Y = 11;
        }

        [Test]
        [ExpectedException(typeof(OutOfGridBoundsException))]
        public void MarsRoverGrid_SetOutOfBoundsXDirection_ThrowsOutOfGridBoundsException()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.X = 11;
        }

        [Test]
        public void MarsRoverGrid_SetOnUpperBoundYDirection_AcceptsInput()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.Y = 10;
            Assert.True(grd.Y == 10);
        }

        [Test]
        public void MarsRoverGrid_SetOnUpperBoundXDirection_AcceptsInput()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.X = 10;
            Assert.True(grd.X == 10);
        }

        [Test]
        public void MarsRoverGrid_SetOnLowerBoundYDirection_AcceptsInput()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.Y = 0;
            Assert.True(grd.Y == 0);
        }

        [Test]
        public void MarsRoverGrid_SetOnLowerBoundXDirection_AcceptsInput()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.X = 0;
            Assert.True(grd.X == 0);
        }

        [Test]
        [ExpectedException(typeof(OutOfGridBoundsException))]
        public void MarsRoverGrid_SetNegativeValueYDirection_ThrowsOutOfGridBoundsException()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.Y = -1;
        }

        [Test]
        [ExpectedException(typeof(OutOfGridBoundsException))]
        public void MarsRoverGrid_SetNegativeValueXDirection_ThrowsOutOfGridBoundsException()
        {
            MarsRoverGrid grd = new MarsRoverGrid(_plateau);
            grd.X = -1;
        }
    }
}
