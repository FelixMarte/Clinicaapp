using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;
using Clinicaapp.Domain.Entities.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.users.apii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet("GetAllMedicalRecords")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicalRecordService.GetAll();
            if (!result.Succes)
            {
                return NotFound(new { Message = result.Message });
            }
            return Ok(result.Data);
        }

        // GET: api/MedicalRecord/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "El ID debe ser mayor que cero." }); 
            }

            var result = await _medicalRecordService.GetById(id);

            if (!result.Succes)
            {
                return NotFound(new { Message = result.Message }); 
            }

            return Ok(result.Data); // Devuelve 200 con el registro encontrado
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicalRecordSaveDto dto)
        {
            var response = await _medicalRecordService.SaveAsync(dto);
            if (!response.Succes)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        // PUT: api/MedicalRecord/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalRecordUpdateDto dto)
        {
            if (dto == null || dto.RecordID != id)
            {
                return BadRequest(new { Message = "El ID del historial médico no coincide con el proporcionado." });
            }

            var result = await _medicalRecordService.UpdateAsync(dto);

            if (!result.Succes)
            {
                return NotFound(new { Message = result.Message }); // Devuelve 404 si no se encuentra el registro
            }

            return NoContent(); // Devuelve 204 si la actualización fue exitosa
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { Message = "El ID debe ser mayor que cero." });
            }

            var result = await _medicalRecordService.DeleteAsync(id);

            if (!result.Succes)
            {
                return NotFound(new { Message = result.Message });
            }

            return NoContent(); // Devuelve 204 si el borrado fue exitoso
        }
    }
}

