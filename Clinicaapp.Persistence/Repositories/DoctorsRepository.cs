

using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistence.Base;
using Clinicaapp.Persistence.Context;
using Clinicaapp.Persistence.Exceptions;
using Clinicaapp.Persistence.Interfaces.Configuracion;
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
                result.Message = ex.Message; // Usar el mensaje de la excepción
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
                // Obtener todos los doctores de la base de datos
                var doctors = await context.Doctors.ToListAsync();

                int doctorCount = await context.Doctors.CountAsync();

                if (doctorCount == 0)
                {
                    result.Succes = false;
                    result.Message = "No se encontraron doctores en la base de datos.";
                }
                else
                {
                    result.Succes = true;
                    result.Message = "Doctores encontrados exitosamente.";
                    result.Data = doctors; // Puedes ajustar el formato de los datos según tus necesidades
                }
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al obtener los doctores: {ex.Message}";
                logger.LogError(ex, "Error al obtener todos los doctores.");
            }

            return result;
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


        public List<OperationResult> DeleteDoctorsByDoctorId(int DoctorID)
        {
            throw new NotImplementedException();
        }

        public List<OperationResult> GetDoctorsByDoctorId(int DoctorID)
        {
            throw new NotImplementedException();
        }

        public List<OperationResult> SaveDoctorsByDoctorId(int DoctorID)
        {
            throw new NotImplementedException();
        }

        public List<OperationResult> UpdateDoctorsByDoctorId(int DoctorID)
        {
            throw new NotImplementedException();
        }
    }
      
}
