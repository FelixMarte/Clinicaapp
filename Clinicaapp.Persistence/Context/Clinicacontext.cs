using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinicaapp.Domain.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BoletosApp.Persistance.Context
{
    public partial class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options) : base(options)
        {

        }

        #region "Configuration Entities"
        public DbSet<Patients> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        #endregion

    }
}