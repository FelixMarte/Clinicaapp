using Clinicaapp.Application.Core;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;


namespace Clinicaapp.Application.Responses.Configuration.MedicalRecord
{
    public class MedicalRecordResponse : BaseResponse
    {
        public List<GetMedicalRecords>? Data { get; set; }
    }
}
