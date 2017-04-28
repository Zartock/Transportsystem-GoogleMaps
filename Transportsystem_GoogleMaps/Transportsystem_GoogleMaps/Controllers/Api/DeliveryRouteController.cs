using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Transportsystem_GoogleMaps.Models;

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


        [HttpPost]
        public void AddDeliveryRoute(DeliveryRoute deliveryRoute)
        {
            _context.DeliveryRoutes.Add(deliveryRoute);
            _context.SaveChanges();
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
