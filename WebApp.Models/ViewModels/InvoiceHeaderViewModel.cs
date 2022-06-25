using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models.ViewModels
{
    public class InvoiceHeaderViewModel
    {
        public InvoiceHeaderViewModel()
        {
            InvoiceHeader = new InvoiceHeader();
            InvoiceHeaders = new List<InvoiceHeader>();
            BranchListItems = new List<SelectListItem>();
            CashierListItems = new List<SelectListItem>();
        }

        public double TotalPrice { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public IEnumerable<InvoiceHeader> InvoiceHeaders { get; set; }
        public IEnumerable<SelectListItem> BranchListItems { get; set; }
        public IEnumerable<SelectListItem> CashierListItems { get; set; }
    }
}
