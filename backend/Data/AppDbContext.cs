using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using backend.Viewmodels;

namespace backend.Data
{
    public class WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
        : IdentityDbContext<User, Role, Guid>(options)
    {

        public DbSet<AccessCode> AccessCodes { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}