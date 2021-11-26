using Davr.Gumon.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Davr.Gumon.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database

            var x = Configuration.GetConnectionString("WebApiDatabase");

            options.UseNpgsql(x);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
