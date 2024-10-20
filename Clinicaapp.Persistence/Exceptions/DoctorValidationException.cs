

using Clinicaapp.Domain.Entities.Configuration;

namespace Clinicaapp.Persistence.Exceptions
{
    public class DoctorValidationException : Exception
    {
        public DoctorValidationException(string message) : base(message) { }
     

    }
}
