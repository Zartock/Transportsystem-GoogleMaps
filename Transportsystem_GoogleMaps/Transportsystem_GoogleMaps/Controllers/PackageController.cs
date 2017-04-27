using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Transportsystem_GoogleMaps.Dtos;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;

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
            var viewModel = new PackageFormViewModel();
            return View("PackageForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var package = _context.Packages.SingleOrDefault(p => p.Id == id);
            if(package == null)
                return HttpNotFound();
            var viewModel = new PackageFormViewModel(package);
            
            return View("PackageForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Package package)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new PackageFormViewModel(package);
                return View("PackageForm", viewModel);

            }
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

            return RedirectToAction("Index", "Package");
        }
    }
}