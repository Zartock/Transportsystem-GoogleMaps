﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.Controllers
{
    public class DeliveryController : Controller
    {
        private ApplicationDbContext _context;

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
            return View();
        }
        public ActionResult Deliveries()
        {
            return View();
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