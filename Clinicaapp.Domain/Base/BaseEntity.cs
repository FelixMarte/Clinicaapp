﻿
namespace Clinicaapp.Domain.Base
{
    public abstract class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
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