using WebApp.DataAccess.IService;

namespace WebApp.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBranchService BranchSrvice { get; }
        ICashierService CashierService { get; }
        ICityService CityService { get; }
        IInvoiceHeaderService InvoiceHeaderService { get; }
        IInvoiceDetailService InvoiceDetailService { get; }
        void SaveChanges();
    }
}