using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Data.ViewModels;
using ProjectDATN.Web.Helpers;
using ProjectDATN.Web.Models;

namespace ProjectDATN.Web.Controllers
{
    public class CheckOutController : Controller
    {

        private readonly ApplicationDBContext _db;
        public INotyfService _notyfService { get; }

        public CheckOutController(ApplicationDBContext db, INotyfService notyfService)
        {
            _db = db;
            _notyfService = notyfService;
        }

        public List<CartItem> Carts
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;

            }
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanId = HttpContext.Session.GetString("Id");
            MuaHangVM vm = new MuaHangVM();
            if (taikhoanId != null)
            {
                var khachhang = _db.Users.AsNoTracking().SingleOrDefault(x => x.Id == Convert.ToString(taikhoanId));
                vm.UserId = khachhang.Id;
                vm.FullName = khachhang.UserName;
                vm.Email = khachhang.Email;
                vm.PhoneNumber = khachhang.PhoneNumber;
                vm.Address = khachhang.HomeAdress;
            }

            ViewBag.GioHang = cart;
            return View(vm);

        }

        [HttpPost]

        public IActionResult Index(MuaHangVM muahang)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var taikhoanId = HttpContext.Session.GetString("Id");
            MuaHangVM vm = new MuaHangVM();
            if (taikhoanId != null)
            {
                var khachhang = _db.Users.AsNoTracking().SingleOrDefault(x => x.Id == Convert.ToString(taikhoanId));
                vm.UserId = khachhang.Id;
                vm.FullName = khachhang.UserName;
                vm.Email = khachhang.Email;
                vm.PhoneNumber = khachhang.PhoneNumber;
                vm.Address = khachhang.HomeAdress;
                vm.PhuongXa = muahang.PhuongXa;
                vm.Tinh = muahang.Tinh;
                vm.Huyen = muahang.Huyen;
            }

            try
            {
                //if (ModelState.IsValid)
                //{
                    Order donhang = new Order();
                    donhang.UserId = vm.UserId;
                    donhang.UserName = _db.Users.FirstOrDefault(x => x.Id == vm.UserId).UserName;
                    donhang.Address = vm.Tinh + " - " + vm.Huyen + " - " + vm.PhuongXa + " - " + vm.Address;
                    
                    donhang.PhoneNumber = vm.PhoneNumber;
                    donhang.TotalPrice = Convert.ToDecimal(cart.Sum(x => x.PriceTotal));
                    donhang.Payment = vm.Payment;
                    donhang.IsPay = false;
                   
                    
                    donhang.Status = 0;
                    _db.Add(donhang);
                    _db.SaveChanges();

                    foreach (var item in cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = donhang.Id;
                        orderDetail.ProductId = item.ProductId;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.Price = donhang.TotalPrice;
                        orderDetail.CreatedDate = DateTime.Now;
                        _db.Add(orderDetail);
                    }
                    _db.SaveChanges();

                    HttpContext.Session.Remove("GioHang");
                    _notyfService.Success("Đơn hàng đã đặt thành công");
                    return RedirectToAction("Success");
                //}
            }
            catch (Exception ex)
            {
                return View(vm);
            }
            ViewBag.GioHang = cart;
            return View(vm);

        }

        public IActionResult Success()
        {
            try
            {
                var taikhoanId = HttpContext.Session.GetString("Id");

                if (string.IsNullOrEmpty(taikhoanId))
                {
                    return RedirectToAction("/login");

                }

                var khachhang = _db.Users.AsNoTracking().SingleOrDefault(x => x.Id == taikhoanId);
                var donhang = _db.Orders.Where(x => x.UserId == taikhoanId).OrderByDescending(x => x.OrderDate).FirstOrDefault();
                MuahangSuccessVM successVM = new MuahangSuccessVM();
                successVM.UserName = khachhang.UserName;
                successVM.OrderID = donhang.Id;
                successVM.Address = donhang.Address;
                successVM.PhoneNumber = khachhang.PhoneNumber;
                return View(successVM);
            }
            catch
            {
                return View();
            }
        }
    }
}
