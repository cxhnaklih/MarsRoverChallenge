using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoverChallengeWebApi.Models;

namespace MarsRoverChallengeWebApi.Tests
{
    class TestPlateauDbSet: TestDbSet<MarsRoverChallengeWebApi.Models.PlateauModel>
    {
        public TestPlateauDbSet()
        {
           
        }

        public override PlateauModel Find(params object[] keyValues)
        {
            return this.SingleOrDefault(plateau => plateau.PlateauId == (int)keyValues.Single());
        }
    }
}
