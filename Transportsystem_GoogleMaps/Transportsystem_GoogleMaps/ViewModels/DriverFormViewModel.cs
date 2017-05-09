using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Transportsystem_GoogleMaps.Models;


namespace Transportsystem_GoogleMaps.ViewModels
{
    public class DriverFormViewModel
    {

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PersonalNumber { get; set; }

        public string Title
        {
            get { return Id != 0 ? "Edit Driver" : "New Driver"; }
        }

        public DriverFormViewModel()
        {
            Id = 0;
        }

        public DriverFormViewModel(Driver driver)
        {
            Id = driver.Id;
            Name = driver.Name;
            PhoneNumber = driver.PhoneNumber;
            PersonalNumber = driver.PersonalNumber;
        }
    }
}