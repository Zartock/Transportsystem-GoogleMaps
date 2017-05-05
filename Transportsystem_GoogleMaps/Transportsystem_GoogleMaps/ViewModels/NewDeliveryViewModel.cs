using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.ViewModels
{
    public class NewDeliveryViewModel
    {

        public DateTime Date { get; set; }

        public List<Driver> Drivers = new List<Driver>();

        public List<DeliveryRoute> DeliveryRoutes { get; set; }

        public Dictionary<string, List<Package>> TestDictionary = new Dictionary<string, List<Package>>();
    }
}