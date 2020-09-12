using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace José_finLogistics.Models
{
    public class LogisticsContext : DbContext
    {
        public LogisticsContext() : base("LogisticsContext")
        {
        }

        public DbSet<Admin> AdminTable { get; set; }
        public DbSet<Client> ClientTable { get; set; }
        public DbSet<Package> PackageTable { get; set; }
    }
}