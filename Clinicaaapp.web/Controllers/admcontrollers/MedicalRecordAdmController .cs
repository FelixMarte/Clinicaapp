using Clinicaapp.Web.Models.MedicalRecords;
using Microsoft.AspNetCore.Mvc;
using Clinicaaapp.web.Models;
using Clinicaapp.Application.Dtos.Configuration.MedicalRecord;

namespace Clinicaapp.Web.Controllers.AdmControllers
{
    public class MedicalRecordAdmController : Controller
    {
        private readonly ApiService _apiService;

        // Constructor donde se inyecta ApiService
        public MedicalRecordAdmController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: Listado de registros médicos
        public async Task<IActionResult> Index()
        {
            try
            {
                // Llamada a la API para obtener todos los registros médicos
                var medicalRecords = await _apiService.MakeApiRequest<MedicalRecordGetAllResultModel>("MedicalRecords/GetMedicalRecords", HttpMethod.Get);
                return View(medicalRecords?.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // GET: Detalles de un registro médico
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                // Llamada a la API para obtener los detalles de un registro médico específico
                var medicalRecord = await _apiService.MakeApiRequest<MedicalRecordGetByIdModel>($"MedicalRecords/GetMedicalRecordById?id={id}", HttpMethod.Get);
                if (medicalRecord?.Data == null)
                {
                    ViewBag.Message = "Registro médico no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                return View(medicalRecord.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // GET: Crear un registro médico
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicalRecordSaveDto medicalRecordSaveDto)
        {
            try
            {
                // Llamada a la API para guardar un nuevo registro médico
                var result = await _apiService.MakeApiRequest<BaseApiResponseModel>("MedicalRecords/SaveMedicalRecord", HttpMethod.Post, medicalRecordSaveDto);

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

        // GET: Editar un registro médico
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Llamada a la API para obtener los detalles del registro médico a editar
                var medicalRecord = await _apiService.MakeApiRequest<MedicalRecordGetByIdModel>($"MedicalRecords/GetMedicalRecordById?id={id}", HttpMethod.Get);
                if (medicalRecord?.Data == null)
                {
                    ViewBag.Message = "Registro médico no encontrado.";
                    return RedirectToAction(nameof(Index));
                }
                return View(medicalRecord.Data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // POST: Editar un registro médico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MedicalRecordUpdateDto medicalRecordUpdateDto)
        {
            try
            {
                // Llamada a la API para actualizar el registro médico
                var result = await _apiService.MakeApiRequest<BaseApiResponseModel>($"MedicalRecords/EditMedicalRecord?id={id}", HttpMethod.Put, medicalRecordUpdateDto);

                if (result != null && result.isSuccess)
                    return RedirectToAction(nameof(Index));

                ViewBag.Message = result?.message ?? "Se produjo un error.";
                return View(medicalRecordUpdateDto);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Ha ocurrido un error: {ex.Message}";
                return View();
            }
        }

        // GET: Eliminar un registro médico
    
        }
    }


