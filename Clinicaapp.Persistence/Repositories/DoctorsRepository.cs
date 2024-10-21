

using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistence.Base;
using Clinicaapp.Persistence.Context;
using Clinicaapp.Persistence.Exceptions;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Clinicaapp.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Clinicaapp.Persistence.Repositories
{
    public sealed class DoctorsRepository : BaseRepository<Doctors>, IDoctorsRepository
    {
        private readonly ILogger<Doctors> logger;  
        private readonly ClinicaContext context;  
        public DoctorsRepository(ClinicaContext clinicacontext, ILogger<Doctors> logger) : base(clinicacontext)
        {
            this.logger = logger;
            this.context = clinicacontext;

        }
        public async override Task<OperationResult> Save(Doctors entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                var existingDoctor = await context.Doctors
                .FirstOrDefaultAsync(d => d.LicenseNumber == entity.LicenseNumber);

                if (existingDoctor != null)
                {
                    result.Succes = false;
                    result.Message = "El doctor con ese número de licencia ya existe.";
                    return result;
                }

                ValidateDoctor(entity);

                await context.Doctors.AddAsync(entity);
                await context.SaveChangesAsync();

                result.Succes = true;
                result.Message = "Doctor guardado exitosamente.";
                result.Data = entity;
            }
            catch (DoctorValidationException ex)
            {
                result.Succes = false;
                result.Message = ex.Message; 
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al guardar el doctor: {ex.Message}.detalle{ex.InnerException?.Message} ";
                logger.LogError(ex, "Error al guardar el doctor.");
            }

            return result;
        }
        public async override Task<OperationResult> Update(Doctors entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                // Obtén el doctor existente
                var existingDoctor = await context.Doctors.FindAsync(entity.DoctorID); 
                if (existingDoctor == null)
                {
                    result.Succes = false;
                    result.Message = "El doctor no existe.";
                    return result;
                }

       
                existingDoctor.YearsOfExperience = entity.YearsOfExperience;
                existingDoctor.Education = entity.Education;
                existingDoctor.Bio = entity.Bio;
                existingDoctor.ConsultationFee = entity.ConsultationFee;
                existingDoctor.ClinicAddress = entity.ClinicAddress;
                existingDoctor.LicenseNumber = entity.LicenseNumber;
                existingDoctor.LicenseExpirationDate = entity.LicenseExpirationDate;
                existingDoctor.UpdatedAt = DateTime.UtcNow; 
                existingDoctor.IsActive = entity.IsActive;
                existingDoctor.PhoneNumber = entity.PhoneNumber;

              
                await context.SaveChangesAsync();

                result.Succes = true;
                result.Message = "Doctor actualizado exitosamente.";
                result.Data = existingDoctor; 
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al actualizar el doctor: {ex.Message}. Detalle: {ex.InnerException?.Message} ";
                logger.LogError(ex, "Error al actualizar el doctor.");
            }

            return result;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var doctors = await (from doctor in context.Doctors
                                     orderby doctor.CreatedAt descending
                                     select new DoctorsModel()
                                     {
                                         DoctorID = doctor.DoctorID,
                                         LicenseNumber = doctor.LicenseNumber,
                                         PhoneNumber = doctor.PhoneNumber,
                                         YearsOfExperience = doctor.YearsOfExperience,
                                         Education = doctor.Education,
                                         Bio = doctor.Bio,
                                         ConsultationFee = doctor.ConsultationFee,
                                         ClinicAddress = doctor.ClinicAddress,
                                         LicenseExpirationDate = doctor.LicenseExpirationDate,
                                     }).ToListAsync();

                if (doctors == null || !doctors.Any())
                {
                    result.Succes = false;
                    result.Message = "No se encontraron doctores en la base de datos.";
                }
                else
                {
                    result.Succes = true;
                    result.Message = "Doctores encontrados exitosamente.";
                    result.Data = doctors;
                }
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al obtener los doctores: {ex.Message}";
                logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult.Data = await(from doctor in context.Doctors
                                             where doctor.DoctorID == Id
                                             select new DoctorsModel()
                                             {
                                                 DoctorID = doctor.DoctorID,
                                                 LicenseNumber = doctor.LicenseNumber,
                                                 PhoneNumber = doctor.PhoneNumber,
                                                 YearsOfExperience = doctor.YearsOfExperience,
                                                 Education = doctor.Education,
                                                 Bio = doctor.Bio,
                                                 ConsultationFee = doctor.ConsultationFee,
                                                 ClinicAddress = doctor.ClinicAddress,
                                                 LicenseExpirationDate = doctor.LicenseExpirationDate,

                                             }).FirstOrDefaultAsync();

                if (operationResult.Data == null)
                {
                    operationResult.Succes = false;
                    operationResult.Message = "No se encontró el doctor con el ID proporcionado.";
                }
                else
                {
                    operationResult.Succes = true;
                    operationResult.Message = "Doctor encontrado exitosamente.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Succes = false;
                operationResult.Message = "Error obteniendo el doctor.";
                logger.LogError(operationResult.Message, ex.ToString());
            }

            return operationResult;
        }
        public async override Task<OperationResult> Delete(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var existingDoctor = await context.Doctors
                    .FirstOrDefaultAsync(d => d.DoctorID == id); 

                if (existingDoctor == null)
                {
                    result.Succes = false;
                    result.Message = "El doctor no existe.";
                    return result;
                }

                context.Doctors.Remove(existingDoctor);
                await context.SaveChangesAsync();

                result.Succes = true;
                result.Message = "Doctor eliminado exitosamente.";
                result.Data = existingDoctor;
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al eliminar el doctor: {ex.Message}. Detalle: {ex.InnerException?.Message} ";
                logger.LogError(ex, "Error al eliminar el doctor.");
            }

            return result;
        }
        private void ValidateDoctor(Doctors entity)
        {

            if (!string.IsNullOrWhiteSpace(entity.ClinicAddress) && entity.ClinicAddress.Length > 255)
            {
                throw new DoctorValidationException("La dirección de la clínica no puede exceder los 255 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(entity.PhoneNumber) || entity.PhoneNumber.Length > 15)
            {
                throw new DoctorValidationException("El número de teléfono es obligatorio y no puede exceder los 15 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(entity.LicenseNumber))
            {
                throw new DoctorValidationException("El número de licencia es obligatorio.");
            }

            if (entity.YearsOfExperience < 0)
            {
                throw new DoctorValidationException("Los años de experiencia no pueden ser negativos.");
            }

            if (string.IsNullOrWhiteSpace(entity.Education))
            {
                throw new DoctorValidationException("La educación es obligatoria.");
            }

            if (entity.ConsultationFee < 0)
            {
                throw new DoctorValidationException("La tarifa de consulta no puede ser negativa.");
            }
        }




        
    }
      
}
