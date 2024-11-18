using Clinicaapp.Persistence.Models.Configuracion;

namespace Clinicaapp.Web.Models.Patient
{
    public class PatientGetAllResultModel :BaseResultModel
    {
        public List<PatientsModel> data { get; set; }
    }
}
