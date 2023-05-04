using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;

namespace ProjectDATN.Web.Controllers
{
    
    public class PostController : Controller
    {
        private readonly ApplicationDBContext _db;
        public PostController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var post = _db.Posts.OrderByDescending(x => x.Id).FirstOrDefault();
            return View(post);
        }

        public IActionResult GetPostById(int id)
        {
            var post = _db.Posts.FirstOrDefault(x => x.Id == id);
            return View(post);
        }
    }
}
