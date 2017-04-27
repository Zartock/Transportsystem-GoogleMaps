using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class PackageCluster
    {
        public LinkedList<Package> AssignedPackages = new LinkedList<Package>();
        public Package CentroidPackage = new Package();

        public PackageCluster(Package centroidPackage)
        {
            CentroidPackage.Latitude = centroidPackage.Latitude;
            CentroidPackage.Longitude = centroidPackage.Longitude;
        }
    }
}