using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class HomeController : Controller
	{
		private readonly ApplicationDBContext _dbContext;
        public HomeController(ApplicationDBContext dBContext)
        {
			_dbContext = dBContext;
        }
        public IActionResult Index()
		{
			int order = _dbContext.Orders.Count();
			int user = _dbContext.Users.Count();
			ViewBag.OrderCount = order;
			ViewBag.UserCount = user;

			int sale = _dbContext.Products.Sum(x => x.SaleQuatity);
			int quantity = _dbContext.Products.Sum(x => x.Quality);
			float rate = (float)sale/(sale+quantity);

			ViewBag.Rate = (rate*100).ToString("0.00");
			return View();
		}

		
	}
}