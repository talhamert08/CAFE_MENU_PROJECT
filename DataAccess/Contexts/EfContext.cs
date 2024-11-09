using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Contexts
{
    public class EfContext : DbContext
    {
        DbSet<CategoryTable> CategoryTable { get; set; } = null;
        DbSet<ProductPropertyTable> ProductPropertyTable { get; set; } = null;
        DbSet<ProductTable> ProductTable { get; set; } = null;
        DbSet<PropertyTable> PropertyTable { get; set; } = null;
        DbSet<UserTable> UserTable { get; set; } = null;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", true);

            IConfigurationRoot _configuration = config.Build();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(_configuration["SqlConnectionString"]);
        }

    }
}
