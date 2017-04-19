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

        public double? TotalDistance { get; set; }
        public int numOfDrivers { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public Delivery(int numOfDrivers)
        {
            this.numOfDrivers = numOfDrivers;
        }
        public LinkedList<Package> Packages { get; set; }

            public void AddPackageToDeliveryList(Package p)
            {
                Packages.AddLast(p);
            }

            public double CalculateRouteCost(LinkedList<Package> route)
            {
                double totalCost = 0;
                for (int i = 0; i < route.Count - 1; i++)
                {
                    totalCost += GetDistanceLatLon(route.ElementAt(i), route.ElementAt(i + 1));
                }
                return totalCost;
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


        public Package FindMostDistantPackage(Package p, ref List<int> avNumbers)
        {
            int packageIndex = -1;
            double oldDistance = Double.NegativeInfinity;
            int i = 0;
            foreach (var package in Packages)
            {
                double newDistance = GetDistanceLatLon(package, p);
                if (newDistance > oldDistance)
                {
                    if (avNumbers.Contains(i))
                    {
                        oldDistance = newDistance;
                        packageIndex = i;
                        avNumbers[i] = -1;
                    }
                    
                }
                i++;
            }
            return Packages.ElementAt(packageIndex);
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
            
            
            /*Random rnd = new Random();
            List<int> avNumbers = new List<int>();
            for (int n = 0; n < Packages.Count; n++)
            {
                avNumbers.Add(n);
            }


            bool notAssigned;
            for (int i = 0; i < numOfDrivers; i++)
            {
                notAssigned = true;
                while (notAssigned)
                {
                    int packageIndex = rnd.Next(Packages.Count);

                    if (avNumbers.Contains(packageIndex))
                    {
                        clusterCentroids.AddLast(new PackageCluster(Packages.ElementAt(packageIndex)));
                        notAssigned = false;
                        avNumbers[packageIndex] = -1;
                    }
                }
            }*/



        }

        public void InitializeClusters(ref LinkedList<PackageCluster> clusterCentroids, List<double> latList, List<double> lngList)
        {        
            for (int i = 0; i < numOfDrivers; i++)
            {
                //notAssigned = true;
                //while (notAssigned)
                //{
                //    int packageIndex = rnd.Next(Packages.Count);

                //    if (avNumbers.Contains(packageIndex))
                //    {
                //        clusterCentroids.AddLast(new PackageCluster(Packages.ElementAt(packageIndex)));
                //        notAssigned = false;
                //        avNumbers[packageIndex] = -1;
                //    }
                //}

                Package p = new Package();
                p.Latitude = latList[i];
                p.Longitude = latList[i];
                clusterCentroids.AddLast(new PackageCluster(p));
            }
        }



        public void AssignPackagesToNearestCluster(ref LinkedList<PackageCluster> clusterCentroids)
        {
            //foreach (var cluster in clusterCentroids)
            //{
            //    cluster.AssignedPackages.Clear();
            //}

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
    
            LinkedList<LinkedList<PackageCluster>> clusterCentroidsOptions = new LinkedList<LinkedList<PackageCluster>>();
            bool changes = true;
            List<double> latList = new List<double>();
            List<double> lngList = new List<double>();
            int iterations = 1;
            for (int i = 0; i < numOfDrivers; i++)
            {
                latList.Add(0);
                lngList.Add(0);
            }

            for (int k = 0; k < iterations; k++)
            {
                changes = true;
                LinkedList<PackageCluster> clusterCentroids = new LinkedList<PackageCluster>();
                InitializeClusters(ref clusterCentroids);

                //first init
                AssignPackagesToNearestCluster(ref clusterCentroids);
                AlterCentroidPosition(ref clusterCentroids, ref changes);

                //repeat until no changes
                while (changes)
                {
                    AssignPackagesToNearestCluster(ref clusterCentroids);
                    AlterCentroidPosition(ref clusterCentroids, ref changes);
                }
                for (int i = 0; i < numOfDrivers; i++)
                {
                    latList[i] += clusterCentroids.ElementAt(i).CentroidPackage.Latitude;
                    lngList[i] += clusterCentroids.ElementAt(i).CentroidPackage.Longitude;
                }
            }

            for (int i = 0; i < numOfDrivers; i++)
            {
                latList[i] /= iterations;
                lngList[i] /= iterations;
            }

            LinkedList<PackageCluster> clusterCent = new LinkedList<PackageCluster>();
            InitializeClusters(ref clusterCent);

            AssignPackagesToNearestCluster(ref clusterCent);
            AlterCentroidPosition(ref clusterCent, ref changes);

            //repeat until no changes
            while (changes)
            {
                AssignPackagesToNearestCluster(ref clusterCent);
                AlterCentroidPosition(ref clusterCent, ref changes);
            }

            foreach (var cluster in clusterCent)
            {
                if (cluster.AssignedPackages.Count < 3)
                {
                    clusterCent = clustering();
                    break;
                }
                    
            }
            return clusterCent;
        }
    }
}