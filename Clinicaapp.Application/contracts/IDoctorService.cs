using Clinicaapp.Application.Base;
using Clinicaapp.Application.Dtos.Configuracion.Doctor;
using Clinicaapp.Application.Reponses.Configuracion.Doctors;
namespace Clinicaapp.Application.Contracts
{
    public interface IDoctorService : IBaseServices<DoctorResponse, DoctorSaveDto, DoctorUpdateDto>
    {
       
    }
}
