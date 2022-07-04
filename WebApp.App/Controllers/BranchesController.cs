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
            BranchViewModel.Branches = _unitOfWork.BranchSrvice.GetAll().ToList();

            if (BranchViewModel.Branches == null)
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

            _unitOfWork.BranchSrvice.Add(BranchViewModel.Branch);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            BranchViewModel.Branch = _unitOfWork.BranchSrvice.FindObject(x => x.Id == id);

            LoadSelectListItems();

            if (BranchViewModel.Branch == null)
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

            _unitOfWork.BranchSrvice.Update(BranchViewModel.Branch);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                BranchViewModel.Branch = _unitOfWork.BranchSrvice.FindObject(x => x.Id == id);
                if (BranchViewModel.Branch == null)
                    return NotFound("no data found");

                _unitOfWork.BranchSrvice.Remove(BranchViewModel.Branch);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }

        public void LoadSelectListItems()
        {
            BranchViewModel.ListItems = _unitOfWork.CityService.GetAll()
                .Select(city => new SelectListItem
                {
                    Text = city.CityName,
                    Value = city.Id.ToString(),
                    Selected = BranchViewModel.Branch != null && BranchViewModel.Branch.CityId == city.Id
                }).ToList();
        }
    }
}
