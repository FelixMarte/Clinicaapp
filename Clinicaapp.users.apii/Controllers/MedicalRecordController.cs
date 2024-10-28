using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Clinicaapp.Persistence.Repositories.Configuracion;
using Clinicaapp.users.apii.ProvidEntities;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.users.apii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordController(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        [HttpGet("GetAllMedicalRecords")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _medicalRecordRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _medicalRecordRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }


        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> Create([FromBody] MedicalRecord record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.CreatedAt = DateTime.Now;

            var operationResult = await _medicalRecordRepository.Save(record);

            if (!operationResult.Success)
            {
                return BadRequest(operationResult.Message);
            }

            var createdRecord = operationResult.Data as MedicalRecord;

            return CreatedAtAction(nameof(GetById), new { id = createdRecord.RecordID }, createdRecord);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] MedicalRecord updatedRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operationResult = await _medicalRecordRepository.GetEntityBy(id);

            if (!operationResult.Success)
            {
                return NotFound(operationResult.Message);
            }

            var existingRecord = operationResult.Data as MedicalRecord;
            if (existingRecord == null)
            {
                return NotFound("El registro médico no se encontró.");
            }

            existingRecord.Diagnosis = updatedRecord.Diagnosis;
            existingRecord.Treatment = updatedRecord.Treatment;
            existingRecord.DateOfVisit = updatedRecord.DateOfVisit;
            existingRecord.UpdatedAt = DateTime.Now;


            await _medicalRecordRepository.Update(existingRecord);

            return NoContent();
        }
        
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicalRecordRepository.Remove(id);
            if (!result.Success)
            {
                return BadRequest(result);

            }
            return Ok(result);
        }
       

    }
}

