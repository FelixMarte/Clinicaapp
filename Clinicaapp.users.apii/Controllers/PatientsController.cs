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
        private readonly IPatientsRepository _patientsRepository;

        public PatientsController(IPatientsRepository patientsRepository)
        {
            _patientsRepository = patientsRepository;
        }
        
        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _patientsRepository.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetPatientById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientsRepository.GetEntityBy(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("SavePatient")]
        public async Task<IActionResult> Post([FromBody] Patients patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientsRepository.Save(patient);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> Put([FromBody] Patients patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientsRepository.Update(patient);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeletePatient/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientsRepository.Remove(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}


