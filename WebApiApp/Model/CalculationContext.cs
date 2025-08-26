using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace WebApiApp.Model
{
    public class CalculationContext : DbContext
    {
        public CalculationContext(DbContextOptions<CalculationContext> option) : base(option) { }

        string constring = @"Data Source=IBM-GJ63SB4\SQLEXPRESS;Initial Catalog = ApiDB; TrustServerCertificate= True; Integrated Security = True";
        public DbSet<Calculation> Calculations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(constring);

        }
    }
}