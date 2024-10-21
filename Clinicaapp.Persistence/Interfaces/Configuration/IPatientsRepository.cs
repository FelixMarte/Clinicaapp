using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;

namespace Clinicaapp.Persistence.Interfaces.Configuration
{
    public interface IPatientsRepository : IBaseRepository<Patients>
    {
    }
}