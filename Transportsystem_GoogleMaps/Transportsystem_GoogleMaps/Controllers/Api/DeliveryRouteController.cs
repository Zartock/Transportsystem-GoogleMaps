using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Results;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.Models;
using System.Data.Entity.Validation;

namespace Transportsystem_GoogleMaps.Controllers.Api
{
    public class DeliveryRouteController : ApiController
    {
        private ApplicationDbContext _context;

        public DeliveryRouteController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<DeliveryRoute> Get()
        {
            var packages = _context.Packages.ToList();
            var drivers = _context.Drivers.ToList();
            return _context.DeliveryRoutes.ToList();
        }

        [HttpGet]
        public IEnumerable<Package> GetDeliveryRoute(int id)
        {
            List<DeliveryRoute> filteredList = new List<DeliveryRoute>();
            List<Package> tmp = new List<Package>();
            Driver driver = _context.Drivers.SingleOrDefault(d => d.Id == id);
            if (driver != null)
            {
                var packages = _context.Packages.ToList();
                var firstList = _context.DeliveryRoutes.ToList();
                filteredList = firstList.FindAll(d => d.Driver == driver);
                
                foreach (var listItem in filteredList)
                {
                    tmp.Add(listItem.Package);
                }
            }
            return tmp;
        }


        //public void changePackageStatus(int id)
        //{
        //    Package package = _context.Packages.SingleOrDefault(p => p.Id == id);
        //    List<DeliveryRoute> filteredList = new List<DeliveryRoute>();
        //    List<Package> tmp = new List<Package>();
        //    Driver driver = _context.Drivers.SingleOrDefault(d => d.Id == id);
        //    if (driver != null)
        //    {
        //        var packages = _context.Packages.ToList();
        //        var firstList = _context.DeliveryRoutes.ToList();
        //        filteredList = firstList.FindAll(d => d.Driver == driver);

        //        foreach (var listItem in filteredList)
        //        {
        //            tmp.Add(listItem.Package);
        //        }
        //    }
        //    return tmp;
        //}


        
        // POST/Api/DeliveryRoute
        [HttpPost]
        public IHttpActionResult AddDeliveryRoutes(string id)
        {
            //TODO Fixa så att clustering endast görs på klient och hindra dubletter av förare när deliveryroutes sparas
            DateTime date;
            var rgx = new Regex(@"^\d{4}-\d{2}-\d{2}");
            if (!rgx.IsMatch(id))
                return BadRequest();
            date = Convert.ToDateTime(id);
            if (date - DateTime.Today <= TimeSpan.FromDays(0))
                return BadRequest("date has already passed");

            var routesToDelete = _context.DeliveryRoutes.ToList();
            foreach (var route in routesToDelete)
            {
                _context.DeliveryRoutes.Remove(route);
            }
            _context.SaveChanges();


            var packages = _context.Packages.ToList();
            // only include packages with status Undelivered
            packages.RemoveAll(p => p.Status == "Delivered");
           
            var driverList = _context.Drivers.ToList();
            int drivers = driverList.Count();

            Delivery d = new Delivery(drivers, packages);

            LinkedList<PackageCluster> clusters = d.clustering();
            List<DeliveryRoute> routes = new List<DeliveryRoute>();

            for (int i = 0; i < clusters.Count; i++)
            {
                List<Package> packageList = new List<Package>(clusters.ElementAt(i).AssignedPackages);
                for (int j = 0; j < packageList.Count; j++)
                {
                    routes.Add(new DeliveryRoute(driverList.ElementAt(i), packageList.ElementAt(j), date));
                }
            }

            for (int i = 0; i < routes.Count; i++)
            {
                _context.Packages.Attach(routes[i].Package);
                _context.DeliveryRoutes.Add(routes[i]);
            }
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        public void Delete()
        {
            var routesToDelete = _context.DeliveryRoutes.ToList();
            foreach (var route in routesToDelete)
            {
                _context.DeliveryRoutes.Remove(route);
            }
            _context.SaveChanges();
        }
    }
}
