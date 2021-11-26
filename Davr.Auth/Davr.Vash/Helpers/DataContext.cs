using Davr.Vash.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Davr.Vash.Helpers
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
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
