﻿

using Clinicaapp.Domain.Base;

namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Patients : BaseEntity
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
        public int InsuranceProviderID { get; set; }
        public new DateTime CreatedAt { get; set; }
        public new DateTime? UpdatedAt { get; set; }
        public new bool IsActive { get; set; }

        public Patients()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
    }
}

