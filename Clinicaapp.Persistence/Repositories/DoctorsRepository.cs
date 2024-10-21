

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
                result.Message = $"Error al guardar el doctor: {ex.Message}";
                logger.LogError(ex, "Error al guardar el doctor.");
            }

            return result;
        }
        public async override Task<OperationResult> Update(Doctors entity)
        {
            OperationResult result = new OperationResult();
            try
            {
                ValidateDoctor(entity);


                var existingDoctor = await context.Doctors
                .FirstOrDefaultAsync(d => d.LicenseNumber == entity.LicenseNumber);

                if (existingDoctor == null)
                {
                    result.Succes = false;
                    result.Message = "El doctor no existe.";
                    return result;
                }
                context.Doctors.Update(entity);
                await context.SaveChangesAsync();

                result.Succes = true;
                result.Message = "Doctor actualizado exitosamente.";
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
                result.Message = $"Error al actualizar el doctor: {ex.Message}";
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
                                         FirstName = doctor.FirstName,
                                         LastName = doctor.LastName,
                                         Specialty = doctor.Specialty,
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
                                                 FirstName = doctor.FirstName,
                                                 LastName = doctor.LastName,
                                                 Specialty = doctor.Specialty,
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

        private void ValidateDoctor(Doctors entity)
        {
            if (string.IsNullOrWhiteSpace(entity.FirstName) || string.IsNullOrWhiteSpace(entity.LastName))
            {
                throw new DoctorValidationException("El nombre y el apellido son obligatorios.");
            }

            if (!string.IsNullOrWhiteSpace(entity.ClinicAddress) && entity.ClinicAddress.Length > 255)
            {
                throw new DoctorValidationException("La dirección de la clínica no puede exceder los 255 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(entity.Specialty))
            {
                throw new DoctorValidationException("La especialidad es obligatoria.");
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
