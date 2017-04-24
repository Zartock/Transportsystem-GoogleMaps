using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.Dtos
{
    public class DeliveryRouteDto
    {
        public int Id { get; set; }

        public Driver Driver { get; set; }

        public LinkedList<Package> PackageList { get; set; }
    }
}