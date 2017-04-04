using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transportsystem_GoogleMaps.Models;

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