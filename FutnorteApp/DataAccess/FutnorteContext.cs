using FutnorteApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace FutnorteApp.DataAccess
{
    internal class FutnorteContext : DbContext
    {
        // Create the DbSets
        public DbSet<Team> Teams { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Field> Fields { get; set; }

        // Configuring the database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // SQL Server connection string
                string connectionString = "Server=DESKTOP-QS89OD2;Database=Futnorte;Trusted_Connection = True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}


