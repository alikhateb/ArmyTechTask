namespace WebApp.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Branch> BranchRepository { get; }
        IBaseRepository<Cashier> CashierRepository { get; }
        IBaseRepository<City> CityRepository { get; }
        IBaseRepository<InvoiceHeader> InvoiceHeaderRepository { get; }
        IBaseRepository<InvoiceDetail> InvoiceDetailRepository { get; }
        void SaveChanges();
    }
}