using Clinicaapp.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Receta : BaseEntity
    {
        public int CitaId { get; set; }
        public string Medicamentos { get; set; }
        public string Instrucciones { get; set; }


    }
}
