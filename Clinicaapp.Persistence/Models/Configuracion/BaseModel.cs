

namespace Clinicaapp.Persistence.Models.Configuracion
{
    public abstract class BaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? PhoneNumber { get; set; }
    }
}
