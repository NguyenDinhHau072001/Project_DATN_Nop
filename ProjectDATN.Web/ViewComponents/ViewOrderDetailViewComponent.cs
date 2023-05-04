using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.ViewModels;

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
                var taikhoanId = HttpContext.Session.GetString("Id");
                var oderID = HttpContext.Session.GetString("OrderId");
                var orderDetails = _db.OrderDetails.Join(_db.Orders, x=>x.OrderId, y => y.Id, (x, y) => new
                {
                    y.UserId,
                    x.Id,
                    x.ProductId,
                    x.OrderId,
                    x.Quantity,
                    x.Price,
                    x.CreatedDate
                }).Where(a=>a.UserId == taikhoanId && a.OrderId ==Convert.ToInt32( oderID)).ToList();
                var listOrderDetail = new List<OrderDetailVM>();
                foreach (var detail in orderDetails)
                {
                    OrderDetailVM vm = new()
                    {
                        Id = detail.Id,
                        OrderId = detail.Id,
                        Image = _db.Products.FirstOrDefault(x => x.Id == detail.ProductId)?.Image,
                        ProductName = _db.Products.FirstOrDefault(x=>x.Id== detail.ProductId)?.ProName,
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                        CreatedDate = detail.CreatedDate
                    };
                    listOrderDetail.Add(vm);
                }
                return View(listOrderDetail);
            }
            
            return View();
        }
    }
}
