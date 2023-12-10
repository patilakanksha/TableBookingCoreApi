using TableBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace TableBooking.Data
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TableId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TableBooking.Models.Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
