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
            var driverList = _context.Drivers.ToList();
            var packageList = _context.Packages.ToList();
            var deliveryRouteList = _context.DeliveryRoutes.ToList();
            

            NewDeliveryViewModel viewModel = new NewDeliveryViewModel
            {
                Drivers = driverList,
                DeliveryRoutes = deliveryRouteList
            };

            List<Driver> deleteList = new List<Driver>();

            foreach (var driver in driverList)
            {
                var tmp = deliveryRouteList.FindAll(d => d.Driver.Id == driver.Id);
                List<Package> tmpPackageList = new List<Package>();
                foreach (var deliveryRoute in tmp)
                {
                    tmpPackageList.Add(deliveryRoute.Package);
                }
                if (tmpPackageList.Count == 0)
                    deleteList.Add(driver);
                else
                    viewModel.DeliveryByDriver.Add(driver.Name, tmpPackageList);
            }
            foreach(var driver in deleteList)
            {
                driverList.Remove(driver);
            }

            return View("Index", viewModel);
        }

        // GET: Delivery/1
        public ActionResult DriverDelivery()
        {
            List<Package> tempPackages = new List<Package>();
            List<DeliveryRoute> allDestinations = new List<DeliveryRoute>();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());


            try
            {
                string s = currentUser.PersonalNumber;
                manager.Users.Single(u => u.PersonalNumber == currentUser.PersonalNumber);
                var driver = _context.Drivers.SingleOrDefault(d => d.PersonalNumber == s);
                var packages = _context.Packages.ToList();

                allDestinations = _context.DeliveryRoutes.ToList().FindAll(d => d.Driver == driver); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                AccountController ac = new AccountController();
                ac.LogOff();
                return View("Index", "Home");
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