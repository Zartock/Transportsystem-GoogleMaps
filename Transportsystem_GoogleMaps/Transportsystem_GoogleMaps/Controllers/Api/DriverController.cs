using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.Controllers.Api
{
    public class DriverController : ApiController
    {
        private ApplicationDbContext _context;

        public DriverController()
        {
            _context = new ApplicationDbContext();
        }

        // GET/Api/drivers
        public IEnumerable<Driver> Get()
        {
            return _context.Drivers.ToList();
        }


        // GET/Api/drivers/1
        public Driver GetDriver(int id)
        {
            var driver = _context.Drivers.SingleOrDefault(d => d.Id == id);
            if (driver == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return driver;
        }

        // POST/Api/drivers
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                return BadRequest(errorResponse.Content.ReadAsStringAsync().Result);
            }
              
            _context.Drivers.Add(driver);
            _context.SaveChanges();
            return Ok();
        }

        // PUT/Api/drivers/1
        [System.Web.Http.HttpPut]
        public void UpdateDriver(int id, Driver driver)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var driverInDb = _context.Drivers.SingleOrDefault(p => p.Id == id);

            if (driverInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            driverInDb.Name = driver.Name;
            driverInDb.PhoneNumber = driver.PhoneNumber;
            //driverInDb.DeliveryId = driver.DeliveryId;

            _context.SaveChanges();
        }

        // DELETE/Api/drivers/1
        [System.Web.Http.HttpDelete]
        public void DeleteDriver(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var driverInDb = _context.Drivers.SingleOrDefault(d => d.Id == id);

            if (driverInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            DeleteDriverFromRoute(driverInDb);

            _context.Drivers.Remove(driverInDb);
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
    }
}
