using WebApp.DataAccess.IService;

namespace WebApp.DataAccess.Service
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
