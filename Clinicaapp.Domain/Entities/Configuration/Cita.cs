using Clinicaapp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Cita : BaseEntity
    {
        public int PacienteId { get; set; }
        public int DoctorId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }


    }
}
