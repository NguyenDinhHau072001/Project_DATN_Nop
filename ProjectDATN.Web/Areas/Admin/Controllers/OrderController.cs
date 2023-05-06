using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.ViewModels;
using X.PagedList;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDBContext _db;
        public INotyfService _notify { get;  }
        public OrderController(ApplicationDBContext db, INotyfService notify)
        {
            _db = db;
            _notify = notify;
        }
        public IActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageNumber = page ?? 1;
            var order = _db.Orders.OrderByDescending(x => x.Id).ToList();


            return View(order.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Detail(int id)
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);

            var orderDetails = _db.OrderDetails.Where(x => x.OrderId == order.Id).ToList();
            var listOrderDetailVM = new List<OrderDetailVM>();
            foreach (var item in orderDetails)
            {
                OrderDetailVM vm = new()
                {
                    ProductId = item.ProductId,
                    ProductName = _db.Products.FirstOrDefault(x => x.Id == item.ProductId).ProName,
                    Price = (decimal)(_db.Products.FirstOrDefault(x => x.Id == item.ProductId)?.PerchasePrice),
                    Quantity = item.Quantity,
                    TotalPrice = item.Price,
                    CreatedDate = DateTime.Now

                };
                listOrderDetailVM.Add(vm);
            }


            ViewBag.Order = order;

            return View(listOrderDetailVM);
        }

        public IActionResult ChangeStatus(int id, int status)
        {
            var order = _db.Orders.FirstOrDefault(x => x.Id == id);
            var orderDetail = _db.OrderDetails.Join(_db.Orders, detail => detail.OrderId, oder => oder.Id, (detail, oder) => new
            {
                detail.ProductId,
                oder.Id,
                detail.Quantity
            }).Where(x => x.Id == id).ToList();
            if (status == 1)
            {
                order.Status = Data.Enums.OrderStatus.Confirmed;
            }
            if(status == 2)
            {
                order.Status = Data.Enums.OrderStatus.Shipping;
            }
            if (status == 3)
            {
                order.Status = Data.Enums.OrderStatus.Success;
            }
            if (status == 4)
            {
                order.Status = Data.Enums.OrderStatus.Canceled;
                foreach (var item in orderDetail)
                {
                    var product = _db.Products.FirstOrDefault(x => x.Id == item.ProductId);
                    product.Quality = product.Quality + item.Quantity;
                    product.SaleQuatity = product.SaleQuatity - item.Quantity;
                    _db.Products.Update(product);
                    _db.SaveChanges();
                }
            }
            _db.SaveChanges();
            _notify.Success("Thay đổi trạng thái đơn hàng thành công!");
            return Json(new {success = true, status = order.Status.ToString()});
        }
    }
}
