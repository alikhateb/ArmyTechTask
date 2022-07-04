namespace WebApp.Models.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Cashiers = new HashSet<Cashier>();
            InvoiceHeaders = new HashSet<InvoiceHeader>();
        }

        public int Id { get; set; }

        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "City Name")]
        [Range(1, int.MaxValue)]
        public int CityId { get; set; }

        public virtual City? City { get; set; }
        public virtual ICollection<Cashier> Cashiers { get; set; }
        public virtual ICollection<InvoiceHeader> InvoiceHeaders { get; set; }
    }
}
