using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
using Clinicaapp.Application.Responses.Configuration.MedicalRecord;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly ILogger<MedicalRecordService> _logger;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordsRepository, ILogger<MedicalRecordService> logger)
        {
            _medicalRecordRepository = medicalRecordsRepository;
            _logger = logger;
        }

        public async Task<MedicalRecordResponse> GetAll()
        {
            var medicalRecordResponse = new MedicalRecordResponse();
            try
            {
                var result = await _medicalRecordRepository.GetAll();
                if (!result.Success)
                {
                    medicalRecordResponse.Message = result.Message;
                    medicalRecordResponse.Succes = result.Success;
                    return medicalRecordResponse;
                }
                medicalRecordResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                medicalRecordResponse.Succes = false;
                medicalRecordResponse.Message = "Error obteniendo los registros médicos.";
                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
            }
            return medicalRecordResponse;
        }

        public async Task<MedicalRecordResponse> GetById(int id)
        {
            var medicalRecordResponse = new MedicalRecordResponse();
            try
            {
                var result = await _medicalRecordRepository.GetEntityBy(id);
                if (!result.Success)
                {
                    medicalRecordResponse.Message = result.Message;
                    medicalRecordResponse.Succes = result.Success;
                    return medicalRecordResponse;
                }
                medicalRecordResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                medicalRecordResponse.Succes = false;
                medicalRecordResponse.Message = "Error obteniendo el registro médico.";
                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
            }
            return medicalRecordResponse;
        }

        public async Task<MedicalRecordResponse> SaveAsync(MedicalRecordSaveDto dto)
        {
            var medicalRecordResponse = new MedicalRecordResponse();
            try
            {
                var medicalRecord = new MedicalRecord
                {
                    Diagnosis = dto.Diagnosis,
                    Treatment = dto.Treatment,
                    DateOfVisit = dto.DateOfVisit,
                    CreatedAt = dto.CreatedAt
                };

                var result = await _medicalRecordRepository.Save(medicalRecord);
                if (!result.Success)
                {
                    medicalRecordResponse.Succes = false;
                    medicalRecordResponse.Message = result.Message;
                    return medicalRecordResponse;
                }

                medicalRecordResponse.Succes = true;
                medicalRecordResponse.Message = "Registro médico guardado exitosamente.";
                medicalRecordResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                medicalRecordResponse.Succes = false;
                medicalRecordResponse.Message = "Error guardando el registro médico.";
                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
            }
            return medicalRecordResponse;
        }

        public async Task<MedicalRecordResponse> UpdateAsync(MedicalRecordUpdateDto dto)
        {
            var medicalRecordResponse = new MedicalRecordResponse();
            try
            {
                var resultGetById = await _medicalRecordRepository.GetEntityBy(dto.RecordID);
                if (!resultGetById.Success)
                {
                    medicalRecordResponse.Succes = resultGetById.Success;
                    medicalRecordResponse.Message = resultGetById.Message;
                    return medicalRecordResponse;
                }

                var medicalRecord = new MedicalRecord
                {
                    RecordID = dto.RecordID,
                    Diagnosis = dto.Diagnosis,
                    Treatment = dto.Treatment,
                    DateOfVisit = dto.DateOfVisit,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await _medicalRecordRepository.Update(medicalRecord);

                medicalRecordResponse.Succes = result.Success;
                medicalRecordResponse.Message = result.Message;
                medicalRecordResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                medicalRecordResponse.Succes = false;
                medicalRecordResponse.Message = "Error actualizando el registro médico.";
                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
            }
            return medicalRecordResponse;
        }
    }
}
