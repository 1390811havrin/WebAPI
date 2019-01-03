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
    public class TechnologiesController : ApiController
    {
        private TestCityDBModels db = new TestCityDBModels();

        // GET: api/Technologies
        public IQueryable<Technology> GetTechnologies()
        {
            return db.Technologies;
        }

        // GET: api/Technologies/5

        [ResponseType(typeof(Technology))]
        public IHttpActionResult GetTechnology(int id)
        {
            Technology technology = db.Technologies.Find(id);
            if (technology == null)
            {
                return NotFound();
            }

            return Ok(technology);
        }

        // PUT: api/Technologies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTechnology(int id, Technology technology)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != technology.TechnologyID)
            {
                return BadRequest();
            }

            db.Entry(technology).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechnologyExists(id))
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

        // POST: api/Technologies
        [ResponseType(typeof(Technology))]
        public IHttpActionResult PostTechnology(Technology technology)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Technologies.Add(technology);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = technology.TechnologyID }, technology);
        }

        // DELETE: api/Technologies/5
        [ResponseType(typeof(Technology))]
        public IHttpActionResult DeleteTechnology(int id)
        {
            Technology technology = db.Technologies.Find(id);
            if (technology == null)
            {
                return NotFound();
            }

            db.Technologies.Remove(technology);
            db.SaveChanges();

            return Ok(technology);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TechnologyExists(int id)
        {
            return db.Technologies.Count(e => e.TechnologyID == id) > 0;
        }
    }
}