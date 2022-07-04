namespace WebApp.App.Controllers
{
    public class InvoiceHeadersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceHeadersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InvoiceHeaderViewModel = new InvoiceHeaderViewModel();
        }

        [BindProperty]
        public InvoiceHeaderViewModel InvoiceHeaderViewModel { get; set; }

        public IActionResult Index()
        {
            var invoiceHeaders = _unitOfWork.InvoiceHeaderRepository.GetAll().ToList();
            if (invoiceHeaders == null)
                return NotFound("no data found");

            return View(invoiceHeaders);
        }

        public IActionResult Details(int id)
        {
            var invoiceHeader = _unitOfWork.InvoiceHeaderRepository.FindObject(x => x.Id == id);
            if (invoiceHeader == null)
                return NotFound("no data found");

            InvoiceHeaderViewModel = _mapper.Map<InvoiceHeaderViewModel>(invoiceHeader);

            InvoiceHeaderViewModel.TotalPrice = 0;
            foreach (var item in InvoiceHeaderViewModel.InvoiceDetails)
            {
                InvoiceHeaderViewModel.TotalPrice += item.ItemCount * item.ItemPrice;
            }

            return View(InvoiceHeaderViewModel);
        }



        public IActionResult Add()
        {
            LoadBranchItems();

            return View("Add_Update", InvoiceHeaderViewModel);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdd()
        {
            if (!ModelState.IsValid)
            {
                LoadBranchItems();

                return View("Add_Update", InvoiceHeaderViewModel);
            }

            if (InvoiceHeaderViewModel.BranchId != 0 && InvoiceHeaderViewModel.CashierId != 0 &&
                !_unitOfWork.CashierRepository.GetAll(b => b.BranchId == InvoiceHeaderViewModel.BranchId)
                .Any(c => c.Id == InvoiceHeaderViewModel.CashierId))
            {
                LoadBranchItems();

                ModelState.AddModelError("", $"{_unitOfWork.CashierRepository.FindObject(c => c.Id == InvoiceHeaderViewModel.CashierId).CashierName} " +
                    $"does not exist in {_unitOfWork.BranchRepository.FindObject(c => c.Id == InvoiceHeaderViewModel.BranchId).BranchName}!");

                return View("Add_Update", InvoiceHeaderViewModel);
            }

            var invoiceHeader = _mapper.Map<InvoiceHeader>(InvoiceHeaderViewModel);
            _unitOfWork.InvoiceHeaderRepository.Add(invoiceHeader);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var invoiceHeader = _unitOfWork.InvoiceHeaderRepository.FindObject(x => x.Id == id);
            if (invoiceHeader == null)
                return NotFound("no data found");

            InvoiceHeaderViewModel = _mapper.Map<InvoiceHeaderViewModel>(invoiceHeader);
            LoadBranchItems();

            return View("Add_Update", InvoiceHeaderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (!ModelState.IsValid)
            {
                LoadBranchItems();

                return View("Add_Update", InvoiceHeaderViewModel);
            }

            if (InvoiceHeaderViewModel.BranchId != 0 && InvoiceHeaderViewModel.CashierId != 0 &&
                !_unitOfWork.CashierRepository.GetAll(b => b.BranchId == InvoiceHeaderViewModel.BranchId)
                .Any(c => c.Id == InvoiceHeaderViewModel.CashierId))
            {
                LoadBranchItems();

                ModelState.AddModelError("", $"{_unitOfWork.CashierRepository.FindObject(c => c.Id == InvoiceHeaderViewModel.CashierId).CashierName} " +
                    $"does not exist in {_unitOfWork.BranchRepository.FindObject(c => c.Id == InvoiceHeaderViewModel.BranchId).BranchName}!");

                return View("Add_Update", InvoiceHeaderViewModel);
            }

            var invoiceHeader = _mapper.Map<InvoiceHeader>(InvoiceHeaderViewModel);
            _unitOfWork.InvoiceHeaderRepository.Update(invoiceHeader);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var invoiceHeader = _unitOfWork.InvoiceHeaderRepository.FindObject(x => x.Id == id);
                if (invoiceHeader == null)
                    return NotFound("no data found");

                _unitOfWork.InvoiceHeaderRepository.Remove(invoiceHeader);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }

        public IActionResult LoadSpecificCashierSelectListItems(int branchId)
        {
            var listOfCashiers = _unitOfWork.CashierRepository.GetAll(cashier => cashier.BranchId == branchId)
                .Select(cashier => new
                {
                    cashier.CashierName,
                    CashierId = cashier.Id.ToString(),
                }).ToList();

            return Ok(listOfCashiers);
        }

        public void LoadBranchItems()
        {
            InvoiceHeaderViewModel.BranchListItems = _unitOfWork.BranchRepository.GetAll()
                .Select(branch => new SelectListItem
                {
                    Text = branch.BranchName,
                    Value = branch.Id.ToString(),
                    Selected = InvoiceHeaderViewModel != null && InvoiceHeaderViewModel.BranchId == branch.Id,
                }).ToList();
        }

        //public void LoadCashierItems(int branchId)
        //{
        //    InvoiceHeaderViewModel.CashierListItems = _unitOfWork.CashierRepository.GetAll(cashier => cashier.BranchId == branchId)
        //        .Select(cashier => new SelectListItem
        //        {
        //            Text = cashier.CashierName,
        //            Value = cashier.Id.ToString(),
        //            Selected = InvoiceHeaderViewModel.InvoiceHeader != null && InvoiceHeaderViewModel.InvoiceHeader.CashierId == cashier.Id
        //        }).ToList();
        //}


    }
}
