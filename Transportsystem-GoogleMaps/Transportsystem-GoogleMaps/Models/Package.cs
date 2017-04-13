using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Transportsystem_GoogleMaps.Models
{
    public class Package
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Content { get; set; }

        [Required]
        [StringLength(255)]
        public string Destination { get; set; }

        public byte? Priority { get; set; }

        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;
  
    }
}