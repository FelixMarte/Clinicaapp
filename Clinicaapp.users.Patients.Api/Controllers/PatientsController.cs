using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinicaapp.users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _Patientsrepository;

        public PatientsController(IPatientsRepository Patientsrepository)
        {
            _Patientsrepository = Patientsrepository;
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Patientsrepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetPatientById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _Patientsrepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] Patient patient)
        {
            var result = await _Patientsrepository.Save(patient);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] Patient patient)
        {
            var result = await _Patientsrepository.Update(patient);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _Patientsrepository.GetEntityBy(id);
            if (patient.Data == null)
            {
                return NotFound(new { Success = false, Message = "Patient not found." });
            }

            var result = await _Patientsrepository.Remove(patient.Data);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
