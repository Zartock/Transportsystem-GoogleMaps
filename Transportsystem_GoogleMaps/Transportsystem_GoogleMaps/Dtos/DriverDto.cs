using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Dtos
{
    public class DriverDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }


        public int? DeliveryId { get; set; }
    }
}