

using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistence.Base;
using Clinicaapp.Persistence.Context;
using Clinicaapp.Persistence.Exceptions;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Clinicaapp.Persistence.Models.Configuracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.Persistence.Repositories
{
    public sealed class PatientsRepository : BaseRepository<Patients>, IPatientsRepository
    {

        private readonly ILogger<Patients> logger;
        private readonly ClinicaContext context;

        public PatientsRepository(ClinicaContext clinicacontext, ILogger<Patients> logger) : base(clinicacontext)
        {
            this.logger = logger;
            this.context = clinicacontext;
        }


        public async override Task<OperationResult> Save(Patients entity)
        {
            OperationResult result = new OperationResult();
            try
            {


                var existingPatient = await context.Patients
                    .FirstOrDefaultAsync(p => p.PatientID == entity.PatientID);

                if (existingPatient != null)
                {
                    result.Succes = false;
                    result.Message = "El paciente con ese ID ya existe.";
                    return result;
                }


                ValidatePatient(entity);


                await context.Patients.AddAsync(entity);
                await context.SaveChangesAsync();

                result.Succes = true;
                result.Message = "Paciente guardado exitosamente.";
                result.Data = entity;
            }
            catch (PatientValidationException ex)
            {
                result.Succes = false;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al guardar el paciente: {ex.Message}. Detalle: {ex.InnerException?.Message}";
                logger.LogError(ex, "Error al guardar el paciente.");
            }
            return result;
        }
        public async override Task<OperationResult> Update(Patients entity)
        {
            OperationResult result = new OperationResult();
            try
            {

                ValidatePatient(entity);


                var existingPatient = await context.Patients.FindAsync(entity.PatientID);
                if (existingPatient == null)
                {
                    result.Succes = false;
                    result.Message = "El paciente no existe.";
                    return result;
                }



                existingPatient.DateOfBirth = entity.DateOfBirth;
                existingPatient.Address = entity.Address;
                existingPatient.EmergencyContactName = entity.EmergencyContactName;
                existingPatient.EmergencyContactPhone = entity.EmergencyContactPhone;
                existingPatient.BloodType = entity.BloodType;
                existingPatient.Allergies = entity.Allergies;
                existingPatient.PhoneNumber = entity.PhoneNumber;
                existingPatient.UpdatedAt = DateTime.Now;
                existingPatient.IsActive = entity.IsActive;
                



                context.Patients.Update(existingPatient);
                await context.SaveChangesAsync();

                result.Succes = true;
                result.Message = "Paciente actualizado exitosamente.";
                result.Data = existingPatient;
            }
            catch (PatientValidationException ex)
            {
                result.Succes = false;
                result.Message = ex.Message;
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al actualizar el paciente: {ex.Message}. Detalle: {ex.InnerException?.Message}";
                logger.LogError(ex, "Error al actualizar el paciente.");
            }
            return result;
        }
        public async override Task<OperationResult> GetAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var patients = await (from patient in context.Patients
                                      orderby patient.CreatedAt descending
                                      select new PatientsModel()
                                      {
                                          PatientID = patient.PatientID,
                                          Gender = patient.Gender,
                                          DateOfBirth = patient.DateOfBirth,
                                          Address = patient.Address,
                                          EmergencyContactName = patient.EmergencyContactName,
                                          EmergencyContactPhone = patient.EmergencyContactPhone,
                                          BloodType = patient.BloodType,
                                          Allergies = patient.Allergies,                                          
                                          PhoneNumber = patient.PhoneNumber,
                                          IsActive = patient.IsActive,
                                          CreatedAt = patient.CreatedAt,
                                          UpdatedAt = patient.UpdatedAt,
                                      }).ToListAsync();

                if (patients == null || !patients.Any())
                {
                    result.Succes = false;
                    result.Message = "No se encontraron pacientes en la base de datos.";
                    logger.LogWarning(result.Message);
                }
                else
                {
                    result.Succes = true;
                    result.Message = "Pacientes encontrados exitosamente.";
                    result.Data = patients;
                    logger.LogInformation("Cantidad de doctores obtenidos: {Count}", patients.Count);
                }
            }
            catch (Exception ex)
            {
                result.Succes = false;
                result.Message = $"Error al obtener los pacientes: {ex.Message}";
                logger.LogError(result.Message, ex);
            }

            return result;
        }
        public async override Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                operationResult.Data = await (from patient in context.Patients
                                              where patient.PatientID == Id
                                              select new PatientsModel()
                                              {
                                                  PatientID = patient.PatientID,
                                                  Gender = patient.Gender,
                                                  DateOfBirth = patient.DateOfBirth,
                                                  Address = patient.Address,
                                                  EmergencyContactName = patient.EmergencyContactName,
                                                  EmergencyContactPhone = patient.EmergencyContactPhone,
                                                  BloodType = patient.BloodType,
                                                  Allergies = patient.Allergies,                                                 
                                                  PhoneNumber = patient.PhoneNumber,
                                                  IsActive = patient.IsActive,
                                                  CreatedAt = patient.CreatedAt,
                                                  UpdatedAt = patient.UpdatedAt,
                                              }).FirstOrDefaultAsync();

                if (operationResult.Data == null)
                {
                    operationResult.Succes = false;
                    operationResult.Message = "No se encontró el paciente con el ID proporcionado.";
                }
                else
                {
                    operationResult.Succes = true;
                    operationResult.Message = "Paciente encontrado exitosamente.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Succes = false;
                operationResult.Message = "Error obteniendo el paciente.";
                logger.LogError(operationResult.Message, ex);
            }

            return operationResult;
        }

          public async override Task<OperationResult> Delete(int id)
          {
              OperationResult result = new OperationResult();
              try
              {
                  var existingpatient = await context.Patients
                      .FirstOrDefaultAsync(p => p.PatientID  == id);
        
                  if (existingpatient == null)
                  {
                      result.Succes = false;
                      result.Message = "El paciente no existe.";
                      return result;
                  }
        
                  context.Patients .Remove(existingpatient);
                  await context.SaveChangesAsync();
        
                  result.Succes = true;
                  result.Message = "paciente eliminado exitosamente.";
                  result.Data = existingpatient;
              }
              catch (Exception ex)
              {
                  result.Succes = false;
                  result.Message = $"Error al eliminar el pasiente: {ex.Message}. Detalle: {ex.InnerException?.Message} ";
                  logger.LogError(ex, "Error al eliminar el doctor.");
              }
        
              return result;
          }

        private void ValidatePatient(Patients entity)
        {

            if (entity.DateOfBirth > DateTime.Now)
            {
                throw new PatientValidationException("La fecha de nacimiento no puede ser una fecha futura.");
            }

            if (!string.IsNullOrWhiteSpace(entity.Address) && entity.Address.Length > 255)
            {
                throw new PatientValidationException("La dirección no puede exceder los 255 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(entity.EmergencyContactPhone) && entity.EmergencyContactPhone.Length > 15)
            {
                throw new PatientValidationException("El número de teléfono de contacto de emergencia no puede exceder los 15 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(entity.PhoneNumber) && entity.PhoneNumber.Length > 15)
            {
                throw new PatientValidationException("El número de teléfono del paciente no puede exceder los 15 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(entity.EmergencyContactPhone) && string.IsNullOrWhiteSpace(entity.EmergencyContactName))
            {
                throw new PatientValidationException("El nombre del contacto de emergencia es obligatorio si se proporciona un número de teléfono de emergencia.");
            }

            if (!string.IsNullOrWhiteSpace(entity.BloodType) && !IsValidBloodType(entity.BloodType))
            {
                throw new PatientValidationException("El tipo de sangre proporcionado no es válido.");
            }

            if (!string.IsNullOrWhiteSpace(entity.Allergies) && entity.Allergies.Length > 500)
            {
                throw new PatientValidationException("La lista de alergias no puede exceder los 500 caracteres.");
            }
        }
        private bool IsValidBloodType(string bloodType)
        {
            var validBloodTypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            return validBloodTypes.Contains(bloodType);
        }
       

       
    }
}
