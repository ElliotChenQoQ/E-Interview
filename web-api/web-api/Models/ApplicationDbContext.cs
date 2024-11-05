// ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyRevenue> CompanyRevenues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasKey(c => c.CompanyID);
            modelBuilder.Entity<CompanyRevenue>().HasKey(cr => new { cr.CompanyID, cr.YearMonth });

            modelBuilder.Entity<CompanyRevenue>()
                .HasOne<Company>()
                .WithMany()
                .HasForeignKey(cr => cr.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
