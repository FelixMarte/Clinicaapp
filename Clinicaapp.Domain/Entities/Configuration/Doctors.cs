using Clinicaapp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicaapp.Domain.Entities.Configuration
{
    [Table("Doctors", Schema ="users")]
    public class Doctors : BaseEntity
    {
        [Key]


        public int DoctorID { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Education {  get; set; }
        public string? Bio {  get; set; }
        public decimal ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
  



    }
}