using Clinicaapp.Persistence.Models.Configuracion;

namespace Clinicaapp.Web.Models.Patient
{
    public class PatientGetByIdModel : BaseResultModel
    {
        public PatientsModel data { get; set; }
    }
}
