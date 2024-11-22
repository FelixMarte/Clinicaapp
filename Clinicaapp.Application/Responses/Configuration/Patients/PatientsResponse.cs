using Clinicaapp.Application.Core;
using Clinicaapp.Application.Dtos.Configuration.Patients;


namespace Clinicaapp.Application.Responses.Configuration.Patients
{
    public class PatientsResponse : BaseResponse
    {
        public List<GetPatients>? Data { get; set; }
    }
}
