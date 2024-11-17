using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
using Clinicaapp.Application.Responses.Configuration.MedicalRecord;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly ILogger<MedicalRecordService> _logger;

        public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, ILogger<MedicalRecordService> logger)
        {
            _medicalRecordRepository = medicalRecordRepository;
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
                // Crear la entidad MedicalRecord
                MedicalRecord medicalRecord = new MedicalRecord
                {
                    Diagnosis = dto.Diagnosis,
                    Treatment = dto.Treatment,
                    DateOfVisit = dto.DateOfVisit,
                    CreatedAt = DateTime.UtcNow
                };

                // Llamar al repositorio para guardar el registro médico
                var result = await _medicalRecordRepository.Save(medicalRecord);

                if (!result.Success)
                {
                    medicalRecordResponse.Succes = false;
                    medicalRecordResponse.Message = result.Message;
                }
                else
                {
                    medicalRecordResponse.Succes = true;
                    medicalRecordResponse.Message = "Registro médico guardado exitosamente.";
                    medicalRecordResponse.Data = result.Data;
                }
            }
            catch (Exception ex)
            {
                medicalRecordResponse.Succes = false;
                medicalRecordResponse.Message = "Error guardando el registro médico.";
                _logger.LogError(ex, "Error guardando el registro médico.");
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
        public async Task<MedicalRecordResponse> DeleteAsync(int id)
        {
            var medicalRecordResponse = new MedicalRecordResponse();
            try
            {
                var result = await _medicalRecordRepository.Remove(id);
                if (!result.Success)
                {
                    medicalRecordResponse.Succes = false;
                    medicalRecordResponse.Message = result.Message;
                    return medicalRecordResponse;
                }

                medicalRecordResponse.Succes = true;
                medicalRecordResponse.Message = "Registro médico eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                medicalRecordResponse.Succes = false;
                medicalRecordResponse.Message = "Error al eliminar el registro médico.";
            }
            return medicalRecordResponse;
        }
    }
}

//using Clinicaapp.Application.Contracts;
//using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
//using Clinicaapp.Application.Responses.Configuration.MedicalRecord;
//using Clinicaapp.Domain.Entities.Configuration;
//using Clinicaapp.Domain.Repositories;
//using Clinicaapp.Domain.Result;
//using Clinicaapp.Persistence.Interfaces.Configuration;
//using Microsoft.Extensions.Logging;

//namespace Clinicaapp.Application.Services
//{
//    public class MedicalRecordService : IMedicalRecordService
//    {
//        private readonly IMedicalRecordRepository _medicalRecordRepository;
//        private readonly ILogger<MedicalRecordService> _logger;

//        public MedicalRecordService(IMedicalRecordRepository medicalRecordsRepository, ILogger<MedicalRecordService> logger)
//        {
//            _medicalRecordRepository = medicalRecordsRepository;
//            _logger = logger;
//        }

//        public async Task<MedicalRecordResponse> GetAll()
//        {
//            var medicalRecordResponse = new MedicalRecordResponse();
//            try
//            {
//                var result = await _medicalRecordRepository.GetAll();
//                if (!result.Success)
//                {
//                    medicalRecordResponse.Message = result.Message;
//                    medicalRecordResponse.Succes = result.Success;
//                    return medicalRecordResponse;
//                }
//                medicalRecordResponse.Data = result.Data;
//            }
//            catch (Exception ex)
//            {
//                medicalRecordResponse.Succes = false;
//                medicalRecordResponse.Message = "Error obteniendo los registros médicos.";
//                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
//            }
//            return medicalRecordResponse;
//        }

//        public async Task<MedicalRecordResponse> GetById(int id)
//        {
//            var medicalRecordResponse = new MedicalRecordResponse();
//            try
//            {
//                var result = await _medicalRecordRepository.GetEntityBy(id);
//                if (!result.Success)
//                {
//                    medicalRecordResponse.Message = result.Message;
//                    medicalRecordResponse.Succes = result.Success;
//                    return medicalRecordResponse;
//                }
//                medicalRecordResponse.Data = result.Data;
//            }
//            catch (Exception ex)
//            {
//                medicalRecordResponse.Succes = false;
//                medicalRecordResponse.Message = "Error obteniendo el registro médico.";
//                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
//            }
//            return medicalRecordResponse;
//        }

