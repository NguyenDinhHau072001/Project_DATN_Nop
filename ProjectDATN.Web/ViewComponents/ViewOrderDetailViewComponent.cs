using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;

namespace ProjectDATN.Web.ViewComponents
{
    public class ViewOrderDetailViewComponent : ViewComponent
    {
        ApplicationDBContext _db;
        public ViewOrderDetailViewComponent(ApplicationDBContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string name)
        {
            if (name == "OrderDetail")
            {
                var listOrder = _db.OrderDetails.OrderBy(x => x.Id).ToList();
                return View(listOrder);
            }
            
            return View();
        }
    }
}
