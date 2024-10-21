using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Clinicaapp.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace Clinicaapp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        public PatientsController(IPatientsRepository patientsRepository) {
            _patientsRepository = patientsRepository;
        }
        [HttpGet("Getpasientes")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientsRepository.GetAll();
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

  
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientsRepository.GetEntityBy(id);
            if (!result.Succes)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

  
        [HttpPost("SavePasiente")]
        public async Task<IActionResult> Post([FromBody] Patients patients)
        {
            var result = await _patientsRepository.Save(patients);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Patients patients)
        {
            if(id != patients.PatientID)
            {
                return BadRequest("El ID no coincide con el Paciente proporcionado.");
            }

            var result = await _patientsRepository.Update(patients);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientsRepository.Delete(id);
            if (!result.Succes)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }
    }
}
