using Clinicaapp.Domain.Base;


namespace Clinicaapp.Domain.Entities.Configuration
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
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

