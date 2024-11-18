using Clinicaapp.Application.contracts;
using Clinicaapp.Application.Dtos.Configuracion.Patient;
using Clinicaapp.Application.Reponses.Configuracion.Patients;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientsRepository patientsRepository, ILogger<PatientService> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }

        public async Task<PatientResponse> GetAll()
        {
            var patientResponse = new PatientResponse();
            try
            {
                var result = await _patientsRepository.GetAll();
                if (!result.Succes)
                {
                    patientResponse.Message = result.Message;
                    patientResponse.Succes = false;
                    _logger.LogWarning("Error obteniendo pacientes: {Message}", result.Message);
                    return patientResponse;
                }

                patientResponse.Data = result.Data;
                patientResponse.Succes = true;
                patientResponse.Message = "Pacientes obtenidos exitosamente.";
                _logger.LogWarning("Error en el repositorio al obtener pacients: {Message}", result.Message);
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error obteniendo los pacientes.";
                _logger.LogError(ex, patientResponse.Message);
            }
            return patientResponse;
        }

        public async Task<PatientResponse> GetById(int id)
        {
            var patientResponse = new PatientResponse();
            try
            {
                var result = await _patientsRepository.GetEntityBy(id);
                if (!result.Succes)
                {
                    patientResponse.Message = result.Message;
                    patientResponse.Succes = false;
                    return patientResponse;
                }

                patientResponse.Data = result.Data;
                patientResponse.Succes = true;
                patientResponse.Message = "Paciente obtenido exitosamente.";
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error obteniendo el paciente.";
                _logger.LogError(ex, patientResponse.Message);
            }
            return patientResponse;
        }

        public async Task<PatientResponse> SaveAsync(PatientSaveDto dto)
        {
            var patientResponse = new PatientResponse();
            try
            {
                var patient = new Patients
                {
                    PatientID = dto.PatientID,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    PhoneNumber = dto.PhoneNumber,
                    IsActive = dto.IsActive,
                    CreatedAt = dto.CreatedAt,
                };

                var result = await _patientsRepository.Save(patient);
                patientResponse.Succes = result.Succes;
                patientResponse.Message = result.Succes ? "Paciente guardado exitosamente." : result.Message;
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error guardando el paciente.";
                _logger.LogError(ex, patientResponse.Message);
            }
            return patientResponse;
        }

        public async Task<PatientResponse> UpdateAsync(PatientUpdateDto dto)
        {
            var patientResponse = new PatientResponse();
            try
            {
                var resultGetById = await _patientsRepository.GetEntityBy(dto.PatientID);
                if (!resultGetById.Succes)
                {
                    patientResponse.Succes = resultGetById.Succes;
                    patientResponse.Message = resultGetById.Message;
                    return patientResponse;
                }

                var patient = new Patients
                {
                    PatientID = dto.PatientID,
                    DateOfBirth = dto.DateOfBirth,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,                    
                    PhoneNumber = dto.PhoneNumber,
                    IsActive = dto.IsActive,
                    UpdatedAt = dto.UpdatedAt,
                };

                var result = await _patientsRepository.Update(patient);
                patientResponse.Succes = result.Succes;
                patientResponse.Message = result.Succes ? "Paciente actualizado exitosamente." : result.Message;
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error actualizando el paciente.";
                _logger.LogError(ex, patientResponse.Message);
            }
            return patientResponse;
        }

        public async Task<PatientResponse> DeleteAsync(int id)
        {
            var patientResponse = new PatientResponse();
            try
            {
                var resultGetById = await _patientsRepository.GetEntityBy(id);
                if (!resultGetById.Succes)
                {
                    patientResponse.Succes = false;
                    patientResponse.Message = "Paciente no encontrado para eliminar.";
                    return patientResponse;
                }

                var resultDelete = await _patientsRepository.Delete(id);
                patientResponse.Succes = resultDelete.Succes;
                patientResponse.Message = resultDelete.Succes ? "Paciente eliminado exitosamente." : "Error eliminando el paciente.";
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error eliminando el paciente.";
                _logger.LogError(ex, patientResponse.Message);
            }
            return patientResponse;
        }
    }
}
