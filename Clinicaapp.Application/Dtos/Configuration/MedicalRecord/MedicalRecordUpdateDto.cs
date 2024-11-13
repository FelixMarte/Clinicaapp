using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Dtos.Configuration.MedicalRecord
{
    public class MedicalRecordUpdateDto : BaseMedicalRecord
    {
        public int RecordID { get; set; }
    }
}
