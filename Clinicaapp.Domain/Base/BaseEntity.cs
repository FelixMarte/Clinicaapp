using System.ComponentModel.DataAnnotations.Schema;

namespace Clinicaapp.Domain.Base
{
    public abstract class BaseEntity
    {

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? PhoneNumber { get; set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
