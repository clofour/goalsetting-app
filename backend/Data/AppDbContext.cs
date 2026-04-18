using backend.Enums;
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<User>(options)
    {

        public DbSet<AccessCode> AccessCodes { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Reflection> Reflections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Goal>()
                .HasDiscriminator<GoalType>("GoalType")
                .HasValue<NorthStar>(GoalType.NorthStar)
                .HasValue<Movement>(GoalType.Movement)
                .HasValue<Bearing>(GoalType.Bearing);

            builder.Entity<Goal>()
                .HasOne(goal => goal.Parent)
                .WithMany(goal => goal.Children)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}