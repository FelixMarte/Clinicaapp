
using Clinicaapp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicaapp.Domain.Entities.Configuration
{
    [Table("Patients", Schema = "users")]
    public class Patients: BaseEntity
    {
        [Key]
        public int PatientID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender {  get; set; }
        public string? Address { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
    
    }
}
