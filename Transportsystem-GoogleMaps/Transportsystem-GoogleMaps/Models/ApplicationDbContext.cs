using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Transportsystem_GoogleMaps.Models
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
    }
}