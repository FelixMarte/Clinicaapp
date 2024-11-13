using Clinicaapp.Application.Base;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
using Clinicaapp.Application.Responses.Configuration.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Contracts
{
    public interface IMedicalRecordService : IBaseServices<MedicalRecordResponse, MedicalRecordSaveDto, MedicalRecordUpdateDto>
    {
    }
}
