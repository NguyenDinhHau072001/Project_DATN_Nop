using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.Entities;
using ProjectDATN.Web.Models.Payment;
using ProjectDATN.Web.Services;

namespace ProjectDATN.Web.Controllers
{
    public class VNPayController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VNPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(Order model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {

            var response = _vnPayService.PaymentExecute(Request.Query);
            return Json(response);
        }
    }
}
