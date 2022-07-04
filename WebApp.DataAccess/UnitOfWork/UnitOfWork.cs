using WebApp.DataAccess.IService;
using WebApp.DataAccess.Service;

namespace WebApp.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            BranchSrvice = new BranchService(context);
            CashierService = new CashierService(context);
            CityService = new CityService(context);
            InvoiceHeaderService = new InvoiceHeaderService(context);
            InvoiceDetailService = new InvoiceDetailService(context);
        }

        public IBranchService BranchSrvice { get; private set; }
        public ICashierService CashierService { get; private set; }
        public ICityService CityService { get; private set; }
        public IInvoiceHeaderService InvoiceHeaderService { get; private set; }
        public IInvoiceDetailService InvoiceDetailService { get; private set; }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
