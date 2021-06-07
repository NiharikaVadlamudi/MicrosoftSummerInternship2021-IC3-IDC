using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserStore.Models
{
    public class UserStoreContext : DbContext
    {
        public UserStoreContext(DbContextOptions<UserStoreContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("https://ms-intern-2021.documents.azure.com:443/", "GGaTLWBUeZIHMwlgR61zxAM5DwPihiPP6F9PBvpxIaBGnyEl1yXb00SPlD5Ba64QmkudYjbERnfMXpmejAsA4A==", "TenantTest");
        }

        public DbSet<User> Users{ get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("TenantSet");
            modelBuilder.Entity<User>().HasPartitionKey(c => c.partitionKey);
            modelBuilder.Entity<User>().OwnsOne(c => c.onlineDialinConferencingPolicy);
            modelBuilder.Entity<User>().OwnsOne(c => c.dataProviderErrors);
            modelBuilder.Entity<User>().OwnsMany(c => c.assignedPlans);

        }
    }
}
