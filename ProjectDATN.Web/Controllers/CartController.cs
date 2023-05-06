using AspNetCoreHero.ToastNotification.Abstractions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Web.Helpers;
using ProjectDATN.Web.Models;

namespace ProjectDATN.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDBContext _db;
        public INotyfService _notityService { get; }
        public CartController(ApplicationDBContext db, INotyfService notyfService)
        {
            _db = db;
            _notityService = notyfService;
        }


        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        public IActionResult Index()
        {
            var myCart = Carts;
            if(myCart != null)
            {
                return View(myCart);
            }
            return View();
        }
       
        public IActionResult Partial_View_Cart()
        {
            var myCart = Carts;
            var products = _db.Products;
            ViewBag.DSProduct = products;
            if(myCart != null)
            {
                return PartialView(myCart);
            }
            return PartialView();
        }
        public IActionResult AddToCart(int id, int quantity)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(x => x.ProductId == id);
            if (item == null)
            {
                var product = _db.Products.SingleOrDefault(x => x.Id == id);
                item = new CartItem
                {
                    ProductId = id,
                    ProductName = product.ProName,
                    Image = product.Image,
                    Price = product.PerchasePrice,
                    Quantity = quantity,
                    MaxQuantity = _db.Products.FirstOrDefault(p => p.Id == id).Quality
                };
                myCart.Add(item);

            }
            else
            {
                item.Quantity += quantity;

            }
            HttpContext.Session.Set("GioHang", myCart);
            _notityService.Success("Thêm vào giỏ hàng thành công!");
            return Json(new { success = true, count = Carts.Count });
        }
        [HttpPost]
		public IActionResult Delete(int id)
		{
			var code = new { Success = false, msg = "", code = -1, SoLuong = 0 };
			var myCart = Carts;
			if (myCart != null)
			{
				var item = myCart.FirstOrDefault(x => x.ProductId == id);
				if (item != null)
				{
					myCart.Remove(item);
					code = new { Success = true, msg = "", code = -1, SoLuong = myCart.Count };
				}
			}
			HttpContext.Session.Set("GioHang", myCart);
			return Json(code);
		}
		[HttpPost]
		public IActionResult Update(int id, int Quantity)
		{

			List<CartItem> myCart = Carts;
			if (myCart != null)
			{
				var item = myCart.SingleOrDefault(x => x.ProductId == id);
				if (item != null)
				{
					if (Quantity < 10)
					{
						item.Quantity = Quantity;
					}
					else
					{
						item.Quantity = 10;
					}

				}
				HttpContext.Session.Set("GioHang", myCart);
				return Json(new { Success = true, SoLuong = Carts.Sum(c => c.Quantity) });
			}

			return Json(new { Success = false });
		}

		public IActionResult CheckOut()
		{
			var mycart = Carts;
			if (mycart != null)
			{
				ViewBag.GioHang = mycart;
				/* var kha*/
				return View();
			}
			return View();
		}

	}


}
