namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Tratamiento : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
    }
}
