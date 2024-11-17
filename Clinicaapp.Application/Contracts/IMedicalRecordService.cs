using Clinicaapp.Application.Base;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
using Clinicaapp.Application.Responses.Configuration.MedicalRecord;

namespace Clinicaapp.Application.Contracts
{
    public interface IMedicalRecordService : IBaseServices<MedicalRecordResponse, MedicalRecordSaveDto, MedicalRecordUpdateDto>
    {
        Task<MedicalRecordResponse> DeleteAsync(int id);
    }
}