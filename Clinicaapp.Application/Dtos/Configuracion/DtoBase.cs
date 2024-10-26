

namespace Clinicaapp.Application.Dtos.Configuracion
{
    public abstract class DtoBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? PhoneNumber { get; set; }
    }
}
