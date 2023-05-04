using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;

namespace ProjectDATN.Web.ViewComponents
{
    public class PostViewComponent : ViewComponent
    {
        ApplicationDBContext _db;
        public PostViewComponent(ApplicationDBContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string name)
        {
            if (name == "Post")
            {
                var listPost = _db.Posts.OrderByDescending(x => x.Id).Take(8).ToList();
                return View(listPost);
            }
            
            return View();
        }
    }
}
