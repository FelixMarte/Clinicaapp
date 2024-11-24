
using Clinicaapp.Persistence.Models.Configuracion;

namespace Clinicaapp.Persistence.Models
{
    public class DoctorsModel : BaseModel
    {
        public int? DoctorID { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
       
    }
}
