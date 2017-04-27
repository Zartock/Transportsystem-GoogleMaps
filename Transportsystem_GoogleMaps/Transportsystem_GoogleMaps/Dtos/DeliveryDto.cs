using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Dtos
{
    public class DeliveryDto
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int PackageId { get; set; }
    }
}