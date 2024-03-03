using Microsoft.EntityFrameworkCore;
using AthleteReviewApp.Models;

namespace AthleteReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Athlete> Athlete { get; set; }
        public DbSet<AthleteOwner> AthleteOwners { get; set; }
        public DbSet<AthleteCategory> AthleteCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AthleteCategory>()
                    .HasKey(pc => new { pc.AthleteId, pc.CategoryId });
            modelBuilder.Entity<AthleteCategory>()
                    .HasOne(p => p.Athlete)
                    .WithMany(pc => pc.AthleteCategories)
                    .HasForeignKey(p => p.AthleteId);
            modelBuilder.Entity<AthleteCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.AthleteCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<AthleteOwner>()
                    .HasKey(po => new { po.AthleteId, po.OwnerId });
            modelBuilder.Entity<AthleteOwner>()
                    .HasOne(p => p.Athlete)
                    .WithMany(pc => pc.AthleteOwners)
                    .HasForeignKey(p => p.AthleteId);
            modelBuilder.Entity<AthleteOwner>()
                    .HasOne(p => p.Owner)
                    .WithMany(pc => pc.AthleteOwners)
                    .HasForeignKey(c => c.OwnerId);
        }
    }
}
