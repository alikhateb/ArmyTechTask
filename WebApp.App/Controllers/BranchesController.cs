using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DataAccess.UnitOfWork;
using WebApp.Models.ViewModels;

namespace WebApp.App.Controllers
{
    public class BranchesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            BranchViewModel = new BranchViewModel();
        }

        [BindProperty]
        public BranchViewModel BranchViewModel { get; set; }

        public IActionResult Index()
        {
            BranchViewModel.Branches = _unitOfWork.BranchRepository.GetAll();

            if (BranchViewModel.Branches is null)
                return NotFound("no data found");

            return View(BranchViewModel);
        }

        public IActionResult Add()
        {
            LoadSelectListItems();

            return View(BranchViewModel);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdd()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectListItems();

                return View(BranchViewModel);
            }

            _unitOfWork.BranchRepository.Add(BranchViewModel.Branch);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            BranchViewModel.Branch = _unitOfWork.BranchRepository.FindObject(x => x.Id == id);

            LoadSelectListItems();

            if (BranchViewModel.Branch is null)
                return NotFound("no data found");

            return View(BranchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectListItems();

                return View(BranchViewModel);
            }

            _unitOfWork.BranchRepository.Update(BranchViewModel.Branch);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                BranchViewModel.Branch = _unitOfWork.BranchRepository.FindObject(x => x.Id == id);
                if (BranchViewModel.Branch is null)
                    return NotFound("no data found");

                _unitOfWork.BranchRepository.Remove(BranchViewModel.Branch);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }

        public void LoadSelectListItems()
        {
            BranchViewModel.ListItems = _unitOfWork.CityRepository.GetAll().Select(city => new SelectListItem
            {
                Text = city.CityName,
                Value = city.Id.ToString(),
                Selected = BranchViewModel.Branch is not null && BranchViewModel.Branch.CityId == city.Id
            }).ToList();
        }
    }
}
