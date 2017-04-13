using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class PackageCluster
    {
        public LinkedList<Package> assignedPackages = new LinkedList<Package>();
        public Package centroidPackage;

        public PackageCluster(Package centroidPackage)
        {
            this.centroidPackage = centroidPackage;
        }
    }
}