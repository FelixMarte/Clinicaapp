using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Clinicaapp.Persistence.Repositories.Configuracion;
using Clinicaapp.users.apii.ProvidEntities;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.users.apii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientsService.GetAll();
            return result.Succes ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientsService.GetById(id);
            return result.Succes ? Ok(result) : NotFound(result);
        }

        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] PatientsSaveDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientsService.SaveAsync(patientDto);
            return result.Succes ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] PatientsUpdateDto patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientsService.UpdateAsync(patientDto);
            return result.Succes ? Ok(result) : BadRequest(result);
        }

    }
}


