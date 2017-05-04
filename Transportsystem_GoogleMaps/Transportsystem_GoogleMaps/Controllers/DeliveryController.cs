using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;

namespace Transportsystem_GoogleMaps.Controllers
{
    public class DeliveryController : Controller
    {
        private ApplicationDbContext _context;
        private LinkedList<PackageCluster> _deliveryOptions = new LinkedList<PackageCluster>();

        public DeliveryController()
        {
            _context = new ApplicationDbContext();
             
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
       
        // GET: Delivery
        public ActionResult Index()
        {
            var packages = _context.Packages.ToList();
            int drivers = _context.Drivers.Count();

            Delivery d = new Delivery(drivers, packages);
            // d.Packages = new LinkedList<Package>(packages);
            //d.TotalDistance = d.CalculateRouteCost(d.Packages);         

            DeliveryViewModel viewModel = new DeliveryViewModel
            {
                PackageClusters = d.clustering()
            };

            return View(viewModel);
        }

        // GET: Delivery/1
        public ActionResult DriverDelivery()
        {
            List<Package> tempPackages = new List<Package>();
            List<DeliveryRoute> allDestinations = new List<DeliveryRoute>();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            string s = currentUser.PersonalNumber;

            try
            {
                var driver = _context.Drivers.SingleOrDefault(d => d.PersonalNumber == s);
                var packages = _context.Packages.ToList();

                allDestinations = _context.DeliveryRoutes.ToList().FindAll(d => d.Driver == driver); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            foreach (var delivery in allDestinations)
            {
                tempPackages.Add(delivery.Package);
            }

            return View(tempPackages);
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