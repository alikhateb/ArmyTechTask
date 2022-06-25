using System.ComponentModel.DataAnnotations;

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
        public string CustomerName { get; set; } = null!;

        [Display(Name = "Invoice Date")]
        public DateTime Invoicedate { get; set; }

        [Display(Name = "Cashier Name")]
        public int? CashierId { get; set; }

        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }

        public virtual Branch? Branch { get; set; } = null!;
        public virtual Cashier? Cashier { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
