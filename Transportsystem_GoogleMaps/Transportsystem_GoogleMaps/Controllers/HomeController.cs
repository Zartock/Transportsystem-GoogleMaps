using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Transportsystem_GoogleMaps.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (User.IsInRole("DriverRole"))
                return RedirectToAction("Deliveries", "Delivery");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}