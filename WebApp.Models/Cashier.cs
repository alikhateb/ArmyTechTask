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
        public string CashierName { get; set; }

        [Display(Name = "Branch Name")]
        public int BranchId { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; }
    }
}
