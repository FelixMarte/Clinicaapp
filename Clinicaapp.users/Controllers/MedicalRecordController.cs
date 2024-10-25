using Clinicaapp.Domain.Entities.Configuration;
using Clinicaapp.Persistence.Interfaces.Configuration;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clinicaapp.users.Controllers
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

        [HttpGet("{id}")]
        public ActionResult<MedicalRecor> GetById(int id)
        {
            var record = _medicalRecordRepository.GetEntityBy(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
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

            // Actualiza los campos
            existingRecord.PatientID = updatedRecord.PatientID;
            existingRecord.DoctorID = updatedRecord.DoctorID;
            existingRecord.Diagnosis = updatedRecord.Diagnosis;
            existingRecord.Treatment = updatedRecord.Treatment;
            existingRecord.DateOfVisit = updatedRecord.DateOfVisit;
            existingRecord.UpdatedAt = DateTime.Now;


            await _medicalRecordRepository.Update(existingRecord);

            return NoContent();
        }



        [HttpDelete("{id}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int PatientId)
        {
            var result = await _medicalRecordRepository.GetEntityBy(PatientId);
            if (result == null || !result.Success)
            {
                return NotFound();
            }

            var record = result.Entity;
            _ = _medicalRecordRepository.Remove(record);

            return NoContent();
        }


    }
}
