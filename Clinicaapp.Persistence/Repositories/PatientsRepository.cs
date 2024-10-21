using BoletosApp.Persistance.Context;
using Clinicaapp.Domain.Base;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Domain.Result;
using Clinicaapp.Persistance.Base;
using Clinicaapp.Persistance.Repositories;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;



namespace Clinicaapp.Persistence.Repositories.Configuracion

{
    public class PatientsRepository(ClinicaContext clinicaContext,
                  ILogger<PatientsRepository> logger) : BaseRepository<Patients>(clinicaContext), IPatientsRepository
    {
        private readonly ClinicaContext _clinicaContext;
        private readonly ILogger<PatientsRepository> _logger;


        private OperationResult ValidateEntity(Patients entity, bool checkId = false)
        {
            var result = new OperationResult();

            if (entity == null)
            {
                result.Success = false;
                result.Message = "La entidad es requerida.";
                return result;
            }

            if (checkId && entity.PatientID <= 0)
            {
                result.Success = false;
                result.Message = "Se requiere enviar el ID del Paciente para realizar esta operación.";
                return result;
            }

            if (string.IsNullOrEmpty(entity.PhoneNumber) || string.IsNullOrEmpty(entity.Address))
            {
                result.Success = false;
                result.Message = "El teléfono y la dirección son requeridos.";
                return result;
            }

            return result;
        }
        private void UpdatePatientFields(Patients patientToUpdate, Patients entity)
        {
            patientToUpdate.PhoneNumber = entity.PhoneNumber;
            patientToUpdate.Address = entity.Address;
            patientToUpdate.DateOfBirth = entity.DateOfBirth;
            patientToUpdate.EmergencyContactName = entity.EmergencyContactName;
            patientToUpdate.EmergencyContactPhone = entity.EmergencyContactPhone;
            patientToUpdate.Gender = entity.Gender;
            patientToUpdate.BloodType = entity.BloodType;
            patientToUpdate.Allergies = entity.Allergies;
            patientToUpdate.InsuranceProviderID = entity.InsuranceProviderID;
            patientToUpdate.UpdateTimestamp(); 
        }
        public async override Task<OperationResult> Save(Patients entity)
        {
            var validation = ValidateEntity(entity);
            if (!validation.Success)
                return validation;

            try
            {
                return await base.Save(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error guardando el Paciente.", ex);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error guardando el Paciente."
                };

            }
        }
        public async override Task<OperationResult> Update(Patients entity)
        {
            var validation = ValidateEntity(entity, true);
            if (!validation.Success)
                return validation;

            try
            {
                var patientToUpdate = await _clinicaContext.Patients.FindAsync(entity.PatientID);
                if (patientToUpdate == null)
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Paciente no encontrado."
                    };

                // Actualizar los datos del paciente
                UpdatePatientFields(patientToUpdate, entity);

                return await base.Update(patientToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error actualizando el Paciente.", ex);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error actualizando el Paciente."
                };
            }
        }
        public async override Task<OperationResult> Remove(Patients entity)
        {
            var validation = ValidateEntity(entity, true);
            if (!validation.Success)
                return validation;

            try
            {
                var patientToRemove = await _clinicaContext.Patients.FindAsync(entity.PatientID);
                if (patientToRemove != null)
                {
                    patientToRemove.IsActive = false;
                    patientToRemove.UpdateTimestamp();
                    return await base.Update(patientToRemove);
                }
                else
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Paciente no encontrado."
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error eliminando el Paciente.", ex);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error eliminando el Paciente."
                };
            }
        }
        public async override Task<OperationResult> GetAll()
        {
            try
            {
                var patients = await _clinicaContext.Patients.Where(p => p.IsActive).ToListAsync();
                return new OperationResult
                {
                    Success = true,
                    Data = patients
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error obteniendo los Pacientes.", ex);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error obteniendo los Pacientes."
                };
            }
        }
        public async override Task<OperationResult> GetEntityBy(int id)
        {
            try
            {
                var patient = await _clinicaContext.Patients
                    .Where(p => p.IsActive && p.PatientID == id)
                    .FirstOrDefaultAsync();

                if (patient == null)
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Paciente no encontrado."
                    };

                return new OperationResult
                {
                    Success = true,
                    Data = patient
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error obteniendo el Paciente.", ex);
                return new OperationResult
                {
                    Success = false,
                    Message = "Error obteniendo el Paciente."
                };
            }
        }
    }
}
