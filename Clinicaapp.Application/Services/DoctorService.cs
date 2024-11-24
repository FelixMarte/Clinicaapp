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
                    _logger.LogWarning("Error en el repositorio al obtener doctores: {Message}", result.Message);
                    return doctorResponse; 
                }
                doctorResponse.Data = result.Data;
                doctorResponse.Succes = true;
                doctorResponse.Message = "Doctores obtenidos exitosamente.";
                _logger.LogWarning("Error en el repositorio al obtener doctores: {Message}", result.Message);
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error obteniendo los doctores.";
                _logger.LogError(ex, doctorResponse.Message);
            }
            return doctorResponse;
        }
        public async Task<DoctorResponse> GetById(int id)
        {
            var doctorResponse = new DoctorResponse();
            try
            {
                var result = await _doctorsRepository.GetEntityBy(id);
                if (!result.Succes)
                {
                    doctorResponse.Message = result.Message;
                    doctorResponse.Succes = false;
                    return doctorResponse;
                }

                doctorResponse.Data = result.Data;
                doctorResponse.Succes = true;
                doctorResponse.Message = "Doctor obtenido exitosamente.";
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error obteniendo el doctor.";
                _logger.LogError(ex, doctorResponse.Message);
            }
            return doctorResponse;
        }
        public async Task<DoctorResponse> SaveAsync(DoctorSaveDto dto)
        {
            var doctorResponse = new DoctorResponse();
            try
            {
                var doctor = new Doctors
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
                    PhoneNumber = dto.PhoneNumber,
                    CreatedAt = dto.CreatedAt,


                };
                


                var result = await _doctorsRepository.Save(doctor);
                doctorResponse.Succes = result.Succes;
                doctorResponse.Message = result.Succes ? "Doctor guardado exitosamente." : result.Message;
                doctorResponse.Data = result.Data;
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error guardando el doctor.";
                _logger.LogError(ex, doctorResponse.Message);
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
                    PhoneNumber = dto.PhoneNumber,
                    IsActive = dto.IsActive,
                    UpdatedAt = dto.UpdatedAt,
                };

         
                var result = await _doctorsRepository.Update(doctor);

                doctorResponse.Succes = result.Succes;
                doctorResponse.Message = result.Succes ? "Doctor actualizado exitosamente." : result.Message;
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
        public async Task<DoctorResponse> DeleteAsync(int id)
        {
            DoctorResponse doctorResponse = new DoctorResponse();
            try
            {
                var resultGetById = await _doctorsRepository.GetEntityBy(id);
                if (!resultGetById.Succes)
                {
                    doctorResponse.Succes = false;
                    doctorResponse.Message = "Doctor no encontrado para eliminar.";
                    return doctorResponse;
                }

                var resultDelete = await _doctorsRepository.Delete(id);
                doctorResponse.Succes = resultDelete.Succes;
                doctorResponse.Message = resultDelete.Succes ? "Doctor eliminado exitosamente." : "Error eliminando el doctor.";
            }
            catch (Exception ex)
            {
                doctorResponse.Succes = false;
                doctorResponse.Message = "Error eliminando el Doctor.";
                _logger.LogError(doctorResponse.Message, ex.ToString());
            }
            return doctorResponse;
        }




    }
}

