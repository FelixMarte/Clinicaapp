using Clinicaapp.Application.Dtos.Configuracion.Doctor;
using Clinicaapp.Web.Models;
using Clinicaapp.Web.Models.Doctor;
using Clinicaapp.Web.Models.Patient;
using Microsoft.AspNetCore.Mvc;



namespace Clinicaapp.Web.Controllers.AdmControllers
{
    public class DoctorAdmController : Controller
    {
        private readonly ApiService _apiService;

        public DoctorAdmController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try {
                var doctorGetAllResultModel = await _apiService.MakeApiRequest<DoctorGetAllResultModel>("Doctor/GetDoctors", HttpMethod.Get);
                return View(doctorGetAllResultModel?.data);
            }
            catch(Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }

        }




        public async Task<IActionResult> Details(int id)
        {
            try 
            {
                var doctorGetByIdModel = await _apiService.MakeApiRequest<DoctorGetByIdModel>($"Doctor/GetDoctorById?id={id}", HttpMethod.Get);
                return View(doctorGetByIdModel?.data);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }

            
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorSaveDto doctorSaveDto)
        {
            try
            {
                var model = await _apiService.MakeApiRequest<BaseResultModel>("Doctor/SaveDoctor", HttpMethod.Post, doctorSaveDto);
                if (model?.succes == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = model?.message ?? "Se produjo un error al guardar el médico.";
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
                var doctorGetByIdModel = await _apiService.MakeApiRequest<DoctorGetByIdModel>($"Doctor/GetDoctorById?id={id}", HttpMethod.Get);
                if (doctorGetByIdModel?.data != null)
                {
                    return View(doctorGetByIdModel.data);
                }
                ViewBag.Message = "Doctor no encontrado";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"ha ocurrido un error: {ex.Message}";
                return View();
            }
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorUpdateDto doctorUpdateDto)
        {
            try
            {
                var model = await _apiService.MakeApiRequest<BaseResultModel>($"Doctor/UpdateDoctor?id={id}", HttpMethod.Put, doctorUpdateDto);
                if (model?.succes == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = model?.message ?? "Se produjo un error al actualizar el doctor.";
                return View();

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
                var doctorGetByIdModel = await _apiService.MakeApiRequest<DoctorGetByIdModel>($"Doctor/GetDoctorById?id={id}", HttpMethod.Get);
                if (doctorGetByIdModel?.data != null)
                {
                    return View(doctorGetByIdModel.data);
                }
                ViewBag.Message = "doctor no encontrado";
                return RedirectToAction(nameof(Index));
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
                var model = await _apiService.MakeApiRequest<BaseResultModel>($"Doctor/DeleteDoctor?id={id}", HttpMethod.Delete);
                if (model?.succes == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = model?.message ?? "Se produjo un error al eliminar el doctor.";
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
