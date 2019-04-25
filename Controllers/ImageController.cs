using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPI.Models;
using System.Web.Configuration;


namespace WebAPI.Controllers
{
    //[EnableCors("http://127.0.0.1:5500", "*", "*")]
    [EnableCors("http://dndhbmacroscalemanager.com", "*", "*")]
    public class ImageController : ApiController
    {
        private TestCityEntities9 db = new TestCityEntities9();

        [HttpGet]
        [ResponseType(typeof(HttpResponseMessage))]
        public IHttpActionResult Get(string cookie)
        {
            int cUID;
            int.TryParse(cookie, out cUID);
            //parse cookie into UID then check val. If val is valid then enter if statemnt. If not return default.
            if (cUID >= 0)
            {
                var buildings = from bds in db.Buildings where bds.UserID == cUID select bds;
                if (buildings.Count() > 1)
                {
                    string user = "User" + cUID;
                    var streamE = File.OpenRead(@WebConfigurationManager.AppSettings["ForSaving"] + user + ".png");

                    var response1 = new HttpResponseMessage(HttpStatusCode.OK);
                    response1.Content = new StreamContent(streamE);

                    response1.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                    response1.Content.Headers.ContentLength = streamE.Length;

                    return ResponseMessage(response1);
                }
            }
            var stream = File.OpenRead(@WebConfigurationManager.AppSettings["Default"]);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.Content.Headers.ContentLength = stream.Length;

            return ResponseMessage(response);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(Class1 img)
        {

            var buildingTemplate = from bds in db.BuildingTemplates
                                   where bds.Name == img.name
                                   select bds;
            int highestBID;
            try
            {
                highestBID = db.Buildings.Max(Building => Building.BuildingID);
            }
            catch
            {
                highestBID = -1;
            }

            Building building = new Building();
            building.BuildingCost = buildingTemplate.First().Cost;
            building.BuildingLevel = buildingTemplate.First().lvl;
            building.BuildingName = buildingTemplate.First().Name;
            building.BuildingType = buildingTemplate.First().Type;
            building.BuildingID = highestBID + 1;
            building.UserID = int.Parse(img.cookie);
            building.X = img.x;
            building.Y = img.y;


            db.Buildings.Add(building);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {

            }

            VDPController vdp = new VDPController();
            vdp.PutBuildingToMap(building.BuildingID);
            return StatusCode(HttpStatusCode.NoContent);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingExists(int id)
        {
            return db.Buildings.Count(e => e.BuildingID == id) > 0;
        }
    }
}



/*
 * 
 * 
 * 
 * 
 * 
 * 
 */
