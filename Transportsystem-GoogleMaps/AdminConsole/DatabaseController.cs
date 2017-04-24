using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.ViewModels;

namespace AdminConsole
{
    class DatabaseController
    {
        private ApplicationDbContext _context;

        public DatabaseController()
        {
            _context = new ApplicationDbContext();
        }

        protected void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IEnumerable<Package> GetPackages()
        {
            return _context.Packages
                .ToList();
                
            
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return _context.Drivers
                .ToList();

            
        }

        public void SavePackage(Package p)
        {
            _context.Packages.Add(p);
            _context.SaveChanges();
        }

        public void SaveDriver(Driver d)
        {
            _context.Drivers.Add(d);
            _context.SaveChanges();
        }

        public void Clustering()
        {

            var routesToDelete = _context.DeliveryRoutes.ToList();
            foreach (var route in routesToDelete)
            {
                _context.DeliveryRoutes.Remove(route);
            }
            _context.SaveChanges();

            var packages = GetPackages();
            List<Driver> driverList = new List<Driver>(GetDrivers());
            int drivers = driverList.Count();

            List<Package> packagesWithData = new List<Package>();
            foreach (var package in packages)
            {
                var json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + package.Destination + "&key=AIzaSyBMVIteB6a_vtVSunhpk56yZWeTSGN2CkM");
                var data = JObject.Parse(json);
                package.Latitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lat");
                package.Longitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lng");
                packagesWithData.Add(package);
            }
            _context.SaveChanges();

            Delivery d = new Delivery(drivers);
            d.Packages = new LinkedList<Package>(packages);


            LinkedList<PackageCluster> clusters = d.clustering();

            for (int i = 0; i < clusters.Count; i++)
            {
                List<Package> packageList = new List<Package>(clusters.ElementAt(i).AssignedPackages);
                for (int j = 0; j < packageList.Count; j++)
                {
                    DeliveryRoute dr = new DeliveryRoute();
                    dr.Driver = driverList.ElementAt(i);
                    dr.Package = packageList.ElementAt(j);
                    _context.DeliveryRoutes.Add(dr);
                }
            }
            _context.SaveChanges();
        }

        public void GetRoute(int id)
        {
            Driver driver = _context.Drivers.SingleOrDefault(d => d.Id == id);
            if (driver != null)
            {
                var packages = _context.Packages.ToList();
                //List<DeliveryRoute> packageList = new List<DeliveryRoute>(_context.DeliveryRoutes.ToList().Where(o => o.Driver == driver));
                //var deliveryRoutes = _context.DeliveryRoutes.ToList().Where(o => o.Driver == driver);
                var firstList = _context.DeliveryRoutes.ToList();
                List<DeliveryRoute> filteredList = firstList.FindAll(d => d.Driver == driver);
            }
            else
            {
                Console.WriteLine("No driver with that ID found");
            }
            
        }




    }
}
