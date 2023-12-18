using IntermediateTheory_Include_Introduce_Dapper.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateTheory_Include_Introduce_Dapper.Data
{
    public class DataContextEF : DbContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public DataContextEF(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnecion")!;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(_connectionString, options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                .ToTable("Computer")
                .HasKey(c => c.ComputerId);
        }

        public DbSet<Computer>? Computer { get; set; }
    }
}
