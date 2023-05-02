using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Web.Helpers;
using ProjectDATN.Web.Models;

namespace ProjectDATN.Web.ViewComponents
{
    public class ProductInCartViewComponent : ViewComponent
    {
        ApplicationDBContext _db;
        public INotyfService _notityService { get; }
        public ProductInCartViewComponent(ApplicationDBContext db, INotyfService notyfService)
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
        public IViewComponentResult Invoke(string name)
        {
            var myCart = Carts;
            if(myCart != null)
            {
                if (name == "Cart")
                { 
                    return View(myCart);
                }
            }
            return View();
        }
    }
}
