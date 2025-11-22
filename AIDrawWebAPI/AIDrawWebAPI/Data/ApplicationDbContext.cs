using AIDrawWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AIDrawWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Drawing> Drawings { get; set; }
    }
}
