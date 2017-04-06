using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.Controllers
{
    public class DeliveryController : Controller
    {
        private ApplicationDbContext _context;

        public DeliveryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Delivery
        public ActionResult Index()
        {
            var json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=Winnetka&key=AIzaSyCfR-nnn-eyRG42Qu0T_AnwttgCVuy8i88");
            return View();
        }
        public ActionResult Deliveries()
        {
            var packages = _context.Packages.ToList();

            foreach (var package in packages)
            {
                var json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + package.Destination + "&key=AIzaSyCfR-nnn-eyRG42Qu0T_AnwttgCVuy8i88");
                var data = JObject.Parse(json);
                package.Latitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lat");
                package.Longitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lng");
            }
            _context.SaveChanges();

            return View(packages);
        }


        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}