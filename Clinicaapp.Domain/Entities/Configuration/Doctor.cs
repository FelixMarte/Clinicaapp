using Clinicaapp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Doctor : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
