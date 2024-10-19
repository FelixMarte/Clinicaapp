

using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;

namespace Clinicaapp.Persistence.Interfaces.Configuracion
{
    public interface IDoctorsRepository : IBaseRepository<Doctors>
    {
        List<OperationResult> SaveDoctorsByDoctorId(int DoctorID);
        List<OperationResult> GetDoctorsByDoctorId(int DoctorID);
        List<OperationResult> UpdateDoctorsByDoctorId(int DoctorID);
        List<OperationResult> DeleteDoctorsByDoctorId(int DoctorID);
    }
}
