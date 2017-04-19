using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Transportsystem_GoogleMaps.Models;

namespace Transportsystem_GoogleMaps.ViewModels
{
    public class DeliveryViewModel
    {
        public int Id { get; set; }

        public double? TotalDistance { get; set; }

        [Required]
        public DateTime Date { get; set; }
        

        public LinkedList<PackageCluster> PackageClusters { get; set; }
    }
}