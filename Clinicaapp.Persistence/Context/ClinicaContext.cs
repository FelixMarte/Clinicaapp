using Clinicaapp.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Clinicaapp.Persistence.Context
{
    public class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options) { }

        #region"Configuracion de las entidades"
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Patients> Patients { get; set; }
        #endregion


    }
}
