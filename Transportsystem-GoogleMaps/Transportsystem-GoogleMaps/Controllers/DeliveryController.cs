using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;

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
            return View();
        }
        public ActionResult Deliveries()
        {
            var packages = _context.Packages.ToList();
            int drivers = _context.Drivers.Count();

            List<Package> packagesWithData = new List<Package>();
            foreach (var package in packages)
            {
                var json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + package.Destination + "&key=AIzaSyCfR-nnn-eyRG42Qu0T_AnwttgCVuy8i88");
                var data = JObject.Parse(json);
                package.Latitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lat");
                package.Longitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lng");
                packagesWithData.Add(package);
            }           
            _context.SaveChanges();

            Delivery d = new Delivery(drivers);
            d.Packages = new LinkedList<Package>(packages);
            d.TotalDistance = d.CalculateRouteCost(d.Packages);
            //d.clustering();
            

            DeliveryViewModel viewModel = new DeliveryViewModel
            {
                Packages = d.Packages,
                TotalDistance = d.TotalDistance
            };

            return View(viewModel);
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