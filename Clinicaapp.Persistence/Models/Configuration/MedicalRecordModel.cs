using Clinicaapp.Domain.Base;


namespace Clinicaapp.Domain.Entities.Configuration
{
    public class MedicalRecordModel
    {
        public int CitaId { get; set; }
        public string? Medicamentos { get; set; }
        public string? Instrucciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
    }
}
