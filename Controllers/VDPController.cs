using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using IsolatedTestingOfVDP;
using System.Web.Http.Description;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors("http://127.0.0.1:5500", "*", "*")]
    public class VDPController : ApiController
    {
        private TestCityEntities4 db = new TestCityEntities4();

        //PutBuildingToMap /api/VDP/1
        [HttpPut]
        public void PutBuildingToMap(int id)
        {
            Building building = db.Buildings.Find(id);

            string buildingToString = building.UserID.ToString() + "," + building.BuildingName.ToString() + "," + building.BuildingType.ToString() + "," + building.BuildingLevel + "," + building.X.ToString() + "," + building.Y.ToString();
            string[] buildingStringArr = new string[1];
            buildingStringArr[0] = buildingToString;
            VDPProgram.Main(buildingStringArr);
        }

        //Doesn't delete the SQL building. Merely covers it's location with plain grass. 
        //Grass might be too large will need to check it.
        [HttpDelete]
        public void DeleteBuildingFromMap(int id)
        {
            Building building = db.Buildings.Find(id);
            if(building == null)
            {
                return;
            }
            //"1,Baracks,1,1,7,9";
            string buildingToString =  "1,Grass,1,1," + building.X.ToString() + "," + building.Y.ToString();
            string[] buildingStringArr = new string[1];
            buildingStringArr[0] = buildingToString;
            VDPProgram.Main(buildingStringArr);
        }

        //allows for getting buildings via user id and x,y loca
        [HttpGet]
        [ResponseType(typeof(Building))]
        public IHttpActionResult GetBuildingDetailsFromMap(int x, int y, int UID)
        {
            var buildings = from building in db.Buildings where building.UserID == UID && building.Y == y && building.X == x select building;
            Building exactBuilding = buildings.First();

            if (exactBuilding == null)
            {
                return NotFound();
            }

            return Ok(exactBuilding);
        }

    }
}
