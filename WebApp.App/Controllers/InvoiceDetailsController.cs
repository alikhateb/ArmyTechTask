using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccess.UnitOfWork;
using WebApp.Models;

namespace WebApp.App.Controllers
{
    public class InvoiceDetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var invoiceDetails = _unitOfWork.InvoiceDetailRepository.GetAll();
            if (invoiceDetails is null)
                return NotFound("no data found");

            return View(invoiceDetails);
        }

        public IActionResult Add(int invoiceHeaderId)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            InvoiceDetail invoiceDetail = new()
            {
                InvoiceHeaderId = invoiceHeaderId,
            };

            return View("Add_Update", invoiceDetail);
        }

        [HttpPost]
        [ActionName(name: "Add")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitAdd(InvoiceDetail invoiceDetail, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Add_Update", invoiceDetail);
            }

            _unitOfWork.InvoiceDetailRepository.Add(invoiceDetail);
            _unitOfWork.Save();
            return Redirect(returnUrl);
        }

        public IActionResult Update(int id)
        {
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            InvoiceDetail invoiceDetail = _unitOfWork.InvoiceDetailRepository.FindObject(x => x.Id == id);

            if (invoiceDetail is null)
                return NotFound("no data found");

            return View("Add_Update", invoiceDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(InvoiceDetail invoiceDetail, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Add_Update", invoiceDetail);
            }

            _unitOfWork.InvoiceDetailRepository.Update(invoiceDetail);
            _unitOfWork.Save();
            return Redirect(returnUrl);
        }

        public IActionResult Delete(int invoiceDetailId, int invoiceHeaderId)
        {
            try
            {
                InvoiceDetail invoiceDetail = _unitOfWork.InvoiceDetailRepository.FindObject(x => x.Id == invoiceDetailId);
                if (invoiceDetail is null)
                    return NotFound("no data found");

                _unitOfWork.InvoiceDetailRepository.Remove(invoiceDetail);
                _unitOfWork.Save();
                return RedirectToAction(actionName: "Details", controllerName: "InvoiceHeaders", routeValues: new { id = invoiceHeaderId });
            }
            catch
            {
                return BadRequest("can't delete this item since it's in use");
            }
        }

    }
}
