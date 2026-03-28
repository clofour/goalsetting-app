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
    }
}