using WebApp.DataAccess.IService;

namespace WebApp.DataAccess.Service
{
    public class CashierService : BaseService<Cashier>, ICashierService
    {
        public CashierService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
