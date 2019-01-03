using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors("http://dndhbmacroscalemanager.com/Hawkmenrise/CitySimulatorTest/html/MainPage.html", "*", "*")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
