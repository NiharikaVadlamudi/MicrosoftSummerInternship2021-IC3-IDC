using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
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

        public DbSet<User> Books { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("TenantSet");
            modelBuilder.Entity<User>().HasPartitionKey(c => c.objectId);
            modelBuilder.Entity<User>().OwnsOne(c => c.onlineDialinConferencingPolicy);
            modelBuilder.Entity<User>().OwnsOne(c => c.dataProviderErrors);
           
            //modelBuilder.Entity<User>().HasMany(c => c.assignedPlans).WithOne(e => e.Book);

        }
    }
}
