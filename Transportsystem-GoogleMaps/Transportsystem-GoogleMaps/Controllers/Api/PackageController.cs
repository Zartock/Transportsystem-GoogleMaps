using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using AutoMapper;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.Models;

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
                return BadRequest();

            var package = Mapper.Map<PackageDto, Package>(packageDto);
            _context.Packages.Add(package);
            _context.SaveChanges();

            packageDto.Id = package.Id;
            return Created(new Uri(Request.RequestUri + "/" + package.Id), packageDto);
        }

        // PUT/Api/packages/1
        [HttpPut]
        public IHttpActionResult UpdatePackage(int id, PackageDto packageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var packageInDb = _context.Packages.SingleOrDefault(p => p.Id == id);

            if(packageInDb == null)
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
