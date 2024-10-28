using Clinicaapp.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Clinicaapp.Domain.Entities.Configuration
{
    [Table("MedicalRecords", Schema = "medical")]
    public class MedicalRecord
    {
        [Key] public int RecordID { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public DateTime DateOfVisit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructor
        public MedicalRecord()
        {
            CreatedAt = DateTime.Now;
        }

    }
}

