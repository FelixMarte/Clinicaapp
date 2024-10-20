

using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;

namespace Clinicaapp.Persistence.Interfaces.Configuracion
{
    public interface IDoctorsRepository : IBaseRepository<Doctors>
    {

        List<OperationResult> GetDoctorsByDoctorId(int DoctorID);
    
    }
}
