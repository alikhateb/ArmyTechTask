using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccess.UnitOfWork;
using WebApp.Models;

namespace WebApp.App.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CitiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var cities = _unitOfWork.CityRepository.GetAll();
            if (cities is null)
                return NotFound("no data found");

            return View(cities);
        }

        public IActionResult Add()
        {
            City city = new();
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(City city)
        {
            if (!ModelState.IsValid)
                return View(city);

            _unitOfWork.CityRepository.Add(city);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            City city = _unitOfWork.CityRepository.FindObject(x => x.Id == id);
            if (city is null)
                return NotFound("no data found");

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(City city)
        {
            if (!ModelState.IsValid)
                return View(city);

            _unitOfWork.CityRepository.Update(city);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                City city = _unitOfWork.CityRepository.FindObject(x => x.Id == id);
                if (city is null)
                    return NotFound("no data found");

                _unitOfWork.CityRepository.Remove(city);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }
    }
}
