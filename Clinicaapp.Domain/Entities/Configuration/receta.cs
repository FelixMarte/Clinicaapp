namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Receta : BaseEntity
    {
        public int CitaId { get; set; }
        public string Medicamentos { get; set; }
        public string Instrucciones { get; set; }


    }
}
