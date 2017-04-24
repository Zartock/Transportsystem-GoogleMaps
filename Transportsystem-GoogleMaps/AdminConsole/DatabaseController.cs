using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.Dtos;

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




    }
}
