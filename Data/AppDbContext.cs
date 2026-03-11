using Microsoft.EntityFrameworkCore;
using Consultorio.Api.Models;
namespace Consultorio.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}
