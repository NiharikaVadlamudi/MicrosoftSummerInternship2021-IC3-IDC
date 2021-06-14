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
            optionsBuilder.UseCosmos("https://niharikavad.documents.azure.com:443/", "TqTWepaPTIehm7pmhwBfINNbfXAubhBLH0sQi5jkRFsOgzsWZNnzJB0fjtVTWPzVtbCFGAiUGC9aGPqiqhX9Gg==", "TenantTest");
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
