using Clinicaapp.Domain.Entities.Configuration;

namespace Clinicaaapp.web.Models.Patients
{
    public class PatientGetByIdModel : BaseApiResponseModel
    {
        public PatientsModel? Data { get; set; }

    }
}
