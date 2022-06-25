using WebApp.DataAccess.Data;
using WebApp.DataAccess.IRepository;
using WebApp.DataAccess.Repository;
using WebApp.Models;

namespace WebApp.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            BranchRepository = new BaseRepository<Branch>(context);
            CashierRepository = new BaseRepository<Cashier>(context);
            CityRepository = new BaseRepository<City>(context);
            InvoiceHeaderRepository = new BaseRepository<InvoiceHeader>(context);
            InvoiceDetailRepository = new BaseRepository<InvoiceDetail>(context);
        }

        public IBaseRepository<Branch> BranchRepository { get; private set; }
        public IBaseRepository<Cashier> CashierRepository { get; private set; }
        public IBaseRepository<City> CityRepository { get; private set; }
        public IBaseRepository<InvoiceHeader> InvoiceHeaderRepository { get; private set; }
        public IBaseRepository<InvoiceDetail> InvoiceDetailRepository { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
