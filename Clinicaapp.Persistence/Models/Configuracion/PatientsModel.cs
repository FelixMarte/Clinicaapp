using Clinicaapp.Domain.Base;


namespace Clinicaapp.Persistence.Models.Configuracion
{
    public class PatientsModel 
    {
        public int PatientID { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
    }
}
