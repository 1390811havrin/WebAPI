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
    [EnableCors("http://127.0.0.1:5500", "*", "*")]
    public class DatesController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        // GET: api/Dates
        public IQueryable<Date> GetDates()
        {
            return db.Dates;
        }

        // GET: api/Dates/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult GetDate(int id)
        {
            var dates = from dt in db.Dates where dt.UID == id select dt;
            if(dates.Count() >= 1)
            {
                Date date = dates.First();
                return Ok(date);
            }
                return NotFound();
        }

        // PUT: api/Dates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDate(int id, Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != date.ID)
            {
                return BadRequest();
            }

            db.Entry(date).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateExists(id))
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

        // POST: api/Dates
        [ResponseType(typeof(Date))]
        public IHttpActionResult PostDate(Date date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dates.Add(date);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = date.ID }, date);
        }

        // DELETE: api/Dates/5
        [ResponseType(typeof(Date))]
        public IHttpActionResult DeleteDate(int id)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return NotFound();
            }

            db.Dates.Remove(date);
            db.SaveChanges();

            return Ok(date);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DateExists(int id)
        {
            return db.Dates.Count(e => e.ID == id) > 0;
        }
    }
}