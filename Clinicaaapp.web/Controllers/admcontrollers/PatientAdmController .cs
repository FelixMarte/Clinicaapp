using Clinicaaapp.web.Models;
using Clinicaaapp.web.Models.Patients;
using Clinicaapp.Application.Dtos.Configuration.Patients;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clinicaapp.Web.Controllers.AdmControllers
{
    public class PatientAdmController : Controller
    {
        private readonly ApiService _apiService;

        // Constructor donde se inyecta ApiService
        public PatientAdmController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Listado de pacientes
        public async Task<IActionResult> Index()
        {
            try
            {
                // Llamada a la API para obtener todos los pacientes
                var patients = await _apiService.MakeApiRequest<PatientGetAllResultModel>("Patients/Getpatients", HttpMethod.Get);
                return View(patients?.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // GET: Detalles de un paciente
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // Llamada a la API para obtener los detalles de un paciente específico
                var patient = await _apiService.MakeApiRequest<PatientGetByIdModel>($"Patients/GetPatientById?id={id}", HttpMethod.Get);
                if (patient?.Data == null)
                {
                    ViewBag.Message = "Paciente no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                return View(patient.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // GET: Crear un paciente
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientsSaveDto patientSaveDto)
        {
            try
            {
                // Llamada a la API para guardar un nuevo paciente
                var result = await _apiService.MakeApiRequest<BaseApiResponseModel>("Patients/SavePatient", HttpMethod.Post, patientSaveDto);

                if (result != null && result.isSuccess)
                    return RedirectToAction(nameof(Index));

                ViewBag.Message = result?.message ?? "Se produjo un error.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // GET: Editar un paciente
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Llamada a la API para obtener los detalles del paciente a editar
                var patient = await _apiService.MakeApiRequest<PatientGetByIdModel>($"Patients/GetPatientById?id={id}", HttpMethod.Get);
                if (patient?.Data == null)
                {
                    ViewBag.Message = "Paciente no encontrado";
                    return RedirectToAction(nameof(Index));
                }
                return View(patient.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // POST: Editar un paciente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientsUpdateDto patientUpdateDto)
        {
            try
            {
                // Llamada a la API para actualizar el paciente
                var result = await _apiService.MakeApiRequest<BaseApiResponseModel>($"Patients/editpatient?id={id}", HttpMethod.Put, patientUpdateDto);

                if (result != null && result.isSuccess)
                    return RedirectToAction(nameof(Index));

                ViewBag.Message = result?.message ?? "Se produjo un error.";
                return View(patientUpdateDto);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        }
    }
