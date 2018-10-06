using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using XMasterTimeandKJV.Models;

namespace XMasterTimeandKJV.Contexts
{
    public class BaseObjectContext : DbContext
    {
        public DbSet<BaseObject> BaseObjects { get; set; }

        private string DatabasePath { get; set; }

        public BaseObjectContext()
        {

        }

        public BaseObjectContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
    public class WorkInstanceContext : DbContext
    {
        public DbSet<WorkInstance> WorkInstances { get; set; }

        private string DatabasePath { get; set; }

        public WorkInstanceContext()
        {

        }

        public WorkInstanceContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
    public class HourlyRateContext : DbContext
    {
        public DbSet<HourlyRate> HourlyRates { get; set; }

        private string DatabasePath { get; set; }

        public HourlyRateContext()
        {

        }

        public HourlyRateContext(string databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
}
