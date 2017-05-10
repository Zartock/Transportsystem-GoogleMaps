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
        [StringLength(50, ErrorMessage = "Content can only be 50 letters long")]
        [RegularExpression("(^[\\w\\d]+[\\w\\d]+(\\s[\\w\\d]+)*)", ErrorMessage = "Please write with only letters")]
        public string Content { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Destination can only be 50 letters long")]
        public string Destination { get; set; }

        public byte? Priority { get; set; }

        public double Latitude { get; set; } = 0;
        public double Longitude { get; set; } = 0;

        public string Status { get; set; }

        public DateTime? PlanedDeliveryDate { get; set; }

        public DateTime? DateDelivered { get; set; }

        public Package()
        {
            Status = "Undelivered";
        }

        public Package(Package package)
        {
            Content = package.Content;
            Destination = package.Destination;
            Priority = package.Priority;
            Latitude = package.Latitude;
            Longitude = package.Longitude;
            Status = "Undelivered";
        }
    }
}