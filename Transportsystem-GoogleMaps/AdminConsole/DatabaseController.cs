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
            var packages = GetPackages();
            int drivers = GetDrivers().Count();

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
        }




    }
}
