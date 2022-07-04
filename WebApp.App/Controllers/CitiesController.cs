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
            var cities = _unitOfWork.CityService.GetAll().ToList();
            if (cities == null)
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

            _unitOfWork.CityService.Add(city);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            City city = _unitOfWork.CityService.FindObject(x => x.Id == id);
            if (city == null)
                return NotFound("no data found");

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(City city)
        {
            if (!ModelState.IsValid)
                return View(city);

            _unitOfWork.CityService.Update(city);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                City city = _unitOfWork.CityService.FindObject(x => x.Id == id);
                if (city == null)
                    return NotFound("no data found");

                _unitOfWork.CityService.Remove(city);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }
    }
}
