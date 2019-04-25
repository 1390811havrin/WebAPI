using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPI.Models;


namespace WebAPI.Controllers
{

    //[EnableCors("http://127.0.0.1:5500", "*", "*")]
    [EnableCors("http://dndhbmacroscalemanager.com", "*", "*")]
    public class BuildingsController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        // GET: api/Buildings
        public IQueryable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        [ResponseType(typeof(int))]
        public IHttpActionResult GetBuilding(int uID, string type)
        {
            var results = from bld in db.Buildings where bld.UserID == uID && bld.BuildingType == type select bld;
            var resCount = results.Sum(x => x.BuildingLevel);
            if(resCount != null)
            {
                return Ok(resCount);
            }
            else
            {
                return Ok(0);
            }
        }

        // GET: api/Buildings/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult GetBuilding(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return NotFound();
            }

            return Ok(building);
        }

        // PUT: api/Buildings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuilding(int id, Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != building.BuildingID)
            {
                return BadRequest();
            }

            db.Entry(building).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Buildings
        [ResponseType(typeof(Building))]
        public IHttpActionResult PostBuilding(Building building)
        {
            Building defBld = new Building();
            if (!ModelState.IsValid)
            {
                defBld.BuildingName = "Bad Model State";
                return Ok(defBld);
            }

            db.Buildings.Add(building);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BuildingExists(building.BuildingID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = building.BuildingID }, building);
        }

        // DELETE: api/Buildings/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult DeleteBuilding(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return NotFound();
            }

            db.Buildings.Remove(building);
            db.SaveChanges();

            return Ok(building);
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