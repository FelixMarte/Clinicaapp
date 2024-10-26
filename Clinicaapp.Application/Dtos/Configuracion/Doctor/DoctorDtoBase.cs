

namespace Clinicaapp.Application.Dtos.Configuracion.Doctor
{
    public abstract class DoctorDtoBase : DtoBase
    {
        public int YearsOfExperience { get; set; }
        public string? Education { get; set; }
        public string? Bio { get; set; }
        public decimal ConsultationFee { get; set; }
        public string? ClinicAddress { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
    }
}
