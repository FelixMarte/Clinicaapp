using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Clinicaapp.Application.Responses.Configuration.Patients;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.Extensions.Logging;


namespace Clinicaapp.Application.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsService> _logger;

        public PatientsService(IPatientsRepository patientsRepository, ILogger<PatientsService> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }

        public async Task<PatientsResponse> GetAll()
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var result = await _patientsRepository.GetAll();
                if (!result.Success)
                {
                    patientsResponse.Message = result.Message;
                    patientsResponse.Succes = result.Success;
                    return patientsResponse;
                }
                patientsResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error obteniendo los pacientes.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }

        public async Task<PatientsResponse> GetById(int id)
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var result = await _patientsRepository.GetEntityBy(id);
                if (!result.Success)
                {
                    patientsResponse.Message = result.Message;
                    patientsResponse.Succes = result.Success;
                    return patientsResponse;
                }
                patientsResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error obteniendo el paciente.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }

        public async Task<PatientsResponse> SaveAsync(PatientsSaveDto dto)
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var patient = new Patients
                {
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    IsActive = dto.IsActive,
                    CreatedAt = dto.CreatedAt
                };

                var result = await _patientsRepository.Save(patient);
                if (!result.Success)
                {
                    patientsResponse.Succes = false;
                    patientsResponse.Message = result.Message;
                    return patientsResponse;
                }

                patientsResponse.Succes = true;
                patientsResponse.Message = "Paciente guardado exitosamente.";
                patientsResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error guardando el paciente.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }

        public async Task<PatientsResponse> UpdateAsync(PatientsUpdateDto dto)
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var resultGetById = await _patientsRepository.GetEntityBy(dto.PatientID);
                if (!resultGetById.Success)
                {
                    patientsResponse.Succes = resultGetById.Success;
                    patientsResponse.Message = resultGetById.Message;
                    return patientsResponse;
                }

                var patient = new Patients
                {
                    PatientID = dto.PatientID,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    IsActive = dto.IsActive,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await _patientsRepository.Update(patient);

                patientsResponse.Succes = result.Success;
                patientsResponse.Message = result.Message;
                patientsResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error actualizando el paciente.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }
    }
}
