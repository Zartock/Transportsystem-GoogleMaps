using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Dtos
{


    public class PackageDto
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Destination { get; set; }
        public string Status { get; set; }

        public byte? Priority { get; set; }

        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;

        public DateTime? PlanedDeliveryDate { get; set; }

        public DateTime? DateDelivered { get; set; }
    }
}