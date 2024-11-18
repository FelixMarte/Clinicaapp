using Clinicaapp.Application.Contracts;
using Clinicaapp.Application.Dtos.Configuracion.Doctor;
using Clinicaapp.Persistence.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinicaapp.Web.Controllers.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _doctorService.GetAll();

            var doctorsModel = result.Data as List<DoctorsModel>;
            if (result.Succes)
            {
                return View(doctorsModel);
            }
            ViewBag.ErrorMessage = result.Message;
            return View(new List<DoctorsModel>());
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await _doctorService.GetById(id);

            if (result.Succes)
            {
                var doctorModel = result.Data as DoctorsModel;

                if (doctorModel != null)
                {
                    return View(doctorModel);
                }
            }


            ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el doctor.";
            return View(result);
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
                doctorSaveDto.CreatedAt = DateTime.Now;
                doctorSaveDto.UpdatedAt = DateTime.Now;
                var result = await _doctorService.SaveAsync(doctorSaveDto);
                if (!result.Succes)
                {
                    ViewBag.ErrorMessage = result.Message ?? "No se pudo Guardar el doctor.";
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

            var result = await _doctorService.GetById(id);

            if (result.Succes)
            {
                var doctorModel = result.Data as DoctorsModel;

                if (doctorModel != null)
                {
                    return View(doctorModel);
                }
            }


            ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el doctor.";
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorUpdateDto doctorUpdateDto)
        {

            try
            {
                doctorUpdateDto.UpdatedAt = DateTime.Now;
                var result = await _doctorService.UpdateAsync(doctorUpdateDto);
                if (!result.Succes)
                {
                    ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el doctor.";
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
            var result = await _doctorService.GetById(id);

            if (result.Succes)
            {
                var doctorModel = result.Data as DoctorsModel;

                if (doctorModel != null)
                {
                    return View(doctorModel);
                }
            }

            ViewBag.ErrorMessage = result.Message ?? "No se pudo obtener el doctor para eliminar.";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var result = await _doctorService.DeleteAsync(id);
                if (!result.Succes)
                {
                    ViewBag.ErrorMessage = result.Message ?? "No se pudo eliminar el doctor.";
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
