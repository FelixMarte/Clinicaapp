namespace Clinicaapp.Domain.Entities.Configuration
{
    public class PatientsModel
    {
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public new DateTime CreatedAt { get; set; }
        public new DateTime? UpdatedAt { get; set; }
        public new bool IsActive { get; set; }


    }
}

