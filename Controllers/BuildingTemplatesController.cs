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
    public class BuildingTemplatesController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        // GET: api/BuildingTemplates
        public IQueryable<BuildingTemplate> GetBuildingTemplates()
        {
            return db.BuildingTemplates;
        }

        // GET: api/BuildingTemplates/5
        [ResponseType(typeof(BuildingTemplate))]
        public IHttpActionResult GetBuildingTemplate(string name)
        {
            BuildingTemplate bt = new BuildingTemplate();
            bt.Name = "NA";
            var results = from bld in db.BuildingTemplates where bld.Name == name select bld;
            if(results.Count() <1)
            {
                return Ok(bt);
            }
            else
            {
                return Ok(results.First());
            }
        }

        // PUT: api/BuildingTemplates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuildingTemplate(int id, BuildingTemplate buildingTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != buildingTemplate.ID)
            {
                return BadRequest();
            }

            db.Entry(buildingTemplate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingTemplateExists(id))
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

        // POST: api/BuildingTemplates
        [ResponseType(typeof(BuildingTemplate))]
        public IHttpActionResult PostBuildingTemplate(BuildingTemplate buildingTemplate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BuildingTemplates.Add(buildingTemplate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BuildingTemplateExists(buildingTemplate.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = buildingTemplate.ID }, buildingTemplate);
        }

        // DELETE: api/BuildingTemplates/5
        [ResponseType(typeof(BuildingTemplate))]
        public IHttpActionResult DeleteBuildingTemplate(int id)
        {
            BuildingTemplate buildingTemplate = db.BuildingTemplates.Find(id);
            if (buildingTemplate == null)
            {
                return NotFound();
            }

            db.BuildingTemplates.Remove(buildingTemplate);
            db.SaveChanges();

            return Ok(buildingTemplate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingTemplateExists(int id)
        {
            return db.BuildingTemplates.Count(e => e.ID == id) > 0;
        }
    }
}