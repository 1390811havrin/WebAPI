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
    public class CitiesHaveTechnologiesController : ApiController
    {
        private TestCityDBModels db = new TestCityDBModels();

        // GET: api/CitiesHaveTechnologies
        public IQueryable<CitiesHaveTechnology> GetCitiesHaveTechnologies()
        {
            return db.CitiesHaveTechnologies;
        }

        // GET: api/CitiesHaveTechnologies/5
        [ResponseType(typeof(CitiesHaveTechnology))]
        public IHttpActionResult GetCitiesHaveTechnology(int id)
        {
            CitiesHaveTechnology citiesHaveTechnology = db.CitiesHaveTechnologies.Find(id);
            if (citiesHaveTechnology == null)
            {
                return NotFound();
            }

            return Ok(citiesHaveTechnology);
        }

        // PUT: api/CitiesHaveTechnologies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCitiesHaveTechnology(int id, CitiesHaveTechnology citiesHaveTechnology)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != citiesHaveTechnology.CityID)
            {
                return BadRequest();
            }

            db.Entry(citiesHaveTechnology).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiesHaveTechnologyExists(id))
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

        // POST: api/CitiesHaveTechnologies
        [ResponseType(typeof(CitiesHaveTechnology))]
        public IHttpActionResult PostCitiesHaveTechnology(CitiesHaveTechnology citiesHaveTechnology)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CitiesHaveTechnologies.Add(citiesHaveTechnology);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CitiesHaveTechnologyExists(citiesHaveTechnology.CityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = citiesHaveTechnology.CityID }, citiesHaveTechnology);
        }

        // DELETE: api/CitiesHaveTechnologies/5
        [ResponseType(typeof(CitiesHaveTechnology))]
        public IHttpActionResult DeleteCitiesHaveTechnology(int id)
        {
            CitiesHaveTechnology citiesHaveTechnology = db.CitiesHaveTechnologies.Find(id);
            if (citiesHaveTechnology == null)
            {
                return NotFound();
            }

            db.CitiesHaveTechnologies.Remove(citiesHaveTechnology);
            db.SaveChanges();

            return Ok(citiesHaveTechnology);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CitiesHaveTechnologyExists(int id)
        {
            return db.CitiesHaveTechnologies.Count(e => e.CityID == id) > 0;
        }
    }
}