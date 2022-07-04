using WebApp.DataAccess.IService;

namespace WebApp.DataAccess.Service
{
    public class InvoiceDetailService : BaseService<InvoiceDetail>, IInvoiceDetailService
    {
        public InvoiceDetailService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
