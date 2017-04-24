using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Transportsystem_GoogleMaps.Models;
using Transportsystem_GoogleMaps.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdminConsole
{
    class RouteLogic
    {
        private ApplicationDbContext _context;
        public LinkedList<Package> Packages;
        public int numOfDrivers;

        public RouteLogic()
        {
            _context = new ApplicationDbContext();
            numOfDrivers = _context.Drivers.ToList().Count;
            Packages = new LinkedList<Package>(_context.Packages.ToList());
        }

        public double GetDistanceLatLon(Package p1, Package p2)
        {
            var radius = 6371; //radius of the earth in km
            double dLat = 0;
            double dLon = 0;
            double a = 0;
            double c = 0;
            double d = 0;

            dLat = Deg2Rad(p2.Latitude - p1.Latitude);
            dLon = Deg2Rad(p2.Longitude - p1.Longitude);
            a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(Deg2Rad(p1.Latitude)) * Math.Cos(Deg2Rad(p2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            d = radius * c;

            return d;
        }

        public double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        public void InitializeClusters(ref LinkedList<PackageCluster> clusterCentroids)
        {
            double avgLat = 0;
            double avgLng = 0;

            foreach (var package in Packages)
            {
                avgLat += package.Latitude;
                avgLng += package.Longitude;
            }
            avgLat /= Packages.Count;
            avgLng /= Packages.Count;

            for (int i = 0; i < numOfDrivers; i++)
            {
                double angle = ((2 * Math.PI) / numOfDrivers) * (i + 1);
                Package p = new Package();
                p.Latitude = avgLat + 0.05 * Math.Cos(angle);
                p.Longitude = avgLng + 0.05 * Math.Sin(angle);
                clusterCentroids.AddLast(new PackageCluster(p));
            }
        }

        public void AssignPackagesToNearestCluster(ref LinkedList<PackageCluster> clusterCentroids)
        {
            foreach (var package in Packages)
            {
                double closestDistance = Double.PositiveInfinity;
                int closestClusterIndex = -1;
                for (int i = 0; i < clusterCentroids.Count; i++)
                {
                    double newDistance = GetDistanceLatLon(package, clusterCentroids.ElementAt(i).CentroidPackage);
                    if (newDistance < closestDistance)
                    {
                        closestDistance = newDistance;
                        closestClusterIndex = i;
                    }
                }
                clusterCentroids.ElementAt(closestClusterIndex).AssignedPackages.AddLast(package);
            }
        }

        public void AlterCentroidPosition(ref LinkedList<PackageCluster> clusterCentroids, ref bool changed)
        {
            int numChanged = 0;
            foreach (var cluster in clusterCentroids)
            {
                double tmpLat = 0;
                double tmpLon = 0;

                if (cluster.AssignedPackages.Count > 0)
                {
                    foreach (var package in cluster.AssignedPackages)
                    {
                        tmpLat += package.Latitude;
                        tmpLon += package.Longitude;
                    }
                    double newLat = Math.Round(tmpLat / cluster.AssignedPackages.Count, 5);
                    double newLon = Math.Round(tmpLon / cluster.AssignedPackages.Count, 5);
                    if (Math.Round(cluster.CentroidPackage.Latitude, 5) != newLat || Math.Round(cluster.CentroidPackage.Longitude, 5) != newLon)
                    {
                        cluster.CentroidPackage.Latitude = tmpLat / cluster.AssignedPackages.Count;
                        cluster.CentroidPackage.Longitude = tmpLon / cluster.AssignedPackages.Count;
                        numChanged++;
                    }
                }
            }
            if (numChanged == 0)
                changed = false;
        }

        public LinkedList<PackageCluster> clustering()
        {
            bool changes = true;
            LinkedList<PackageCluster> clusterCent = new LinkedList<PackageCluster>();
            InitializeClusters(ref clusterCent);

            AssignPackagesToNearestCluster(ref clusterCent);
            AlterCentroidPosition(ref clusterCent, ref changes);

            //repeat until no changes
            while (changes)
            {
                foreach (var cluster in clusterCent)
                {
                    cluster.AssignedPackages.Clear();
                }
                AssignPackagesToNearestCluster(ref clusterCent);
                AlterCentroidPosition(ref clusterCent, ref changes);
            }
            return clusterCent;
        }

        public void FixClustering()
        {
            var packages = _context.Packages.ToList();
            var driverList = _context.Drivers.ToList();
            int drivers = driverList.Count;

            List<Package> packagesWithData = new List<Package>();
            foreach (var package in packages)
            {
                var json = new WebClient().DownloadString("https://maps.googleapis.com/maps/api/geocode/json?address=" + package.Destination + "&key=AIzaSyBMVIteB6a_vtVSunhpk56yZWeTSGN2CkM");
                var data = JObject.Parse(json);
                package.Latitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lat");
                package.Longitude = (double)data["results"][0].SelectToken("geometry").SelectToken("location").SelectToken("lng");
                packagesWithData.Add(package);
            }
            _context.SaveChanges();

            Delivery d = new Delivery(drivers);
            d.Packages = new LinkedList<Package>(packages);

            LinkedList<PackageCluster> clusters = d.clustering();


            //for (int i = 0; i < drivers; i++)
            //{
            //    DeliveryRoute dr = new DeliveryRoute();
            //    dr.Driver = driverList.ElementAt(i);
            //    dr.PackageList = clusters.ElementAt(i).AssignedPackages;
            //    _context.DeliveryRoutes.Add(dr);
            //}
            //_context.SaveChanges();





        }
    }
}
