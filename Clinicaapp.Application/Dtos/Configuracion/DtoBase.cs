

namespace Clinicaapp.Application.Dtos.Configuracion
{
    public abstract class DtoBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string? PhoneNumber { get; set; }
        
    }
}
