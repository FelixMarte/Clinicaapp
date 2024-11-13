
using Clinicaapp.Application.Base;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Clinicaapp.Application.Responses.Configuration.MedicalRecord;
using Clinicaapp.Application.Responses.Configuration.Patients;

namespace Clinicaapp.Application.Contracts
{
    public interface IPatientsService : IBaseServices<PatientsResponse, PatientsSaveDto, PatientsUpdateDto>
    {
    }
}
