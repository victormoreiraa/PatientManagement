using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PatientManagement.Domain.Entities;

namespace PatientManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {

        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Patient> Pacientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connection);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Patient>(b =>
           {
               b.ToTable("Patients");
           });
        }

    }
}