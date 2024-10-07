using IPAddress.Storage.Service.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IPAddress.Storage.Service.DataAccess
{
    public class DataContext : DbContext
    {
        #region variables

        public DbSet<User>? Users { get; set; }
        public DbSet<UserIPAddress>? UserIPAddress { get; set; }
        #endregion


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserIPAddress>()
                       .HasMany(p => p.Users)
                       .WithMany(c => c.UserIPAddresses)
                       .UsingEntity<Dictionary<string, object>>(
                        right => right
                                .HasOne<User>()
                                .WithMany(),
                            left => left
                                .HasOne<UserIPAddress>()
                                .WithMany(),
                            join => join
                       .ToTable("IP_ADDRESSES_OF_USERES"));
        }
    }
}