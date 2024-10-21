using BoletosApp.Persistance.Context;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistance.Base;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace Clinicaapp.Persistance.Repositories
{
    public class MedicalRecordRepository(ClinicaContext clinicaContext, ILogger<MedicalRecordRepository> logger) : BaseRepository<MedicalRecord>(clinicaContext), IMedicalRecordRepository
    {
        private readonly ClinicaContext _clinicaContext = clinicaContext;
        private readonly ILogger<MedicalRecordRepository> logger = logger;

        public override async Task<OperationResult> Save(MedicalRecord entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            try
            {
                await _clinicaContext.MedicalRecords.AddAsync(entity);
                await _clinicaContext.SaveChangesAsync();
                operationResult.Data = entity;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error guardando el historial médico.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public override async Task<OperationResult> Update(MedicalRecord entity)
        {
            OperationResult operationResult = new OperationResult();

            if (entity == null)
            {
                operationResult.Success = false;
                operationResult.Message = "La entidad es requerida.";
                return operationResult;
            }

            if (entity.RecordID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Se requiere el ID del historial médico.";
                return operationResult;
            }

            try
            {
                var recordToUpdate = await _clinicaContext.MedicalRecords.FindAsync(entity.RecordID);
                if (recordToUpdate == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Historial médico no encontrado.";
                    return operationResult;
                }

                recordToUpdate.Diagnosis = entity.Diagnosis;
                recordToUpdate.Treatment = entity.Treatment;
                recordToUpdate.DateOfVisit = entity.DateOfVisit;
                recordToUpdate.UpdatedAt = DateTime.Now;

                _clinicaContext.MedicalRecords.Update(recordToUpdate);
                await _clinicaContext.SaveChangesAsync();
                operationResult.Data = recordToUpdate;
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error actualizando el historial médico.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async Task<OperationResult> Remove(int recordID)
        {
            OperationResult operationResult = new OperationResult();

            if (recordID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Se requiere el ID del historial médico para eliminarlo.";
                return operationResult;
            }

            try
            {
                var recordToRemove = await _clinicaContext.MedicalRecords.FindAsync(recordID);
                if (recordToRemove != null)
                {
                    _clinicaContext.MedicalRecords.Remove(recordToRemove);
                    await _clinicaContext.SaveChangesAsync();
                    operationResult.Data = recordToRemove;
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "Historial médico no encontrado.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error eliminando el historial médico.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async Task<OperationResult> GetAll()
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult.Data = await _clinicaContext.MedicalRecords.ToListAsync();
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo los historiales médicos.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

        public async Task<OperationResult> GetById(int recordID)
        {
            OperationResult operationResult = new OperationResult();

            if (recordID <= 0)
            {
                operationResult.Success = false;
                operationResult.Message = "Se requiere el ID del historial médico.";
                return operationResult;
            }

            try
            {
                operationResult.Data = await _clinicaContext.MedicalRecords.FirstOrDefaultAsync(m => m.RecordID == recordID);
                if (operationResult.Data == null)
                {
                    operationResult.Success = false;
                    operationResult.Message = "Historial médico no encontrado.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error obteniendo el historial médico.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }

    }
}
    
