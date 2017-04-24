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

        public void GetPackages()
        {
            var packages = _context.Packages
                .ToList();
                
            for (int i = 1; i <= packages.Count(); i++)
            {
                Console.WriteLine(i + ".");
                Console.WriteLine("    Content: " + packages.ElementAt(i - 1).Content);
                Console.WriteLine("    Destination: " + packages.ElementAt(i - 1).Destination);
                Console.WriteLine("------------------------------");
            }
            Console.ReadLine();
        }

        public void GetDrivers()
        {
            var drivers = _context.Drivers
                .ToList();

            for (int i = 1; i <= drivers.Count(); i++)
            {
                Console.WriteLine(i + ".");
                Console.WriteLine("    Name: " + drivers.ElementAt(i - 1).Name);
                Console.WriteLine("    Phone number: " + drivers.ElementAt(i -1).PhoneNumber);
                Console.WriteLine("------------------------------");
            }
            Console.ReadLine();
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
