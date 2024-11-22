using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Clinicaapp.Application.Services;
using Clinicaapp.Domain.Entities.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Clinicaapp.web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientsService _patientService;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientsService patientService, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _patientService.GetAll();

            if (result.Succes)
            {
                List<PatientsModel> patientModel = (List<PatientsModel>)result.Data;
                return View(patientModel);
            }

            ViewBag.Message = "No se pudieron obtener los pacientes.";
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _patientService.GetById(id);

            if (result.Succes)
            {
                PatientsModel patientModel = (PatientsModel)result.Data;
                return View(patientModel);
            }

            ViewBag.Message = "El paciente no fue encontrado.";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientsSaveDto patientSave)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientSave.CreatedAt = DateTime.UtcNow;

                    var result = await _patientService.SaveAsync(patientSave);

                    if (result.Succes)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Message = $"Error al guardar: {result.Message}";
                        return View(patientSave);
                    }
                }
                else
                {
                    ViewBag.Message = "Los datos enviados no son válidos. Por favor, corrige los errores.";
                    return View(patientSave);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar guardar un paciente.");
                ViewBag.Message = "Ocurrió un error inesperado al procesar la solicitud.";
                return View(patientSave);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _patientService.GetById(id);

            if (result.Succes)
            {
                PatientsModel patientModel = (PatientsModel)result.Data;
                return View(patientModel);
            }

            ViewBag.Message = "El paciente no fue encontrado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientsUpdateDto patientUpdate)
        {
            try
            {
                patientUpdate.UpdatedAt = DateTime.UtcNow; // Usar UTC para consistencia en fechas
                var result = await _patientService.UpdateAsync(patientUpdate);

                if (result.Succes)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = $"Error al actualizar: {result.Message}";
                    return View(patientUpdate);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar actualizar un paciente.");
                ViewBag.Message = "Ocurrió un error inesperado al procesar la solicitud.";
                return View(patientUpdate);
            }
        }
    }
}