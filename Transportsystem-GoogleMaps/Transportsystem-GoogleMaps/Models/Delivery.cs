using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public Package Package { get; set; }
    }
}