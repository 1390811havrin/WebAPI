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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class BuildingToVDPWrapper : ApiController
    {
        private TestCityEntities4 db = new TestCityEntities4();

        // PUT: api/BuildingToVDPWrapper/5
        //For upgrading a building at a location
        //Needs to call put building on the right building
        //Then it needs to call upgrade building from vdp
        //then it needs to return the updated image.
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuilding(int UID, int x, int y, string name)
        {
        }

        // POST: api/BuildingToVDPWrapper
        //For Creating a new building at a certain location
        //Takes in userID from cookie the X and Y coords and the name of the building.
        [ResponseType(typeof(Building))]
        public IHttpActionResult PostBuilding(int UID, int x, int y, string name)
        {
           
        }

        // DELETE: api/BuildingToVDPWrapper/5
        //for deleting building at a certain location
        /// <summary>
        /// For deleting a building at a designated location
        /// First deletes teh building from sql
        /// then covers its location on the map
        /// then returns updated information
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [ResponseType(typeof(Building))]
        public IHttpActionResult DeleteBuilding(int UID, int x, int y)
        {
                
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingExists(int id)
        {
            return db.Buildings.Count(e => e.BuildingID == id) > 0;
        }
    }
}