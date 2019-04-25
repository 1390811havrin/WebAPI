using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WebAPI.Models;
using System.Web.Configuration;

namespace IsolatedTestingOfVDP
{
    class VDP
    {
        
        public int buildingsConstructedSoFar = 0;
        public Bitmap defaultImage = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Default"]);
        public Dictionary<int, Buildings> CityMap = new Dictionary<int, Buildings>();
        public struct Buildings
        {
            public int owner { get; set; }
            public string type { get; set; }
            public int level { get; set; }
            public string name { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        };

       
        //returns a bitmap image for use
        public Bitmap GetImage(string name)
        {
            Bitmap aBuilding;

            if (name == "Baracks")
            {
                string potatoe = WebConfigurationManager.AppSettings["Baracks"];
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Baracks"]);
                return aBuilding;
            }
            else if (name == "BlackSmith")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["BlackSmith"]);
                return aBuilding;

            }
            else if (name == "Farm")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Farm"]);
                return aBuilding;

            }
            else if (name == "Grass")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Grass"]);
                return aBuilding;

            }
            else if (name == "ArcanaLibrary")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Magic"]);
                return aBuilding;

            }
            else if (name == "ScienceLab")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Science"]);
                return aBuilding;

            }
            else if (name == "Storage")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Storage"]);
                return aBuilding;

            }
            else if (name == "East Wall")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["EWall"]);
                return aBuilding;

            }
            else if (name == "West Wall")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["WWall"]);
                return aBuilding;

            }
            else if (name == "North Wall")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["NWall"]);
                return aBuilding;

            }
            else if (name == "South Wall")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["SWall"]);
                return aBuilding;

            }
            else if (name == "Wall")
            {
                aBuilding = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["Wall"]);
                return aBuilding;

            }
            else
            {
              //  Console.Out.WriteLine("Image Doesn't have a match");
                return null;
            }
        }
        //Clears the dictionairy
        public void WrapItUp()
        {
            CityMap.Clear();
          //  Console.Out.WriteLine("CityMap Cleared");
        }
        //prints out dictionairy
        public void PrintIt()
        {
            for(int i = 0; i < CityMap.Count; i++)
            {
               // Console.Out.WriteLine(CityMap[i].name);
            }

        }
        //hashes two int vals into one hashed key
        public int HashIt(int valX, int valY)
        {
            //key gets hashed or an error gets thrown
            if (valX >= 0 && valY >= 0 && valX<=40 && valY<=40)
            {
                int hashedKey = 0;
                hashedKey = valX * 100;
                hashedKey = hashedKey + valY;
                return hashedKey;
            }
            else
            {
                //error occurred
                return int.MinValue;
            }
        }
        
        //creates a image at a certain point
        public void CreateBuilding(Buildings buildings)
        {
            if (buildingsConstructedSoFar == 0)
            {


                if (System.IO.File.Exists(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png"))
                {
                    defaultImage.Dispose();
                    Bitmap defaultImage1 = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png");
                    defaultImage = new Bitmap(defaultImage1);
                    defaultImage1.Dispose();
                    System.IO.File.Delete(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png");
                }
                

                int x = buildings.x * 70;
                int y = buildings.y * 70;
                Bitmap overlayImage = GetImage(buildings.name);
                var graphics = Graphics.FromImage(defaultImage);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(overlayImage, x, y);     


                defaultImage.Save(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png", ImageFormat.Jpeg);
                graphics.Dispose();
                overlayImage.Dispose();
                defaultImage.Dispose();
                buildingsConstructedSoFar++;
            }
            else
            {
                Bitmap baseImage1 = (Bitmap)Image.FromFile(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png");
                Bitmap baseImage = new Bitmap(baseImage1);
                baseImage1.Dispose();
                if(System.IO.File.Exists(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png"))
                     System.IO.File.Delete(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png");

                
                int x = buildings.x * 70;
                int y = buildings.y * 70;
                Bitmap overlayImage = GetImage(buildings.name);
                var graphics = Graphics.FromImage(baseImage);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.DrawImage(overlayImage, x, y);
                
                baseImage.Save(WebConfigurationManager.AppSettings["ForSaving"] + "User" + buildings.owner + ".png", ImageFormat.Jpeg);
                graphics.Dispose();
                baseImage.Dispose();
                overlayImage.Dispose();
                buildingsConstructedSoFar++;
            }
            //Console.Out.WriteLine("Buildings Constructed So Far: " + buildingsConstructedSoFar);
        }

        public int[] InvertyXYBecauseWhyisTheOriginTopLeft(int x, int y)
        {
            int[] arr = new int[2];
            y = 40 - y;
            arr[0] = x;
            arr[1] = y;
            return arr;
        }

    }
}
