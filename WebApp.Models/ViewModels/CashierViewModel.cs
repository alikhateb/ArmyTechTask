namespace WebApp.Models.ViewModels
{
    public class CashierViewModel
    {
        public CashierViewModel()
        {
            Cashier = new Cashier();
            Cashiers = new List<Cashier>();
            ListItems = new List<SelectListItem>();
        }
        public Cashier Cashier { get; set; }
        public IEnumerable<Cashier> Cashiers { get; set; }
        public IEnumerable<SelectListItem> ListItems { get; set; }

    }
}
