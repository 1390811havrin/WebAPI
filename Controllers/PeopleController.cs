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
    public class PeopleController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        // GET: api/People
        public IQueryable<People> GetPeoples()
        {
            return db.Peoples;
        }

        // GET: api/People/5
        [ResponseType(typeof(int))]
        public IHttpActionResult GetPeople(int id)
        {

            var people = from pp in db.Peoples where pp.UID == id select pp;
            if (people.Count() < 1)
            {
                return NotFound();
            }

            return Ok(people.Count());
        }

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPeople(int id, People people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != people.ID)
            {
                return BadRequest();
            }

            db.Entry(people).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
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

        // POST: api/People
        [ResponseType(typeof(People))]
        public IHttpActionResult PostPeople(People people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Peoples.Add(people);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = people.ID }, people);
        }

        // DELETE: api/People/5
        [ResponseType(typeof(People))]
        public IHttpActionResult DeletePeople(int UID)
        {
            People allGone = new People();
            allGone.RaceID = 100;
            var populi = from pop in db.Peoples where pop.UID == UID select pop;
            if(populi.Count() >= 1)
            {
                People people = populi.First();

                db.Peoples.Remove(people);
                db.SaveChanges();

                return Ok(people);

            }
            return Ok(allGone);
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PeopleExists(int id)
        {
            return db.Peoples.Count(e => e.ID == id) > 0;
        }
    }
}