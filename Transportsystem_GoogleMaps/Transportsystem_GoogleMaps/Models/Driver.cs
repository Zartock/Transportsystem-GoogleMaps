using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class Driver
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name can only be 50 letters long")]
        [RegularExpression("(^[\\w]+[\\w]+\\s[\\w]+[\\w])|(^[\\w]+[\\w])", ErrorMessage = "Name can contain only letters")]

        public string Name { get; set; }

        [Required]
        [RegularExpression("(^07[0236]+\\d{7}$)", ErrorMessage = "Phone number invalid, must be e.g 0706754357")]

        public string PhoneNumber { get; set; }

        //public Delivery Delivery { get; set; }

        //public int? DeliveryId { get; set; }
    }
}