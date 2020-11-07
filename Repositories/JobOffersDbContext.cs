using JobOffersMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace JobOffersMVC.Repositories
{
    public class JobOffersDbContext : DbContext
    {
        public JobOffersDbContext(DbContextOptions<JobOffersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<UserApplication> UserApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserApplications)
                .WithOne()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobOffer>()
                .HasMany(j => j.UserApplications)
                .WithOne()
                .HasForeignKey(ua => ua.JobOfferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplication>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserApplications);

            modelBuilder.Entity<UserApplication>()
                .HasOne(ua => ua.JobOffer)
                .WithMany(j => j.UserApplications);

            modelBuilder.Entity<JobOffer>()
                .HasOne(j => j.User);
        }
    }
}
