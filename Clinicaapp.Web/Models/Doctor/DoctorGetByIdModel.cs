using Clinicaapp.Persistence.Models;

namespace Clinicaapp.Web.Models.Doctor
{
    public class DoctorGetByIdModel : BaseResultModel
    {
        public DoctorsModel data { get; set; }
    }
}
