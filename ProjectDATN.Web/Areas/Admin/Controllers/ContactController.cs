using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using X.PagedList;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly ApplicationDBContext _db;

        public ContactController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? page)
        {
            var listContact = _db.Contacts.ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(listContact.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Detail(int id)
        {
            var contact = _db.Contacts.FirstOrDefault(x => x.Id == id);
            return View(contact);
        }
    }
}
