using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class DeliveryRoute
    {
        public int Id { get; set; }


        [Required]
        public Driver Driver { get; set; }

        [Required]
        public Package Package { get; set; }

    }
}