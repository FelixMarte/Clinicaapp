using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Dtos.Configuration.Patients

{
    public class PatientsUpdateDto : BasePatients
    {
        public int PatientID { get; set; }
    }
}
