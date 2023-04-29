using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDATN.Data.EF;
using ProjectDATN.Web.Models;
using System.Diagnostics;

namespace ProjectDATN.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }
 
        public IActionResult Index()
        {
            /*ViewBag.ListProductFeatures = _db.Products.OrderByDescending(x => x.SaleQuatity).Take(30).ToList();
            ViewBag.ListProductPromotion = _db.Products.Join(_db.Promotions, p => p.Id, pr => pr.ProId, (p, pr) => new
            {
                p.ProName,
                p.PerchasePrice,
                pr.Promo,
                p.Image
            }).Where(pr => pr.Promo == Data.Enums.KM.Percent75).ToList();


            var products = _db.Products.Take(10).ToList();*/
            return View();
        }

        /*public IActionResult GetProductByCateName(string? name)
        {
            ViewBag.ListProduct = _db.Products.Join(_db.Categories, p => p.CateID, c => c.Id, (p, c) => new
            {
                p.ProName,
                p.PerchasePrice,
                c.CateName
            }).Where(c => c.CateName.Equals(name)).ToList();
            return View(ViewBag.ListProduct);
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}