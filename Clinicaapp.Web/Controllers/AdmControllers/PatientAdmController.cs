using Clinicaapp.Application.Dtos.Configuracion.Patient;
using Clinicaapp.Web.Models;
using Clinicaapp.Web.Models.Patient;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.Web.Controllers.AdmControllers
{
    public class PatientAdmController : Controller
    {
        private readonly ApiService _apiService;

        public PatientAdmController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var patients = await _apiService.MakeApiRequest<PatientGetAllResultModel>("Patients/Getpatients", HttpMethod.Get);
                return View(patients?.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }

        
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var patient = await _apiService.MakeApiRequest<PatientGetByIdModel>($"Patients/GetPatientById?id={id}", HttpMethod.Get);
                if (patient?.data == null)
                {
                    ViewBag.Message = "Patient no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                return View(patient.data);
                }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
           
        }

        // GET: PatientAdmController/Create
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
                var result = await _apiService.MakeApiRequest<BaseResultModel>("Patients/SavePatient", HttpMethod.Post, patientSaveDto);

                if (result != null && result.succes)
                    return RedirectToAction(nameof(Index));

                ViewBag.Message = result?.message ?? "Se produjo un error.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var patient = await _apiService.MakeApiRequest<PatientGetByIdModel>($"Patients/GetPatientById?id={id}", HttpMethod.Get);
                if (patient?.data == null)
                {
                    ViewBag.Message = "Patient no encontrado";
                    return RedirectToAction(nameof(Index));
                }
                return View(patient.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientUpdateDto patientUpdateDto)
        {
            try
            {
                var result = await _apiService.MakeApiRequest<BaseResultModel>($"Patients/editpatient?id={id}", HttpMethod.Put, patientUpdateDto);

                if (result != null && result.succes)
                    return RedirectToAction(nameof(Index));

                ViewBag.Message = result?.message ?? "Se produjo un error.";
                return View(patientUpdateDto);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var patient = await _apiService.MakeApiRequest<PatientGetByIdModel>($"Patients/GetPatientById?id={id}", HttpMethod.Get);
                if (patient?.data == null)
                {
                    ViewBag.Message = "Patient no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                return View(patient.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = await _apiService.MakeApiRequest<BaseResultModel>($"Patients/deletepatient?id={id}", HttpMethod.Delete);

                if (result?.succes == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = result?.message ?? "Se produjo un error al eliminar el paciente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }
    }
}
