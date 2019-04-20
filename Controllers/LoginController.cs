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
    public class LoginController : ApiController
    {
        private TestCityEntities4 db = new TestCityEntities4();

        // GET: api/Login
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Login/5
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

        // PUT: api/Login/5
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

        [ResponseType(typeof(void))]
        public IHttpActionResult Post(User user)
        {

            var users = from us in db.Users
                        where us.Email == user.Email
                        select us;
            if (users.Count() == 0 || users == null)
            {
                return NotFound();
            }
            else if (users.Count() >= 1)
            {
                var suser = from us in users
                            where us.PSW == user.PSW
                            select us;
                if (suser.Count() >= 1)
                {
                    return Ok(suser.First().UserID);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();

            }
        }

        // DELETE: api/Login/5
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