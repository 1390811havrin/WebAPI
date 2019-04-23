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
        private TestCityEntities9 db = new TestCityEntities9();

        //PutBuildingToMap /api/VDP/1
        //Takes building ID from SQL then references that buildings SQL parameters to generate a building.
        [HttpPut]
        [ResponseType(typeof(int))]
        public IHttpActionResult PutBuildingToMap(int id)
        {
            try
            {
                Building building = db.Buildings.Find(id);

                string buildingToString = building.UserID.ToString() + "," + building.BuildingName.ToString() + "," + building.BuildingType.ToString() + "," + building.BuildingLevel + "," + building.X.ToString() + "," + building.Y.ToString();
                string[] buildingStringArr = new string[1];
                buildingStringArr[0] = buildingToString;
                VDPProgram.Main(buildingStringArr);
                return Ok(1);
            }
            catch
            {
                return Ok(0);
            }
        }

        //Doesn't delete the SQL building. Merely covers it's location with plain grass. 
        //Grass might be too large will need to check it.
        [HttpDelete]
        public void DeleteBuildingFromMap(int id)
        {
            //[ int ownerID, string buildingName, int buildingType, int buildingLevel, int x, int y]
            Building building = db.Buildings.Find(id);
            if(building == null)
            {
                return;
            }
            //"1,Baracks,1,1,7,9";
            string buildingToString = building.UserID + ",Grass,Default,1," + building.X.ToString() + "," + building.Y.ToString();
            string[] buildingStringArr = new string[1];
            buildingStringArr[0] = buildingToString;
            VDPProgram.Main(buildingStringArr);
        }

        //allows for getting buildings via user id and x,y loca
        [HttpGet]
        [ResponseType(typeof(Building))]
        public IHttpActionResult GetBuildingDetailsFromMap(int x, int y, int UID)
        {
            Building bd1 = new Building();
            bd1.BuildingName = "NA";
            var buildings = from building in db.Buildings where building.UserID == UID && building.Y == y && building.X == x select building;
            if (buildings.Count() >= 1)
            {
                Building exactBuilding = buildings.First();

                return Ok(exactBuilding);
            }
            return Ok(bd1);
        }

    }
}
