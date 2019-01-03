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
    public class CitiesHavePopulationsController : ApiController
    {
        private TestCityDBModels db = new TestCityDBModels();

        // GET: api/CitiesHavePopulations
        public IQueryable<CitiesHavePopulation> GetCitiesHavePopulations()
        {
            return db.CitiesHavePopulations;
        }

        // GET: api/CitiesHavePopulations/5
        [ResponseType(typeof(CitiesHavePopulation))]
        public IHttpActionResult GetCitiesHavePopulation(int id)
        {
            CitiesHavePopulation citiesHavePopulation = db.CitiesHavePopulations.Find(id);
            if (citiesHavePopulation == null)
            {
                return NotFound();
            }

            return Ok(citiesHavePopulation);
        }

        // PUT: api/CitiesHavePopulations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCitiesHavePopulation(int id, CitiesHavePopulation citiesHavePopulation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != citiesHavePopulation.PUID)
            {
                return BadRequest();
            }

            db.Entry(citiesHavePopulation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitiesHavePopulationExists(id))
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

        // POST: api/CitiesHavePopulations
        [ResponseType(typeof(CitiesHavePopulation))]
        public IHttpActionResult PostCitiesHavePopulation(CitiesHavePopulation citiesHavePopulation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CitiesHavePopulations.Add(citiesHavePopulation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = citiesHavePopulation.PUID }, citiesHavePopulation);
        }

        // DELETE: api/CitiesHavePopulations/5
        [ResponseType(typeof(CitiesHavePopulation))]
        public IHttpActionResult DeleteCitiesHavePopulation(int id)
        {
            CitiesHavePopulation citiesHavePopulation = db.CitiesHavePopulations.Find(id);
            if (citiesHavePopulation == null)
            {
                return NotFound();
            }

            db.CitiesHavePopulations.Remove(citiesHavePopulation);
            db.SaveChanges();

            return Ok(citiesHavePopulation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CitiesHavePopulationExists(int id)
        {
            return db.CitiesHavePopulations.Count(e => e.PUID == id) > 0;
        }
    }
}