//        public async Task<OperationResult> Remove(int id)
//        {
//            var operationResult = new OperationResult();

//            try
//            {
//                // Intentar obtener el registro médico por su ID
//                var result = await _medicalRecordRepository.GetEntityBy(id);

//                // Verificar si el registro médico existe
//                if (!result.Success)
//                {
//                    operationResult.Success = false;
//                    operationResult.Message = "Registro médico no encontrado.";
//                    return operationResult;
//                }

//                var medicalRecord = result.Data as MedicalRecord;

//                // Verificar si el registro médico fue encontrado
//                if (medicalRecord == null)
//                {
//                    operationResult.Success = false;
//                    operationResult.Message = "No se pudo encontrar el registro médico.";
//                    return operationResult;
//                }

//                // Eliminar el registro de la base de datos
//                var deleteResult = await _medicalRecordRepository.Remove(id);

//                if (!deleteResult.Success)
//                {
//                    operationResult.Success = false;
//                    operationResult.Message = "Error al intentar eliminar el registro médico.";
//                    return operationResult;
//                }

//                operationResult.Success = true;
//                operationResult.Message = "Registro médico eliminado exitosamente.";

//                return operationResult;
//            }
//            catch (Exception ex)
//            {
//                operationResult.Success = false;
//                operationResult.Message = "Ocurrió un error al eliminar el registro médico.";
//                // Registrar el error en los logs si es necesario
//                _logger.LogError(ex, "Error en la eliminación del registro médico.");
//                return operationResult;
//            }
//        }

//        public async Task<MedicalRecordResponse> SaveAsync(MedicalRecordSaveDto dto)
//        {
//            var medicalRecordResponse = new MedicalRecordResponse();
//            try
//            {
//                var medicalRecord = new MedicalRecord
//                {
//                    Diagnosis = dto.Diagnosis,
//                    Treatment = dto.Treatment,
//                    DateOfVisit = dto.DateOfVisit,
//                    CreatedAt = dto.CreatedAt
//                };

//                var result = await _medicalRecordRepository.Save(medicalRecord);
//                if (!result.Success)
//                {
//                    medicalRecordResponse.Succes = false;
//                    medicalRecordResponse.Message = result.Message;
//                    return medicalRecordResponse;
//                }

//                medicalRecordResponse.Succes = true;
//                medicalRecordResponse.Message = "Registro médico guardado exitosamente.";
//                medicalRecordResponse.Data = result.Data;
//            }
//            catch (Exception ex)
//            {
//                medicalRecordResponse.Succes = false;
//                medicalRecordResponse.Message = "Error guardando el registro médico.";
//                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
//            }
//            return medicalRecordResponse;
//        }

//        public async Task<MedicalRecordResponse> UpdateAsync(MedicalRecordUpdateDto dto)
//        {
//            var medicalRecordResponse = new MedicalRecordResponse();
//            try
//            {
//                var resultGetById = await _medicalRecordRepository.GetEntityBy(dto.RecordID);
//                if (!resultGetById.Success)
//                {
//                    medicalRecordResponse.Succes = resultGetById.Success;
//                    medicalRecordResponse.Message = resultGetById.Message;
//                    return medicalRecordResponse;
//                }

//                var medicalRecord = new MedicalRecord
//                {
//                    RecordID = dto.RecordID,
//                    Diagnosis = dto.Diagnosis,
//                    Treatment = dto.Treatment,
//                    DateOfVisit = dto.DateOfVisit,
//                    UpdatedAt = DateTime.UtcNow
//                };

//                var result = await _medicalRecordRepository.Update(medicalRecord);

//                medicalRecordResponse.Succes = result.Success;
//                medicalRecordResponse.Message = result.Message;
//                medicalRecordResponse.Data = result.Data;
//            }
//            catch (Exception ex)
//            {
//                medicalRecordResponse.Succes = false;
//                medicalRecordResponse.Message = "Error actualizando el registro médico.";
//                _logger.LogError(medicalRecordResponse.Message, ex.ToString());
//            }
//            return medicalRecordResponse;
//        }
//    }
//}
