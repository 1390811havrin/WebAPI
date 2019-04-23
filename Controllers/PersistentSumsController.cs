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
    public class PersistentSumsController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        // GET: api/PersistentSums
        public IQueryable<PersistentSum> GetPersistentSums()
        {

            return db.PersistentSums;
        }

        // GET: api/PersistentSums/5
        [ResponseType(typeof(PersistentSum))]
        public IHttpActionResult GetPersistentSum(int id)
        {

            var result = from ps in db.PersistentSums where ps.UID == id select ps;
            if (result.Count() >= 1)
            {
                PersistentSum persistentSum = new PersistentSum();
                persistentSum.Food = result.First().Food;
                persistentSum.Gold = result.First().Gold;
                persistentSum.RareResources = result.First().RareResources;
                persistentSum.UID = result.First().UID;
                persistentSum.ID = result.First().ID;
                return Ok(persistentSum);
            }
            return NotFound();
        }

        // PUT: api/PersistentSums/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersistentSum(int id, PersistentSum persistentSum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persistentSum.ID)
            {
                return BadRequest();
            }

            db.Entry(persistentSum).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersistentSumExists(id))
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

        // POST: api/PersistentSums
        [ResponseType(typeof(PersistentSum))]
        public IHttpActionResult PostPersistentSum(PersistentSum persistentSum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersistentSums.Add(persistentSum);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persistentSum.ID }, persistentSum);
        }

        // DELETE: api/PersistentSums/5
        [ResponseType(typeof(PersistentSum))]
        public IHttpActionResult DeletePersistentSum(int id)
        {
            PersistentSum persistentSum = db.PersistentSums.Find(id);
            if (persistentSum == null)
            {
                return NotFound();
            }

            db.PersistentSums.Remove(persistentSum);
            db.SaveChanges();

            return Ok(persistentSum);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersistentSumExists(int id)
        {
            return db.PersistentSums.Count(e => e.ID == id) > 0;
        }
    }
}