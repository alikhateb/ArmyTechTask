namespace WebApp.Models.ViewModels
{
    public class BranchViewModel
    {
        public BranchViewModel()
        {
            Branch = new();
            Branches = new List<Branch>();
            ListItems = new List<SelectListItem>();
        }
        public Branch Branch { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public IEnumerable<SelectListItem> ListItems { get; set; }
    }
}
