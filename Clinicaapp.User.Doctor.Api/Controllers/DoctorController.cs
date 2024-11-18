using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuracion.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;


        public DoctorController(IDoctorService doctorService) {
            _doctorService = doctorService;
        }


        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorService.GetAll();
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("GetDoctorById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorService.GetById(id);
            if (!result.Succes)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        [HttpPost("SaveDoctor")]
        public async Task<IActionResult>  Post([FromBody] DoctorSaveDto doctorSaveDto)
        {
            var result = await _doctorService.SaveAsync(doctorSaveDto);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPut("UpdateDoctor")]
        public async Task<IActionResult> Put(int id, [FromBody] DoctorUpdateDto doctorUpdateDto)
        {
            if (id != doctorUpdateDto.DoctorID) 
            {
                return BadRequest("El ID no coincide con el doctor proporcionado.");
            }

            var result = await _doctorService.UpdateAsync(doctorUpdateDto);
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }



        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult>  Delete(int id)
        {
            var result = await _doctorService.DeleteAsync(id); 
            if (!result.Succes)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
