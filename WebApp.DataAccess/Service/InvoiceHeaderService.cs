using WebApp.DataAccess.IService;

namespace WebApp.DataAccess.Service
{
    public class InvoiceHeaderService : BaseService<InvoiceHeader>, IInvoiceHeaderService
    {
        public InvoiceHeaderService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
