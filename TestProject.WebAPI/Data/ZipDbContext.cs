using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TestProject.WebAPI.Data
{
    
    /// <summary>
    /// Zip Database Context
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ZipDbContext : DbContext
    {
        public ZipDbContext(DbContextOptions<ZipDbContext> options):base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }    
    }
}
