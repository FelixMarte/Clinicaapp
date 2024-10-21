using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Domain.Repositories;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Clinicaapp.Persistence.Repositories.Configuracion;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinicaapp.users.Patients.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientsRepository _Patientsrepository;

        public PatientController(IPatientsRepository Patientsrepository)
        {
            _Patientsrepository = Patientsrepository;
        }

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
        public async Task<IActionResult> Post([FromBody] Patients patients)
        {
            var result = await _Patientsrepository.Save(patients);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
