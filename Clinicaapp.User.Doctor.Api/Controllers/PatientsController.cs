using Clinicaapp.Application.contracts;
using Clinicaapp.Application.Dtos.Configuracion.Patient;
using Microsoft.AspNetCore.Mvc;
namespace Clinicaapp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService) {
            _patientService = patientService;
        }
        [HttpGet("Getpatients")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientService.GetAll();
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

  
        [HttpGet("GetPatientById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientService.GetById(id);
            if (!result.Succes)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

  
        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] PatientSaveDto patientSaveDto)
        {
            var result = await _patientService.SaveAsync(patientSaveDto);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPut("editpatient")]
        public async Task<IActionResult> Put(int id, [FromBody] PatientUpdateDto patientUpdateDto)
        {
            if(id != patientUpdateDto.PatientID)
            {
                return BadRequest("El ID no coincide con el Paciente proporcionado.");
            }

            var result = await _patientService.UpdateAsync(patientUpdateDto);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("deletepatient")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientService.DeleteAsync(id);
            if (!result.Succes)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }
    }
}
