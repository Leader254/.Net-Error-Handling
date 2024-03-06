using CrudBreakfast.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudBreakfast.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BreakFast> Breakfasts { get; set; }
    }
}