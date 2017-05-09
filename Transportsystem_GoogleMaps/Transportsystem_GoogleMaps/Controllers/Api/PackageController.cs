using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.Models;
using System.Data.Entity.Validation;


namespace Transportsystem_GoogleMaps.Controllers.Api
{
    public class PackageController : ApiController
    {
        private ApplicationDbContext _context;

        public PackageController()
        {
            _context = new ApplicationDbContext();
        }

        // GET/Api/packages
        public IEnumerable<PackageDto> GetPackages()
        {
            return _context.Packages
                .ToList()
                .Select(Mapper.Map<Package, PackageDto>);
        }


        // GET/Api/packages/1
        public IHttpActionResult GetPackage(int id)
        {
            var package = _context.Packages.SingleOrDefault(p => p.Id == id);
            if (package == null)
                return NotFound();
            return Ok(Mapper.Map<Package, PackageDto>(package));
        }

        // POST/Api/packages
        [HttpPost]
        public IHttpActionResult CreatePackage(PackageDto packageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToString());
            }
              
            var package = Mapper.Map<PackageDto, Package>(packageDto);

            try
            {
                var json =
                    new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" +
                                                   package.Destination +
                                                   "&key=AIzaSyBMVIteB6a_vtVSunhpk56yZWeTSGN2CkM");
                var data = JObject.Parse(json);
                package.Latitude =
                    (double) data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lat");
                package.Longitude =
                    (double) data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lng");
                _context.Packages.Add(package);
                _context.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                return BadRequest(e.StackTrace + "\n  Validation of content failed");
            }
            catch (Exception e)
            {
                return BadRequest(e.StackTrace + "\n  Address does not exist");
            }
           
            packageDto.Id = package.Id;
            return Created(new Uri(Request.RequestUri + "/" + package.Id), packageDto);
        }


        // POST/Api/packages
        [HttpPost]
        public void ChangeStatus(int id)
        {
            var packageInDb = _context.Packages.SingleOrDefault(p => p.Id == id);
            packageInDb.Status = "Delivered";
            _context.SaveChanges();
        }


        // PUT/Api/packages/1
        [HttpPut]
        public IHttpActionResult UpdatePackage(int id, PackageDto packageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.ToString());
            }

            var packageInDb = _context.Packages.SingleOrDefault(p => p.Id == id);

            if (packageInDb == null)
                return NotFound();

            Mapper.Map(packageDto, packageInDb);
            _context.SaveChanges();

            return Ok();
        }



        // DELETE/Api/packages/1
        [HttpDelete]
        public IHttpActionResult DeletePackage(int id)
        {

            var packageInDb = _context.Packages.SingleOrDefault(p => p.Id == id);

            if (packageInDb == null)
                return NotFound();

            DeletePackageFromRoute(packageInDb);
            _context.Packages.Remove(packageInDb);
            _context.SaveChanges();
            return Ok();
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
    }
}
