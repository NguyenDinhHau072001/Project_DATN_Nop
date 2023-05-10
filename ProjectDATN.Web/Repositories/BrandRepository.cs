using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;

namespace ProjectDATN.Web.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private ApplicationDBContext _context;
        public BrandRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public IList<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }
    }
}
