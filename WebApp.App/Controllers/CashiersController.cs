namespace WebApp.App.Controllers
{
    public class CashiersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashiersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CashierViewModel = new CashierViewModel();
        }

        [BindProperty]
        public CashierViewModel CashierViewModel { get; set; }

        public IActionResult Index()
        {
            CashierViewModel.Cashiers = _unitOfWork.CashierRepository.GetAll().ToList();

            if (CashierViewModel.Cashiers == null)
                return NotFound("no data found");

            return View(CashierViewModel);
        }

        public IActionResult Add()
        {
            LoadSelectListItems();

            return View(CashierViewModel);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdd()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectListItems();

                return View(CashierViewModel);
            }

            _unitOfWork.CashierRepository.Add(CashierViewModel.Cashier);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            CashierViewModel.Cashier = _unitOfWork.CashierRepository.FindObject(x => x.Id == id);

            LoadSelectListItems();

            if (CashierViewModel.Cashier == null)
                return NotFound("no data found");

            return View(CashierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectListItems();

                return View(CashierViewModel);
            }

            _unitOfWork.CashierRepository.Update(CashierViewModel.Cashier);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                CashierViewModel.Cashier = _unitOfWork.CashierRepository.FindObject(x => x.Id == id);
                if (CashierViewModel.Cashier == null)
                    return NotFound("no data found");

                _unitOfWork.CashierRepository.Remove(CashierViewModel.Cashier);
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
            CashierViewModel.ListItems = _unitOfWork.BranchRepository.GetAll()
                .Select(branch => new SelectListItem
                {
                    Text = branch.BranchName,
                    Value = branch.Id.ToString(),
                    Selected = CashierViewModel.Cashier != null && CashierViewModel.Cashier.BranchId == branch.Id
                }).ToList();
        }
    }
}
