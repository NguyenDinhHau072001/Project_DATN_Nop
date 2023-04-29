using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.ViewModels;
using ProjectDATN.Web.Models;
using X.PagedList;

namespace ProjectDATN.Web.Controllers
{
    public class PromotionController : Controller
    {
        private readonly ApplicationDBContext _db;
        public PromotionController(ApplicationDBContext db) { _db = db; }

        public IActionResult Index(int? page)
        {
            var listProducts = new List<PrmotionVM>();

            var products = _db.Promotions.ToList();
            foreach (var item in products)
            {
                PrmotionVM vm = new()
                {
                    Id = item.Id,
                    ProId = item.ProId,
                    ProName = _db.Products.FirstOrDefault(x=>x.Id == item.ProId).ProName,
                    Price = _db.Products.FirstOrDefault(x=>x.Id == item.ProId).Price,
                    PurChasePrice = _db.Products.FirstOrDefault(x => x.Id == item.ProId).PerchasePrice,
                    ProImage = _db.Products.FirstOrDefault(x => x.Id == item.ProId).Image,
                    KM = item.Promo
                    
                };
                listProducts.Add(vm);
            }
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(listProducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
        }
    }
}
