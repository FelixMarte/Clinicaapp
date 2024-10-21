using Clinicaapp.Domain.Base;


namespace Clinicaapp.Persistence.Models
{
    public class DoctorsModel 
    {
        public int? DoctorID { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
       
    }
}
