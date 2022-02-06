using CsvHelper;
using EnsekGlobal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace DataLibrary
{
    public class EnsekDbContext : DbContext
    {
        public EnsekDbContext(DbContextOptions<EnsekDbContext> options) : base(options)
        {

        }

        public DbSet<AccountDM> Accounts { get; set; }
        public DbSet<MeterReadingsDM> MeterReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DbConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            using (var sr = new StreamReader($"{solutionDirectory}/ENSEKTest/ENSEK_Technical_Test/wwwroot/Test_Accounts.csv"))
            using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                var accounts = csv.GetRecords<AccountDM>();
                modelBuilder.Entity<AccountDM>().HasData(accounts);
            }
        }
    }
}