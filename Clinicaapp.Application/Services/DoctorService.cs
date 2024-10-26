using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuracion.Doctor;
using Clinicaapp.Application.Reponses.Configuracion.Doctors;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly ILogger<DoctorService> _logger;

        public DoctorService(IDoctorsRepository doctorsRepository, ILogger<DoctorService> logger)
        {
           _doctorsRepository = doctorsRepository;
            _logger = logger;
        }

        public async Task<DoctorResponse> GetAll()
        {
           DoctorResponse doctorResponse = new DoctorResponse();
            try
            {
                var result = await _doctorsRepository.GetAll();
                if (!result.Succes)
                {
                    doctorResponse.Message = result.Message;
                    doctorResponse.Succes = result.Succes;
                    return doctorResponse;

                }
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error Obteniendo los Doctores.";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }
        public async Task<DoctorResponse> GetById(int id)
        {
            DoctorResponse doctorResponse = new DoctorResponse();
            try
            {
                var result = await _doctorsRepository.GetEntityBy(id);
                if (!result.Succes)
                {
                    doctorResponse.Message = result.Message;
                    doctorResponse.Succes = result.Succes;
                    return doctorResponse;

                }
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error Obteniendo el Doctores.";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }
        public async Task<DoctorResponse> SaveAsync(DoctorSaveDto dto)
        {
            DoctorResponse doctorResponse = new DoctorResponse();

            try
            {
                Doctors doctor = new Doctors();

                doctor.YearsOfExperience = dto.YearsOfExperience;
                doctor.Education = dto.Education;
                doctor.Bio = dto.Bio;
                doctor.ConsultationFee = dto.ConsultationFee;
                doctor.ClinicAddress = dto.ClinicAddress;
                doctor.LicenseNumber = dto.LicenseNumber;
                doctor.LicenseExpirationDate = dto.LicenseExpirationDate;
                doctor.IsActive = dto.IsActive;
                doctor.PhoneNumber = dto.PhoneNumber;

                
                var result = await _doctorsRepository.Save(doctor);

                if (!result.Succes)
                {
                    doctorResponse.Succes = false;
                    doctorResponse.Message = result.Message;
                }
                doctorResponse.Succes = true;
                doctorResponse.Message = "Doctor guardado exitosamente.";
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error guardando el doctor.";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }

            return doctorResponse;
        }
        public async Task<DoctorResponse> UpdateAsync(DoctorUpdateDto dto)
        {
            DoctorResponse doctorResponse = new DoctorResponse();
            try
            {
                var resultGetById = await _doctorsRepository.GetEntityBy(dto.DoctorID);

                if (!resultGetById.Succes)
                {
                    doctorResponse.Succes = resultGetById.Succes;
                    doctorResponse.Message = resultGetById.Message;
                    return doctorResponse;
                }
              
                Doctors doctor = new Doctors
                {
                    DoctorID = dto.DoctorID,
                    YearsOfExperience = dto.YearsOfExperience,
                    Education = dto.Education,
                    Bio = dto.Bio,
                    ConsultationFee = dto.ConsultationFee,
                    ClinicAddress = dto.ClinicAddress,
                    LicenseNumber = dto.LicenseNumber,
                    LicenseExpirationDate = dto.LicenseExpirationDate,
                    IsActive = dto.IsActive,
                    PhoneNumber = dto.PhoneNumber
                };

         
                var result = await _doctorsRepository.Update(doctor);

                doctorResponse.Succes = result.Succes;
                doctorResponse.Message = result.Message;
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error actualizando el Doctor.";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }
        
    }
}

