namespace WebApp.Models
{
    public partial class InvoiceHeader
    {
        public InvoiceHeader()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public long Id { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Invoice Date")]
        public DateTime Invoicedate { get; set; }

        [Display(Name = "Cashier Name")]
        [Range(1, int.MaxValue)]
        public int? CashierId { get; set; }

        [Display(Name = "Branch Name")]
        [Range(1, int.MaxValue)]
        public int BranchId { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual Cashier? Cashier { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
