﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Data.Enums;
using ProjectDATN.Data.ViewModels;
using ProjectDATN.Web.Helpers;
using ProjectDATN.Web.Models;
using ProjectDATN.Web.Services;

namespace ProjectDATN.Web.Controllers
{
    public class CheckOutController : Controller
    {

        private readonly ApplicationDBContext _db;
        public INotyfService _notyfService { get; }
        private readonly IVnPayService _vnPayService;

        public CheckOutController(ApplicationDBContext db, INotyfService notyfService, IVnPayService vnPayService)
        {
            _db = db;
            _notyfService = notyfService;
            _vnPayService = vnPayService;
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
                vm.Tinh = khachhang.Tinh;
                vm.PhuongXa = khachhang.Xa;
                vm.Huyen = khachhang.Huyen;
            }

            ViewBag.GioHang = cart;
            return View(vm);

        }

        [HttpPost]

        public IActionResult Index(MuaHangVM muahang, IFormCollection form)
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
                if (khachhang.PhoneNumber != null)
                {
                    vm.PhoneNumber = khachhang.PhoneNumber;
                }
                else
                {
                    vm.PhoneNumber = muahang.PhoneNumber;
                }
                if(khachhang.HomeAdress != null)
                {
                    vm.Address = muahang.Address;
                }
                else
                {
                    vm.Address = khachhang.HomeAdress;
                }
                
                if(khachhang.Xa == null)
                {
                    vm.PhuongXa = muahang.PhuongXa;
                }
                else
                {
                    vm.PhuongXa = khachhang.Xa;
                }
                if (khachhang.Tinh == null)
                {
                    vm.Tinh = muahang.Tinh;
                }
                else
                {
                    vm.Tinh = khachhang.Tinh;
                }
                if (khachhang.Huyen == null)
                {
                    vm.Tinh = muahang.Tinh;
                }
                else
                {
                    vm.Huyen = khachhang.Huyen;
                }
                vm.Payment = muahang.Payment;
                if (muahang.Payment.Equals("cod"))
                {
                    vm.Payment = Data.Enums.PaymentStatus.cod;
                }
                else
                {
                    vm.Payment = Data.Enums.PaymentStatus.vnpay;
                }
                khachhang.HomeAdress = muahang.Address;
                khachhang.PhoneNumber = muahang.PhoneNumber;
                khachhang.Tinh = muahang.Tinh;
                khachhang.Huyen = muahang.Huyen;
                khachhang.Xa = muahang.PhuongXa;
                _db.SaveChanges();
            }

            try
            {
                //if (ModelState.IsValid)
                //{



                Order donhang = new Order();
                donhang.UserId = vm.UserId;
                donhang.UserName = _db.Users.FirstOrDefault(x => x.Id == vm.UserId)?.UserName;
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
                    orderDetail.Price = item.Price;
                    orderDetail.CreatedDate = DateTime.Now;
                    _db.Add(orderDetail);
                }
                _db.SaveChanges();


                foreach(var item in cart)
                {
                    var product = _db.Products.FirstOrDefault(x=>x.Id == item.ProductId);
                    product.Quality = product.Quality - item.Quantity;
                    product.SaleQuatity = product.SaleQuatity + item.Quantity;
                    _db.Update(product);
                }
                _db.SaveChanges();
                HttpContext.Session.SetString("OrderId", donhang.Id.ToString());
                var oderID = HttpContext.Session.GetString("OderId");
                HttpContext.Session.Remove("GioHang");

                if (form["PaymentMethod"] == "cod")
                {
                    donhang.Payment = PaymentStatus.cod;
                    _db.SaveChanges();
                    //_notyfService.Success("Đơn hàng đã đặt thành công");
                    return RedirectToAction("Success");
                }
                else
                {
                    var url = _vnPayService.CreatePaymentUrl(donhang, HttpContext);
                    donhang.Payment = PaymentStatus.vnpay;
                    _db.SaveChanges();

                    return Redirect(url);
                   // _notyfService.Success("Thanh toán bằng VNPAY ở đây");

                   // return View();
                }

               
                //}
            }
            catch (Exception ex)
            {
                return View(vm);
            }
            ViewBag.GioHang = cart;
            return View(vm);

        }
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if(response.UserName == "falsefalse")
            {
                return RedirectToAction("Fail");
            }
            //return Json(response);
            return RedirectToAction("Success");
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
                successVM.OrderStatus = _db.Orders.FirstOrDefault(x => x.Id == successVM.OrderID)?.Status;

                _notyfService.Success("Bạn đã đặt hàng thành công!");
                return View(successVM);
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Fail()
        {
            return View();
        }
        //public ThanhToanOnl()
        //{
        //    var cartItems = HttpContext.Session.Get<List<CartItem>>("GioHang");
        //    string endpoint = ConfigurationManager.AppSettings["endpoint"].ToString();
        //}
    }
}
