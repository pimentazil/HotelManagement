using HotelManagement.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<TabHotel> tabHotel { get; set; }
        public DbSet<TabHospedes> tabHospedes { get; set; }
        public DbSet<CheckIn> CheckIn { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabHotel>().ToTable("tabHotel");
            modelBuilder.Entity<TabHospedes>().ToTable("tabHospedes");
            modelBuilder.Entity<CheckIn>().ToTable("checkIn");
        }
    }
}
