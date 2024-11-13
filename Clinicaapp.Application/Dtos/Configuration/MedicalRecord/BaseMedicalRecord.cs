using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Dtos.Configuration.MedicalRecord
{
    public class BaseMedicalRecord : DtoBase
    {
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }


    }
}
