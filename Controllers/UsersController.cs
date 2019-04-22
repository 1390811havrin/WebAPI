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
    public class UsersController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            var results = from usr in db.Users
                          where usr.Email == user.Email && usr.PSW == user.PSW
                          select usr;
            int uID = results.First().UserID;
            #region Date Stuff
            Date date = new Date();
            date.UID = uID;
            date.Date1 = 0;
            db.Dates.Add(date);
            #endregion
            #region People Stuff
            People people = new People();
            people.RaceID = 1;
            people.UID = uID;
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            db.Peoples.Add(people);
            db.SaveChanges();
            #endregion
            #region persistent Stuff
            PersistentSum persistentSum = new PersistentSum();
            persistentSum.UID = uID;
            persistentSum.Food = 10;
            persistentSum.Gold = 1000;
            persistentSum.RareResources = 0;
            db.PersistentSums.Add(persistentSum);
            #endregion

            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }
    }
}