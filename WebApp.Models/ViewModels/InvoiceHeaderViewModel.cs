namespace WebApp.Models.ViewModels
{
    public class InvoiceHeaderViewModel
    {
        public InvoiceHeaderViewModel()
        {
            InvoiceDetails = new List<InvoiceDetail>();
            BranchListItems = new List<SelectListItem>();
            CashierListItems = new List<SelectListItem>();
        }

        public long Id { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Invoice Date")]
        public DateTime Invoicedate { get; set; }

        [Display(Name = "Cashier Name")]
        [Range(1, int.MaxValue, ErrorMessage = "you should choose cashier")]
        public int CashierId { get; set; }

        [Display(Name = "Branch Name")]
        [Range(1, int.MaxValue, ErrorMessage = "you should choose branch")]
        public int BranchId { get; set; }

        public double TotalPrice { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public IEnumerable<SelectListItem> BranchListItems { get; set; }
        public IEnumerable<SelectListItem> CashierListItems { get; set; }
    }
}
