using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ThongKeController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            DateTime dateTimeNow = DateTime.Now.Date;
            dateTimeNow = dateTimeNow.AddYears(-1);

            string[] dateX = new string[12];
            string[] data = new string[12];
            for (int i = 0; i < 12; i++)
            {

                dateX[i] = (dateTimeNow.Month.ToString() + "/" + dateTimeNow.Year.ToString()).ToString();
                var temp = _db.Orders.Where(a => a.OrderDate.Month == dateTimeNow.Month && a.Status == Data.Enums.OrderStatus.Success).Sum(s => s.TotalPrice);
                if (temp == null)
                {
                    temp = 0;
                }
                data[i] = temp.ToString();
                dateTimeNow = dateTimeNow.AddMonths(1);
            }
            ViewBag.dateX = dateX;
            ViewBag.data = data;

            // DatachartLine();
            //var ac = (Admin)Session["Account"];
            //if (ac == null)
            //{
            //    return RedirectToAction("Login", "Admin");
            //}
            //else { return View(); }

            return View();
        }
    }
}
