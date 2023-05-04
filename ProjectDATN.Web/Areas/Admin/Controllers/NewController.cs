using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Data.ViewModels;
using X.PagedList;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewController : Controller
    {
        private readonly ApplicationDBContext _db;

        public NewController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? page, string search = "")
        {
            List<Post> posts;
            if (search != "" && search != null)
            {
                posts = _db.Posts.Where(c => c.Name.Contains(search)).ToList();
            }
            else
            {
                posts = _db.Posts.ToList();
            }


            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(posts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post a)
        {
            if (ModelState.IsValid)
            {
                _db.Posts.Add(a);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(_db.Categories);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var postID = _db.Posts.FirstOrDefault(u => u.Id == id);
            if (postID == null)
                return NotFound();
            return View(postID);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post a)
        {
            if (ModelState.IsValid)
            {
                _db.Posts.Update(a);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var postID = _db.Posts.FirstOrDefault(u => u.Id == id);
            if (postID == null)
                return NotFound();
            return PartialView("DeletePost", postID);
        }

        [HttpPost]
        public IActionResult Delete(Post a)
        {
            _db.Posts.Remove(a);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Details(int id)
        {
            var post = _db.Posts.FirstOrDefault(X => X.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

    }
}