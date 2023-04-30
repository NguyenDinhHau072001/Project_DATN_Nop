using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Data.ViewModels;
using X.PagedList;

namespace ProjectDATN.Web.Controllers
{

	public class ProductController : Controller
	{
		private readonly ApplicationDBContext _db;
		public ProductController(ApplicationDBContext db) { _db = db; }


		public IActionResult Index(int? page, string search)
		{
			var listProducts = new List<ProductVM>();
			var products = new List<Product>();
			if(search != null && search != "")
			{
				products = _db.Products.Where(x=>x.ProName.Contains(search)).ToList();
			}
			else
			{
                products = _db.Products.ToList();
            }
			
			foreach (var item in products)
			{
				ProductVM vm = new()
				{
					Id = item.Id,
					ProName = item.ProName,
					Price = item.Price,
					PerchasePrice = item.PerchasePrice,
					Image = item.Image,
					Quantity = item.Quality
				};
				listProducts.Add(vm);
			}
			int pageSize = 9;
			int pageNumber = (page ?? 1);

			return View(listProducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
			//return View(listProducts);
		
		}
		public IActionResult GetProductByCateId(int? id, int? page)
		{
			var listProducts = new List<ProductVM>();
			
			var products = _db.Products.Where(x=>x.CateID == id).ToList();
			foreach(var item in products)
			{
				ProductVM vm = new()
				{
					Id = item.Id,
					ProName = item.ProName,
					Price = item.Price,
					PerchasePrice = item.PerchasePrice,
					Image = item.Image,
					Quantity = item.Quality
				};
				listProducts.Add(vm);
			}
			int pageSize = 6;
			int pageNumber = (page ?? 1);

			return View(listProducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
		}


		public IActionResult GetProductByBrandId(int? id, int? page)
		{
			var listProducts = new List<ProductVM>();

			var products = _db.Products.Where(x => x.BandID == id).ToList();
			foreach (var item in products)
			{
				ProductVM vm = new()
				{
					Id = item.Id,
					ProName = item.ProName,
					Price = item.Price,
					PerchasePrice = item.PerchasePrice,
					Image = item.Image,
					Quantity = item.Quality
				};
				listProducts.Add(vm);
			}
			int pageSize = 6;
			int pageNumber = (page ?? 1);

			return View(listProducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
		}

		public IActionResult Details(int id)
		{
			var product = _db.Products.FirstOrDefault(X => X.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		/*public IActionResult GetProductByCateID(int? id)
		{
			return View();
		}*/
	}
}
