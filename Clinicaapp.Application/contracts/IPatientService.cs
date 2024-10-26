
using Clinicaapp.Application.Base;
using Clinicaapp.Application.Dtos.Configuracion.Patient;
using Clinicaapp.Application.Reponses.Configuracion.Patients;

namespace Clinicaapp.Application.contracts
{
    public class IPatientService : IBaseServices<PatientResponse, PatientSaveDto, PatientUpdateDto>
    {
    }
}
