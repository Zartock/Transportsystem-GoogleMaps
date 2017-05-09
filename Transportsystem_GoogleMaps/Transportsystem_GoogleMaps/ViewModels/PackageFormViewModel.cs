using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.ViewModels
{
    public class PackageFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Destination { get; set; }

        public string Status { get; set; }

        public string Title
        {
            get { return Id != 0 ? "Edit Package" : "New Package"; }
        }

        public PackageFormViewModel()
        {
            Id = 0;
            Status = "Undelivered";
        }

        public PackageFormViewModel(Package package)
        {
            Id = package.Id;
            Content = package.Content;
            Destination = package.Destination;
            Status = "Undelivered";
        }

    }
}