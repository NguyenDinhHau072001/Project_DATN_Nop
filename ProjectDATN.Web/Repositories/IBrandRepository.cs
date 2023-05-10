using ProjectDATN.Data.Entities;

namespace ProjectDATN.Web.Repositories
{
    public interface IBrandRepository
    {
        IList<Brand> GetAll();
    }
}
