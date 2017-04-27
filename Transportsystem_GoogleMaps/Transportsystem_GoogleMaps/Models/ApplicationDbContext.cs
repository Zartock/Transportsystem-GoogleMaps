using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Transportsystem_GoogleMaps.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }


        public ApplicationDbContext()
            : base("aspnet-Transportsystem_GoogleMaps-20170426044318", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

