using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;

namespace ProjectDATN.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ContactController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact a)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(a);
                _db.SaveChanges();
                TempData["AlertMessage"] = "Cám ơn sự đóng góp của bạn!! Chúng tôi sẽ nhanh chóng cập nhật thêm tính năng mới hoặc sửa lại những tính năng lỗi để mang lại trải nghiệm tốt nhất đến với khách hàng";
                return RedirectToAction("Contact");
            }
            return View();
        }
    }
}
