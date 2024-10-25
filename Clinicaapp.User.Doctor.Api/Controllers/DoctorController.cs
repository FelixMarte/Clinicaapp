using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuracion;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public DoctorController(IDoctorsRepository doctorsRepository) {
            _doctorsRepository = doctorsRepository;
        }


        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorsRepository.GetAll();
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorsRepository.GetEntityBy(id);
            if (!result.Succes)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        [HttpPost("SaveDoctor")]
        public async Task<IActionResult>  Post([FromBody] Doctors doctors)
        {
            var result = await _doctorsRepository.Save(doctors);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Doctors doctor)
        {
            if (id != doctor.DoctorID) 
            {
                return BadRequest("El ID no coincide con el doctor proporcionado.");
            }

            var result = await _doctorsRepository.Update(doctor);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult>  Delete(int id)
        {
            var result = await _doctorsRepository.Delete(id); 
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
