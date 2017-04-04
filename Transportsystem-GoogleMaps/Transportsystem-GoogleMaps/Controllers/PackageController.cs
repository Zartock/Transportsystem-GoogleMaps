using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.Controllers
{
    public class PackageController : Controller
    {
        private ApplicationDbContext _context;

        public PackageController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Package
        public ViewResult Index()
        {
            var packages = _context.Packages
                .ToList()
                .Select(Mapper.Map<Package, PackageDto>);
            return View("Index");
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}