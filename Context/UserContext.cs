using JsonWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonWT.Context
{
    public class UserContext:DbContext

    {
        public UserContext(DbContextOptions<UserContext> options) : base(GetOptions()) { 


        }

        private static DbContextOptions GetOptions()
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), "Server=localhost;Database=jwt;Trusted_Connection=True;TrustServerCertificate=True").Options;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
