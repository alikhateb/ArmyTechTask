using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.DataAccess.UnitOfWork;
using WebApp.Models.ViewModels;

namespace WebApp.App.Controllers
{
    public class InvoiceHeadersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceHeadersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InvoiceHeaderViewModel = new InvoiceHeaderViewModel();
        }

        [BindProperty]
        public InvoiceHeaderViewModel InvoiceHeaderViewModel { get; set; }

        public IActionResult Index()
        {
            InvoiceHeaderViewModel.InvoiceHeaders = _unitOfWork.InvoiceHeaderRepository.GetAll();
            if (InvoiceHeaderViewModel.InvoiceHeaders is null)
                return NotFound("no data found");

            return View(InvoiceHeaderViewModel);
        }

        public IActionResult Details(int id)
        {
            InvoiceHeaderViewModel.InvoiceHeader = _unitOfWork.InvoiceHeaderRepository.FindObject(x => x.Id == id);
            if (InvoiceHeaderViewModel.InvoiceHeader is null)
                return NotFound("no data found");

            InvoiceHeaderViewModel.TotalPrice = 0;
            foreach (var item in InvoiceHeaderViewModel.InvoiceHeader.InvoiceDetails)
            {
                InvoiceHeaderViewModel.TotalPrice += item.ItemCount * item.ItemPrice;
            }

            return View(InvoiceHeaderViewModel);
        }

        public IActionResult Add()
        {
            LoadSelectListItems();

            return View("Add_Update", InvoiceHeaderViewModel);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdd()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectListItems();

                return View("Add_Update", InvoiceHeaderViewModel);
            }

            _unitOfWork.InvoiceHeaderRepository.Add(InvoiceHeaderViewModel.InvoiceHeader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            InvoiceHeaderViewModel.InvoiceHeader = _unitOfWork.InvoiceHeaderRepository.FindObject(x => x.Id == id);

            LoadSelectListItems();

            if (InvoiceHeaderViewModel.InvoiceHeader is null)
                return NotFound("no data found");

            return View("Add_Update", InvoiceHeaderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectListItems();

                return View("Add_Update", InvoiceHeaderViewModel);
            }

            _unitOfWork.InvoiceHeaderRepository.Update(InvoiceHeaderViewModel.InvoiceHeader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                InvoiceHeaderViewModel.InvoiceHeader = _unitOfWork.InvoiceHeaderRepository.FindObject(x => x.Id == id);
                if (InvoiceHeaderViewModel.InvoiceHeader is null)
                    return NotFound("no data found");

                _unitOfWork.InvoiceHeaderRepository.Remove(InvoiceHeaderViewModel.InvoiceHeader);
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
            InvoiceHeaderViewModel.BranchListItems = _unitOfWork.BranchRepository.GetAll().Select(branch => new SelectListItem
            {
                Text = branch.BranchName,
                Value = branch.Id.ToString(),
                Selected = InvoiceHeaderViewModel.InvoiceHeader is not null && InvoiceHeaderViewModel.InvoiceHeader.BranchId == branch.Id
            }).ToList();

            InvoiceHeaderViewModel.CashierListItems = _unitOfWork.CashierRepository.GetAll().Select(cashier => new SelectListItem
            {
                Text = cashier.CashierName,
                Value = cashier.Id.ToString(),
                Selected = InvoiceHeaderViewModel.InvoiceHeader is not null && InvoiceHeaderViewModel.InvoiceHeader.CashierId == cashier.Id
            }).ToList();
        }
    }
}
