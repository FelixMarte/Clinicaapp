using Clinicaapp.Domain.Entities.Configuration;


namespace Clinicaaapp.web.Models.Patients
{
    public class PatientGetAllResultModel : BaseApiResponseModel
    {
        public List<PatientsModel>? data { get; set; }

    }
}
