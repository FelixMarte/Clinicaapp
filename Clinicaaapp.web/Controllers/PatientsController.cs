using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.web.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _patientsService.GetAll();
            if (result.Succes)
            {
                return View(result.Data); 
            }

            ViewBag.Message = "No se pudieron cargar los pacientes.";
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var patientsResponse = await _patientsService.GetById(id);
            if (!patientsResponse.Succes || patientsResponse.Data == null || !patientsResponse.Data.Any())
            {
                return NotFound("Paciente no encontrado.");
            }

            var patient = patientsResponse.Data.First();

            return View(patient);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientsSaveDto patient)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientsService.SaveAsync(patient);
                if (result.Succes)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = $"Error al guardar: {result.Message}";
            }
            return View(patient);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patientsResponse = await _patientsService.GetById(id);
            if (!patientsResponse.Succes || patientsResponse.Data == null || !patientsResponse.Data.Any())
            {
                return NotFound("Paciente no encontrado.");
            }


            var patient = patientsResponse.Data.First();

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientsUpdateDto patient)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Los datos proporcionados no son válidos.";
                return View(patient);
            }

            try
            {
                var result = await _patientsService.UpdateAsync(patient);
                if (result.Succes)
                {
                    TempData["SuccessMessage"] = "Paciente actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = $"Error al actualizar: {result.Message}";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ocurrió un error inesperado: {ex.Message}";
            }

            return View(patient);
        }
    }
}


