using Clinicaapp.Application.contracts;
using Clinicaapp.Application.Dtos.Configuracion.Patient;
using Clinicaapp.Persistence.Models.Configuracion;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.Web.Controllers.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _patientService.GetAll();

            var patientModels = result.Data as List<PatientsModel>;
            if (result.Succes)
            {
                return View(patientModels);
            }
            ViewBag.ErrorMessage = result.Message;
            return View(new List<PatientsModel>());
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _patientService.GetById(id);

            if (result.Succes)
            {
                var patientModel = result.Data as PatientsModel;

                if (patientModel != null)
                {
                    return View(patientModel);
                }
            }


            ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el paciente.";
            return View(result);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientSaveDto patientSaveDto)
        {
            try
            {
                patientSaveDto.CreatedAt = DateTime.Now;
                patientSaveDto.UpdatedAt = DateTime.Now;
                var result = await _patientService.SaveAsync(patientSaveDto);
                if (!result.Succes)
                {
                    ViewBag.ErrorMessage = result.Message ?? "No se pudo Guardar el paciente.";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> Edit(int id)
        {

            var result = await _patientService.GetById(id);

            if (result.Succes)
            {
                var patientModel = result.Data as PatientsModel;

                if (patientModel != null)
                {
                    return View(patientModel);
                }
            }


            ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el paciente.";
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientUpdateDto patientUpdateDto)
        {
            try
            {
                patientUpdateDto.UpdatedAt = DateTime.Now;
                var result = await _patientService.UpdateAsync(patientUpdateDto);
                if (!result.Succes)
                {
                    ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el paciente.";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _patientService.GetById(id);

            if (result.Succes)
            {
                var patientModel = result.Data as PatientsModel;

                if (patientModel != null)
                {
                    return View(patientModel);
                }
            }

            ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el paciente para eliminar.";
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = await _patientService.DeleteAsync(id);
                if (!result.Succes)
                {
                    ViewBag.ErrorMessage = result.Message ?? "No se pudo eliminar el paciente.";
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
