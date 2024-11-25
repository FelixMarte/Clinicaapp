using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;

using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.web.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _medicalRecordService.GetAll();
            if (result.Succes)
            {
                return View(result.Data);
            }

            ViewBag.Message = "No se pudieron cargar los registros médicos.";
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var medicalRecordResponse = await _medicalRecordService.GetById(id);
            if (!medicalRecordResponse.Succes || medicalRecordResponse.Data == null || !medicalRecordResponse.Data.Any())
            {
                return NotFound("Registro médico no encontrado.");
            }

            var medicalRecord = medicalRecordResponse.Data.First();

            return View(medicalRecord);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalRecordSaveDto medicalRecord)
        {
            if (ModelState.IsValid)
            {
                var result = await _medicalRecordService.SaveAsync(medicalRecord);
                if (result.Succes)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = $"Error al guardar: {result.Message}";
            }
            return View(medicalRecord);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicalRecordResponse = await _medicalRecordService.GetById(id);
            if (!medicalRecordResponse.Succes || medicalRecordResponse.Data == null || !medicalRecordResponse.Data.Any())
            {
                return NotFound("Registro médico no encontrado.");
            }

            var medicalRecord = medicalRecordResponse.Data.First();

            return View(medicalRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicalRecordUpdateDto medicalRecord)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Los datos proporcionados no son válidos.";
                return View(medicalRecord);
            }

            try
            {
                var result = await _medicalRecordService.UpdateAsync(medicalRecord);
                if (result.Succes)
                {
                    TempData["SuccessMessage"] = "Registro médico actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = $"Error al actualizar: {result.Message}";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ocurrió un error inesperado: {ex.Message}";
            }

            return View(medicalRecord);
        }
    }
}

