using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Clinicaapp.Application.Responses.Configuration.Patients;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Clinicaapp.Application.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsService> _logger;

        public PatientsService(IPatientsRepository patientsRepository, ILogger<PatientsService> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }

        public async Task<PatientsResponse> GetAll()
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var result = await _patientsRepository.GetAll();
                if (!result.Success)
                {
                    patientsResponse.Message = result.Message;
                    patientsResponse.Succes = result.Success;
                    return patientsResponse;
                }

                // Aquí conviertes los registros a la estructura adecuada.
                var patients = result.Data as IEnumerable<Patients>;

                if (patients != null)
                {
                    patientsResponse.Data = patients.Select(p => new GetPatients
                    {
                        PatientID = p.PatientID,
                        DateOfBirth = p.DateOfBirth,
                        Gender = p.Gender,
                        PhoneNumber = p.PhoneNumber,
                        Address = p.Address,
                        EmergencyContactName = p.EmergencyContactName,
                        EmergencyContactPhone = p.EmergencyContactPhone,
                        BloodType = p.BloodType,
                        Allergies = p.Allergies,
                        CreatedAt = p.CreatedAt,
                        IsActive = p.IsActive
                    }).ToList();
                }
                else
                {
                    patientsResponse.Message = "No se encontraron pacientes válidos.";
                    patientsResponse.Succes = false;
                }

                patientsResponse.Succes = true;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error obteniendo los pacientes.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }

        public async Task<PatientsResponse> GetById(int id)
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var result = await _patientsRepository.GetEntityBy(id);
                if (!result.Success)
                {
                    patientsResponse.Message = result.Message;
                    patientsResponse.Succes = result.Success;
                    return patientsResponse;
                }

                // Aquí conviertes el registro a la estructura adecuada.
                var patient = result.Data;
                patientsResponse.Data = new List<GetPatients>
        {
            new GetPatients
            {
                PatientID = patient.PatientID,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                EmergencyContactName = patient.EmergencyContactName,
                EmergencyContactPhone = patient.EmergencyContactPhone,
                BloodType = patient.BloodType,
                Allergies = patient.Allergies,
                CreatedAt = patient.CreatedAt,
                IsActive = patient.IsActive
            }
        };

                patientsResponse.Succes = true;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error obteniendo el paciente.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }


        public async Task<PatientsResponse> SaveAsync(PatientsSaveDto dto)
        {
            PatientsResponse patientResponse = new PatientsResponse();
            try
            {
                Patients patient = new Patients
                {
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var result = await _patientsRepository.Save(patient);

                if (!result.Success)
                {
                    patientResponse.Succes = false;
                    patientResponse.Message = result.Message;
                }
                patientResponse.Succes = true;
                patientResponse.Message = "Paciente guardado exitosamente.";
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error guardando el paciente.";
                _logger.LogError(ex, "Error guardando el paciente.");
            }
            return patientResponse;
        }

        public async Task<PatientsResponse> UpdateAsync(PatientsUpdateDto dto)
        {
            var patientsResponse = new PatientsResponse();
            try
            {
                var resultGetById = await _patientsRepository.GetEntityBy(dto.PatientID);
                if (!resultGetById.Success)
                {
                    patientsResponse.Succes = resultGetById.Success;
                    patientsResponse.Message = resultGetById.Message;
                    return patientsResponse;
                }

                var patient = new Patients
                {
                    PatientID = dto.PatientID,
                    DateOfBirth = dto.DateOfBirth,
                    Gender = dto.Gender,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    IsActive = dto.IsActive,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await _patientsRepository.Update(patient);

                patientsResponse.Succes = result.Success;
                patientsResponse.Message = result.Message;
                patientsResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientsResponse.Succes = false;
                patientsResponse.Message = "Error actualizando el paciente.";
                _logger.LogError(patientsResponse.Message, ex.ToString());
            }
            return patientsResponse;
        }
    }
}
