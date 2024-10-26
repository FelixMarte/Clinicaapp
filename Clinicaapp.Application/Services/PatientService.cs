

using Clinicaapp.Application.contracts;
using Clinicaapp.Application.Dtos.Configuracion.Doctor;
using Clinicaapp.Application.Dtos.Configuracion.Patient;
using Clinicaapp.Application.Reponses.Configuracion.Doctors;
using Clinicaapp.Application.Reponses.Configuracion.Patients;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Clinicaapp.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientsRepository _patientsRepository;   
        private readonly ILogger<PatientService> _logger;

        public PatientService( IPatientsRepository patientsRepository, ILogger<PatientService> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }
        public async Task<PatientResponse> GetAll()
        {
            PatientResponse patientResponse = new PatientResponse();
            try
            {
                var result = await _patientsRepository.GetAll();
                if (!result.Succes)
                {
                    patientResponse.Message = result.Message;
                    patientResponse.Succes = result.Succes;
                    return patientResponse;

                }
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error Obteniendo los Pasientes.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }
            return patientResponse;
        }

        public async Task<PatientResponse> GetById(int id)
        {
            PatientResponse patientResponse = new PatientResponse();
            try
            {
                var result = await _patientsRepository.GetEntityBy(id);
                if (!result.Succes)
                {
                    patientResponse.Message = result.Message;
                    patientResponse.Succes = result.Succes;
                    return patientResponse;

                }
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error Obteniendo el Pasiente.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }
            return patientResponse;
        }

        public async Task<PatientResponse> SaveAsync(PatientSaveDto dto)
        {
            PatientResponse patientResponse = new PatientResponse();

            try
            {
                Patients patients = new Patients();

                patients.DateOfBirth = dto.DateOfBirth;
                patients.Address = dto.Address;
                patients.EmergencyContactName = dto.EmergencyContactName;
                patients.EmergencyContactPhone = dto.EmergencyContactPhone;
                patients.BloodType = dto.BloodType;
                patients.Allergies = dto.Allergies;
                patients.PhoneNumber = dto.PhoneNumber;


                var result = await _patientsRepository.Save(patients);

                if (!result.Succes)
                {
                    patientResponse.Succes = false;
                    patientResponse.Message = result.Message;
                }
                patientResponse.Succes = true;
                patientResponse.Message = "Doctor guardado exitosamente.";
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error guardando el doctor.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }

            return patientResponse;
        }
        public async Task<PatientResponse> UpdateAsync(PatientUpdateDto dto)
        {
            PatientResponse patientResponse = new PatientResponse();
            try
            {
                var resultGetById = await _patientsRepository.GetEntityBy(dto.PatientID);

                if (!resultGetById.Succes)
                {
                    patientResponse.Succes = resultGetById.Succes;
                    patientResponse.Message = resultGetById.Message;
                    return patientResponse;
                }

                Patients patients = new Patients
                {
                    PatientID = dto.PatientID,
                    DateOfBirth = dto.DateOfBirth,
                    Address = dto.Address,
                    EmergencyContactName = dto.EmergencyContactName,
                    EmergencyContactPhone = dto.EmergencyContactPhone,
                    BloodType = dto.BloodType,
                    Allergies = dto.Allergies,
                    PhoneNumber = dto.PhoneNumber
                };


                var result = await _patientsRepository.Update(patients);

                patientResponse.Succes = result.Succes;
                patientResponse.Message = result.Message;
                patientResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                patientResponse.Succes = false;
                patientResponse.Message = "Error actualizando el Doctor.";
                _logger.LogError(patientResponse.Message, ex.ToString());
            }
            return patientResponse;
        }

    }
}
