using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;

namespace Transportsystem_GoogleMaps.Controllers
{
    public class DriverController : Controller
    {
        private ApplicationDbContext _context;

        public DriverController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Driver
        public ActionResult Index()
        {
            var drivers = _context.Drivers.ToList();
            return View(drivers);
        }

      
        public ActionResult New()
        {
            var viewModel = new DriverFormViewModel();

            return View("DriverForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var driver = _context.Drivers.SingleOrDefault(d => d.Id == id);
            if (driver == null)
                return HttpNotFound();
            var viewModel = new DriverFormViewModel(driver);
            return View("DriverForm", viewModel);
        }


        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Driver driver)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new DriverFormViewModel(driver);
                return View("DriverForm", viewModel);

            }
            if (driver.Id == 0)
            {
                _context.Drivers.Add(driver);
            }
            else
            {
                var driverInDb = _context.Drivers.Single(d => d.Id == driver.Id);
                driverInDb.Name = driver.Name;
                driverInDb.PhoneNumber = driver.PhoneNumber;
                driverInDb.PersonalNumber = driver.PersonalNumber;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Driver");
        }
    }
}