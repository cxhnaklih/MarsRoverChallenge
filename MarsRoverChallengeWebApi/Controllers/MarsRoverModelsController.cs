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
using MarsRoverTechnicalChallenge;

namespace MarsRoverChallengeWebApi.Controllers
{
    ///<summary>Mars Rover Models allow you to interact and control mars rovers.  Each mars rover is associated with a particular plateau. </summary>
    public class MarsRoverModelsController : ApiController
    {
        private IDataStore db = new ApplicationDbContext();

        public MarsRoverModelsController() { }

        public MarsRoverModelsController(IDataStore mockStore)
        {
            db = mockStore;
        }

        ///<summary>Retrieves the list of mars rovers for a particular plateau</summary>      
        // GET: api/MarsRoverModels
        public IQueryable<MarsRoverModel> GetMarsRoverModels(int plateauid)
        {
            return db.MarsRoverModels.Where(r=> r.PlateauId == plateauid);
        }

        ///<summary>Retrieves a single mars rover from a particular plateau </summary>      
        // GET: api/MarsRoverModels/5
        [ResponseType(typeof(MarsRoverModel))]
        public IHttpActionResult GetMarsRoverModel(int plateauid, int roverid)
        {
            MarsRoverModel marsRoverModel = db.MarsRoverModels.Find(roverid);
            if (marsRoverModel == null)
            {
                return NotFound();
            }

            return Ok(marsRoverModel);
        }

        ///<summary>Updates a mars rovers from a particular plateau</summary>      
        // PUT: api/MarsRoverModels/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMarsRoverModel(int plateauid, int roverid, MarsRoverModel marsRoverModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (roverid != marsRoverModel.MarsRoverId)
            {
                return BadRequest();
            }

            db.Entry(marsRoverModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarsRoverModelExists(roverid))
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

        ///<summary>Creates a new mars rover for a particular plateau </summary>
        // POST: api/MarsRoverModels
        [ResponseType(typeof(MarsRoverModel))]
        public IHttpActionResult PostMarsRoverModel(int plateauid, MarsRoverModel marsRoverModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MarsRoverModels.Add(marsRoverModel);
            db.SaveChanges();

            return CreatedAtRoute("MarsRoverModelsRoute", new { id = marsRoverModel.MarsRoverId }, marsRoverModel);
        }

        ///<summary>Deletes a new mars rover from a particular plateau </summary>
        // DELETE: api/MarsRoverModels/5
        [ResponseType(typeof(MarsRoverModel))]
        public IHttpActionResult DeleteMarsRoverModel(int plateauid, int roverid)
        {
            MarsRoverModel marsRoverModel = db.MarsRoverModels.Find(roverid);
            if (marsRoverModel == null)
            {
                return NotFound();
            }

            db.MarsRoverModels.Remove(marsRoverModel);
            db.SaveChanges();

            return Ok(marsRoverModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MarsRoverModelExists(int id)
        {
            return db.MarsRoverModels.Count(e => e.MarsRoverId == id) > 0;
        }

        ///<summary>runs a list of commands to move the rover can be L(eft), R(ight), M(ove Forward). L & R only alter the orientation of the rover</summary>
        [HttpGet]
        [ResponseType(typeof(MarsRoverDestinationModel))]
        [Route("api/marsrover/plateau/{plateauid}/rover/{roverid}/{commands}")]
        public IHttpActionResult PlateauNameExists(int plateauid, int roverid,string commands)
        {
            PlateauModel plateauModel = db.PlateauModels.Find(plateauid);
            if(plateauModel != null)
            {
                MarsRoverModel roverModel = db.MarsRoverModels.Find(roverid);
                if (roverModel != null)
                {
                    MarsPlateau plateau = new MarsPlateau((uint)plateauModel.Width, (uint)plateauModel.Length);
                    MarsRoverDirection dir = MarsRoverDirection.North;
                    switch (roverModel.Direction)
                    {
                        case "N":
                            dir = MarsRoverDirection.North;
                            break;
                        case "S":
                            dir = MarsRoverDirection.South;
                            break;
                        case "E":
                            dir = MarsRoverDirection.East;
                            break;
                        case "W":
                            dir = MarsRoverDirection.West;
                            break;

                    }


                    MarsRover rover = new MarsRover(plateau, roverModel.InitialX, roverModel.InitialY, dir);
                    string successfulCommands="";
                    if(rover.Commands.TryParse(commands, out successfulCommands))
                    {
                        rover = new MarsRover(plateau, roverModel.InitialX, roverModel.InitialY, dir);
                        MarsRoverPosition pos = rover.Commands.ParseCommand(commands);
                        MarsRoverDestinationModel destination = new MarsRoverDestinationModel() { X = pos.X, Y = pos.Y, Direction = pos.Direction.ToString() };
                        return Ok(destination);
                    }
                    else
                    {
                        rover = new MarsRover(plateau, roverModel.InitialX, roverModel.InitialY, dir);
                        MarsRoverPosition pos = rover.Commands.ParseCommand(successfulCommands);
                        
                        return BadRequest(string.Format("The Command to the Rover must Only Contain L, R or M and cannot leave the plateau. Your rover was last seen here at {0} having run these commands {1} " ,pos.ToString(),successfulCommands));
                    }

                }
            }

            return NotFound();

        }

    }
}