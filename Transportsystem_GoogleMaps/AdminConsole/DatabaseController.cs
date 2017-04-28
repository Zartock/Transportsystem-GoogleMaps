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
            var fisk = _context.Packages.ToList();
            return _context.Packages
                .ToList();
        }

        public Package GetPackageById(int id)
        {
            return _context.Packages.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Driver> GetDrivers()
        {
            return _context.Drivers
                .ToList();
        }

        public Driver GetDriverById(int id)
        {
            return _context.Drivers.SingleOrDefault(d => d.Id == id);
        }

        public void SavePackage(Package package)
        {
            if (package.Id == 0)
            {
                _context.Packages.Add(package);
            }
            else
            {
                var packageInDb = _context.Packages.Single(p => p.Id == package.Id);
                packageInDb.Content = package.Content;
                packageInDb.Destination = package.Destination;
            }
            _context.SaveChanges();
        }

        public void SaveDriver(Driver driver)
        {
            if (driver.Id == 0)
            {
                _context.Drivers.Add(driver);
            }
            else
            {
                var driverInDb = _context.Drivers.Single(d => d.Id == driver.Id);
                driverInDb.Name = driver.Name;
                driverInDb.Name = driver.Name;
            }
            _context.SaveChanges();
        }

        public void UpdateDriver(int id, Driver driver)
        {
            var driverInDb = _context.Drivers.SingleOrDefault(d => d.Id == id);


            driverInDb.Name = driver.Name;
            driverInDb.PhoneNumber = driver.PhoneNumber;

            _context.SaveChanges();
        }

        public void UpdatePackage(int id, Package package)
        {
            var packageInDb = _context.Packages.SingleOrDefault(p => p.Id == id);


            packageInDb.Content = package.Content;
            packageInDb.Destination = package.Destination;

            _context.SaveChanges();
        }

        public void DeletePackage(int id)
        {
            var packageInDb = _context.Packages.SingleOrDefault(p => p.Id == id);

            if (packageInDb == null)
                Console.WriteLine("No package with that ID in db");
            else
            {
                DeletePackageFromRoute(packageInDb);
                _context.Packages.Remove(packageInDb);
                _context.SaveChanges();
            }
        }

        public void DeleteDriver(int id)
        {
            var driverInDb = _context.Drivers.SingleOrDefault(d => d.Id == id);

            if (driverInDb == null)
                Console.WriteLine("No driver with that ID in db");
            else
            {
                DeleteDriverFromRoute(driverInDb);
                _context.Drivers.Remove(driverInDb);
                _context.SaveChanges();
            }
        }

        public void DeletePackageFromRoute(Package package)
        {
            var routes = _context.DeliveryRoutes.ToList();
            foreach (var route in routes)
            {
                if (route.Package == package)
                {
                    _context.DeliveryRoutes.Remove(route);
                }
            }
            _context.SaveChanges();
        }

        public void DeleteDriverFromRoute(Driver driver)
        {
            var routes = _context.DeliveryRoutes.ToList();
            foreach (var route in routes)
            {
                if (route.Driver == driver)
                    _context.DeliveryRoutes.Remove(route);
            }
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
                var json =
                    new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" +
                                                   package.Destination + "&key=AIzaSyBMVIteB6a_vtVSunhpk56yZWeTSGN2CkM");
                var data = JObject.Parse(json);
                package.Latitude =
                    (double) data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lat");
                package.Longitude =
                    (double) data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lng");
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