using WebApp.DataAccess.IService;

namespace WebApp.DataAccess.Service
{
    public class BranchService : BaseService<Branch>, IBranchService
    {
        public BranchService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
