namespace Clinicaapp.Domain.Entities.Configuration
{
    public class Doctor : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
