using BoletosApp.Persistance.Context;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinicaapp.Persistence.Repositories.Configuration
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly ClinicaContext _context;

        public MedicalRecordRepository(ClinicaContext context)
        {
            _context = context;
        }

        // Método para obtener todos los registros médicos
        public async Task<OperationResult> GetAll()
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.MedicalRecords.ToListAsync();
                result.Success = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los registros médicos.";
            }
            return result;
        }

        public async Task<OperationResult> Save(MedicalRecord entity)
        {
            var result = new OperationResult();
            try
            {
                await _context.MedicalRecords.AddAsync(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al guardar el registro médico.";
            }
            return result;
        }

        public async Task<OperationResult> Update(MedicalRecord entity)
        {
            var result = new OperationResult();
            try
            {
                _context.MedicalRecords.Update(entity);
                await _context.SaveChangesAsync();
                result.Success = true;
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al actualizar el registro médico.";
            }
            return result;
        }

        public async Task<OperationResult> Remove(int id)
        {
            var result = new OperationResult();
            try
            {
                var entity = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.RecordID == id);
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "Registro médico no encontrado.";
                    return result;
                }

                entity.UpdatedAt = DateTime.UtcNow;

                _context.MedicalRecords.Update(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al eliminar el registro médico.";
            }
            return result;
        }

        public async Task<OperationResult> GetAll(Expression<Func<MedicalRecord, bool>> filter)
        {
            var result = new OperationResult();
            try
            {
                var data = await _context.MedicalRecords.Where(filter).ToListAsync();
                result.Success = true;
                result.Data = data;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los registros médicos.";
            }
            return result;
        }

        public async Task<OperationResult> GetEntityBy(int id)
        {
            var result = new OperationResult();
            try
            {
                var entity = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.RecordID == id);
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "Registro médico no encontrado.";
                    return result;
                }
                result.Success = true;
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el registro médico.";
            }
            return result;
        }

        public async Task<bool> Exists(Expression<Func<MedicalRecord, bool>> filter)
        {
            return await _context.MedicalRecords.AnyAsync(filter);
        }
    }
}
