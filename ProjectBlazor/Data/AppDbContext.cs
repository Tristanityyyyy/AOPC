
using Microsoft.EntityFrameworkCore;
using ProjectBlazor.Models.Entities;

namespace ProjectBlazor.Data
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",
                    Role = "Admin"
                },
                new UserAccount
                {
                    Id = 2,
                    Username = "user",
                    Password = "user123",
                    Role = "User"
                }
            );
        }
    }
}
