using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class DeliveryRoute
    {
        public int Id { get; set; }

        public Driver Driver { get; set; }

        public Package Package { get; set; }

        public DeliveryRoute()
        {
          
        }
        public DeliveryRoute(Driver driver, Package package)
        {
            this.Driver = driver;
            this.Package = package;
        }
    }
}