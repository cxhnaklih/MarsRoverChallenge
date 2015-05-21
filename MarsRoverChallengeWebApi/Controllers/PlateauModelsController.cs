using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MarsRoverChallengeWebApi.Models;

namespace MarsRoverChallengeWebApi.Controllers
{
    /// <summary>A Plateau defines the area within which a mars explorer can move.  
    /// YOU MUST HAVE A PLATEAU TO CREATE A MARS EXPLORER ROBOT</summary>
    public class PlateauModelsController : ApiController
    {
        private IDataStore db = new ApplicationDbContext();

        public PlateauModelsController() { }

        public PlateauModelsController(IDataStore mockStore)
        {
            db = mockStore;
        }

        /// <summary>Retrieves a list of available plateaus</summary>
        // GET: api/marsrover/plateau
        public IQueryable<PlateauModel> GetPlateauModels()
        {
            return db.PlateauModels;
        }

        /// <summary>Retrieves a plateau with the specified id</summary>
        // GET: api/marsrover/plateau/5
        [ResponseType(typeof(PlateauModel))]
        public IHttpActionResult GetPlateauModel(int plateauId)
        {
            PlateauModel plateauModel = db.PlateauModels.Find(plateauId);
            if (plateauModel == null)
            {
                return NotFound();
            }

            return Ok(plateauModel);
        }

        /// <summary>Updates a plateau with the specified id</summary>
        // PUT: api/marsrover/plateau/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlateauModel(int plateauId, PlateauModel plateauModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (plateauId != plateauModel.PlateauId)
            {
                return BadRequest();
            }

            db.Entry(plateauModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlateauModelExists(plateauId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Checks whether the  name you want to assign to your plateau already exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        
        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/marsrover/plateauNameAvailable/{name}")]
        public IHttpActionResult PlateauNameExists(string name)
        {
            
            return Ok(plateauNameExists(name));
            
        }

        private bool plateauNameExists(string plateauModelName)
        {
            return (from m in db.PlateauModels where m.Name.ToUpper() == plateauModelName.ToUpper() select m).Count() > 0;
        }

        
        /// <summary>Creates a new plateau</summary>
        // POST: api/marsrover/plateau
        [ResponseType(typeof(PlateauModel))]
        public IHttpActionResult PostPlateauModel(PlateauModel plateauModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Check that the name doesn't already exist

                if (plateauNameExists(plateauModel.Name))
                {
                    return BadRequest("That Plateau Model Name Already Exists");
                }
            
            db.PlateauModels.Add(plateauModel);
            db.SaveChanges();

            return CreatedAtRoute("PlateauModelsRoute", new { id = plateauModel.PlateauId }, plateauModel);
        }


        /// <summary>Removes a plateau from the list of available plateaus</summary>
        // DELETE: api/marsrover/plateau/5
        [ResponseType(typeof(PlateauModel))]
        public IHttpActionResult DeletePlateauModel(int plateauId)
        {
            PlateauModel plateauModel = db.PlateauModels.Find(plateauId);
            if (plateauModel == null)
            {
                return NotFound();
            }

            db.PlateauModels.Remove(plateauModel);
            foreach(var x in db.MarsRoverModels.Where(r => r.PlateauId == plateauId))
            {
                db.MarsRoverModels.Remove(x);
            }


            db.SaveChanges();

            return Ok(plateauModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlateauModelExists(int id)
        {
            return db.PlateauModels.Count(e => e.PlateauId == id) > 0;
        }
    }
}