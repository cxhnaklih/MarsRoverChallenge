using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MarsRoverChallengeWebApi;
using MarsRoverChallengeWebApi.Models;
using System.Web.Http.Results;

namespace MarsRoverChallengeWebApi.Tests
{
    [TestFixture]
    public class PlateauTests
    {

        [Test]
        public void  PlateauModelsController_Get_ListsAllData()
        {
            var db = new MockDataStore();
            
            db.PlateauModels.Add(new PlateauModel() { Length = 10, PlateauId = 1, Width = 10 });
            db.PlateauModels.Add(new PlateauModel() { Length = 20, PlateauId = 2, Width = 20 });
            db.PlateauModels.Add(new PlateauModel() { Length = 30, PlateauId = 3, Width = 30 });
            db.PlateauModels.Add(new PlateauModel() { Length = 40, PlateauId = 4, Width = 40 });

            var controller = new MarsRoverChallengeWebApi.Controllers.PlateauModelsController(db);
            var result = controller.GetPlateauModels() as TestPlateauDbSet;
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Local.Count);
        }

        [Test]
        public void PlateauModelsController_Get_RetrievesASingleItem()
        {
            var db = new MockDataStore();

            db.PlateauModels.Add(new PlateauModel() { Length = 10, PlateauId = 1, Width = 10 });
            db.PlateauModels.Add(new PlateauModel() { Length = 20, PlateauId = 2, Width = 20 });
            db.PlateauModels.Add(new PlateauModel() { Length = 30, PlateauId = 3, Width = 30 });
            db.PlateauModels.Add(new PlateauModel() { Length = 40, PlateauId = 4, Width = 40 });

            var controller = new MarsRoverChallengeWebApi.Controllers.PlateauModelsController(db);
            var result = controller.GetPlateauModel(2) as OkNegotiatedContentResult<PlateauModel>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.PlateauId == 2);
            

        }

    }
}
