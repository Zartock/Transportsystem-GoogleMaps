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

        /*public void InitializeClusters(ref LinkedList<PackageCluster> clusterCentroids)
        {
            // TODO 1. Initalize cluster centers
            bool notAssigned;
            for (int i = 0; i < numOfDrivers; i++)
            {
                // assign numOfClusters clusters uniqely by picking random packages
                notAssigned = true;
                while (notAssigned)
                {
                    Random rnd = new Random();
                    int packageIndex = rnd.Next(Packages.Count());

                    bool exists = false;
                    foreach (var cluster in clusterCentroids)
                    {
                        if (cluster.centroidPackage == Packages.ElementAt(packageIndex))
                            exists = true;
                    }
                    if (!exists)
                    {
                        clusterCentroids.AddLast(new PackageCluster(Packages.ElementAt(packageIndex)));
                        notAssigned = false;
                    }

                    
                }
            }
        }

        public LinkedList<PackageCluster> AssignPackagesToNearestCluster(LinkedList<PackageCluster> clusterCentroids, ref bool clusterChanged)
        {
           // LinkedList<PackageCluster> tmp = new LinkedList<PackageCluster>(clusterCentroids);            
          //  clusterCentroids.Clear();
            clusterChanged = true;
            // TODO 2. Calculate distance between each package and cluster center
            // TODO 3. Assign package to its closest cluster center
            for (int i = 0; i < Packages.Count(); i++)
            {
                // assign packages to first centroid to start with
                PackageCluster nearest = clusterCentroids.ElementAt(0);
                int nearestIndex = 0;
                double distanceToNearest = GetDistanceLatLon(Packages.ElementAt(i), nearest.centroidPackage);

                // check if the other clusters are closer to the package and reassign
                for (int j = 1; j < clusterCentroids.Count(); j++)
                {
                    if (GetDistanceLatLon(Packages.ElementAt(i), clusterCentroids.ElementAt(j).centroidPackage ) < distanceToNearest)
                    {
                        nearest = clusterCentroids.ElementAt(j);
                        distanceToNearest = GetDistanceLatLon(Packages.ElementAt(i), clusterCentroids.ElementAt(j).centroidPackage);
                        nearestIndex = j;
                        clusterChanged = false;
                    }
                }
               clusterCentroids.ElementAt(nearestIndex).assignedPackages.AddLast(Packages.ElementAt(i));
               
            }
            //clusterCentroids = tmp;
            //tmp.Clear();
            return clusterCentroids;
        }

        public void clustering()
        {
    
            LinkedList<LinkedList<PackageCluster>> clusterCentroidsOptions = new LinkedList<LinkedList<PackageCluster>>();

            for (int k = 0; k < 10; k++)
            {
                LinkedList<PackageCluster> clusterCentroids = new LinkedList<PackageCluster>();
                InitializeClusters(ref clusterCentroids);
                bool clustersChanged = false;
                while (!clustersChanged)
                {
                    clusterCentroids = AssignPackagesToNearestCluster(clusterCentroids, ref clustersChanged);

                    for (int i = 0; i < clusterCentroids.Count; i++)
                    {
                        double tempLat = 0;
                        double tempLng = 0;
                        for (int j = 0; j < clusterCentroids.ElementAt(i).assignedPackages.Count; j++)
                        {
                            tempLat += clusterCentroids.ElementAt(i).assignedPackages.ElementAt(j).Latitude;
                            tempLng += clusterCentroids.ElementAt(i).assignedPackages.ElementAt(j).Longitude;
                        }
                        tempLat = tempLat / clusterCentroids.ElementAt(i).assignedPackages.Count;
                        tempLng = tempLng / clusterCentroids.ElementAt(i).assignedPackages.Count;
                        clusterCentroids.ElementAt(i).centroidPackage.Longitude = tempLng;
                        clusterCentroids.ElementAt(i).centroidPackage.Latitude = tempLat;
                    }
                    if (clustersChanged)
                    {
                        clusterCentroidsOptions.AddLast(clusterCentroids);
                    }
                    for (int i = 0; i < clusterCentroids.Count; i++)
                    {
                        clusterCentroids.ElementAt(i).assignedPackages.Clear();
                    }
                }
               
                
             
            }
            LinkedList<PackageCluster> clusterCentroidsResult = new LinkedList<PackageCluster>();
            for (int m = 0; m < numOfDrivers; m++)
            {

                double meanLat = 0;
                double meanLng = 0;
                for (int n = 0; n < clusterCentroidsOptions.Count; n++)
                {
                    //for (int i = 0; i < clusterCentroidsOptions.ElementAt(n).Count; i++)
                    //{
                    //    meanLng += clusterCentroidsOptions.ElementAt(n).ElementAt(i).centroidPackage.Longitude;
                    //    meanLat += clusterCentroidsOptions.ElementAt(n).ElementAt(i).centroidPackage.Latitude;
                    //}
           
                }
               
            }
                
                
            





            // PackageCluster packageCluster = new PackageCluster();




            // TODO 4. Calculate new cluster positions as the mean of all packages in the cluster
            // TODO 5. Loop through 2-4 until no new assignments being made
        }*/
    }
}