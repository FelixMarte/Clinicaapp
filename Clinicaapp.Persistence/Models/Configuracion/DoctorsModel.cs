using Clinicaapp.Domain.Base;


namespace Clinicaapp.Persistence.Models
{
    public class DoctorsModel : BaseEntity
    {
        public int? DoctorID { get; set; }
        public string? Specialty { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Bio { get; set; }
        public float ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
    }
}
