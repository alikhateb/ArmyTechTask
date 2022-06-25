using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public partial class Cashier
    {
        public Cashier()
        {
            InvoiceHeaders = new HashSet<InvoiceHeader>();
        }

        public int Id { get; set; }

        [Display(Name = "Cashier Name")]
        public string CashierName { get; set; } = null!;

        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }

        public virtual Branch? Branch { get; set; } = null!;
        public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; }
    }
}
