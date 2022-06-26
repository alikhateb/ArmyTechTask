using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public partial class City
    {
        public City()
        {
            Branches = new HashSet<Branch>();
        }

        public int Id { get; set; }

        [Display(Name = "City Name")]
        public string CityName { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }
}
