using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserTenantTestAPI.Models
{
    public class UserStoreContext : DbContext
    {
        public UserStoreContext(DbContextOptions<UserStoreContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos("***", "***", "***");
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("FinalUserSet");
            modelBuilder.Entity<User>().HasPartitionKey(c => c.pk);
            modelBuilder.Entity<User>().OwnsOne(c => c.onlineDialinConferencingPolicy);
            modelBuilder.Entity<User>().OwnsOne(c => c.dataProviderErrors);
            modelBuilder.Entity<User>().OwnsMany(c => c.assignedPlans);

        }
    }
}
