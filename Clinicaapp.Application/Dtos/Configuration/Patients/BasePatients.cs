using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicaapp.Application.Dtos.Configuration.Patients
{
    public class BasePatients : DtoBase
    {
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "La dirección es requerida.")]
        public required string Address { get; set; }

        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
    }
}
