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
    [EnableCors("http://dndhbmacroscalemanager.com", "*", "*")]
    public class CitiesHaveBuildingsController : ApiController
    {
        private TestCityDBModels db = new TestCityDBModels();

        // GET: api/CitiesHaveBuildings

        public IQueryable<CitiesHaveBuilding> GetCitiesHaveBuildings()
        {
            return db.CitiesHaveBuildings;
        }

        // GET: api/CitiesHaveBuildings/5

        [ResponseType(typeof(CitiesHaveBuilding))]
        public IHttpActionResult GetCitiesHaveBuilding(int id)
        {
            CitiesHaveBuilding citiesHaveBuilding = db.CitiesHaveBuildings.Find(id);
            if (citiesHaveBuilding == null)
            {
                return NotFound();
            }

            return Ok(citiesHaveBuilding);
        }

        // PUT: api/CitiesHaveBuildings/5

        [ResponseType(typeof(void))]
        public IHttpActionResult PutCitiesHaveBuilding(int id, CitiesHaveBuilding citiesHaveBuilding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != citiesHaveBuilding.CityID)
            {
                return BadRequest();
            }

            db.Entry(citiesHaveBuilding).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiesHaveBuildingExists(id))
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

        // POST: api/CitiesHaveBuildings

        [ResponseType(typeof(CitiesHaveBuilding))]
        public IHttpActionResult PostCitiesHaveBuilding(CitiesHaveBuilding citiesHaveBuilding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CitiesHaveBuildings.Add(citiesHaveBuilding);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CitiesHaveBuildingExists(citiesHaveBuilding.CityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = citiesHaveBuilding.CityID }, citiesHaveBuilding);
        }

        // DELETE: api/CitiesHaveBuildings/5

        [ResponseType(typeof(CitiesHaveBuilding))]
        public IHttpActionResult DeleteCitiesHaveBuilding(int id)
        {
            CitiesHaveBuilding citiesHaveBuilding = db.CitiesHaveBuildings.Find(id);
            if (citiesHaveBuilding == null)
            {
                return NotFound();
            }

            db.CitiesHaveBuildings.Remove(citiesHaveBuilding);
            db.SaveChanges();

            return Ok(citiesHaveBuilding);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CitiesHaveBuildingExists(int id)
        {
            return db.CitiesHaveBuildings.Count(e => e.CityID == id) > 0;
        }
    }
}