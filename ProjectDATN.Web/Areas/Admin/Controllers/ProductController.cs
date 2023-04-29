using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Data.ViewModels;
using X.PagedList;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly ApplicationDBContext _db;

		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly UserManager<AppUser> _userManager;

		public ProductController(ApplicationDBContext context, IWebHostEnvironment webHostEnviroment, UserManager<AppUser> userManager)
		{
			_db = context;

			_webHostEnvironment = webHostEnviroment;
			_userManager = userManager;
		}

		public IActionResult Index(int? page)
		{
			var listProducts = new List<ProductVM>();
			var products = _db.Products.OrderByDescending(x => x.Id).ToList();
			foreach (var item in products)
			{
				ProductVM vm = new()
				{
					Id = item.Id,
					ProName = item.ProName,
					Description = item.Description,
					Quantity = item.Quality,
					Price = item.Price,
					PerchasePrice = item.PerchasePrice,
					Image = item.Image,
					CateID = item.CateID,
					CateName = _db.Categories?.Find(item.CateID)?.CateName,
					BrandName = _db.Brands.Find(item.BandID)?.Name,
					BandID = item.BandID
				};
				listProducts.Add(vm);
			}
			//_db.Products.OrderByDescending(x => x.Id).ToList().ForEach(product =>
			//{
			//    listProducts.Add(new ProductVM(
			//     product.Id,
			//      product.ProName,
			//        product.Description,
			//    product.Quality,
			//    product.Price,
			//    product.PerchasePrice,
			//    product.Created,
			//     product.Image,
			//    product.CateID,
			//      cateName: _db.Categories.Find(product.CateID).CateName,

			//        product.BandID,
			//        brandName: _db.Brands.Find(product.BandID).Name
			//        ));
			//});

			int pageSize = 5;
			int pageNumber = (page ?? 1);

			return View(listProducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
		}

		public IActionResult Create()
		{
			ViewBag.listCategories = new SelectList(_db.Categories.ToList(), "Id", "CateName");
			ViewBag.listBrands = new SelectList(_db.Brands.ToList(), "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Product model)
		{
			if (ModelState.IsValid)
			{
				if (model.ImageFile != null)
				{
					model.Image = UploadImage(model.ImageFile);
				}

				_db.Products.Add(model);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		private string UploadImage(IFormFile file)
		{
			string uniqueFileName = "";
			var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/images/product");
			uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
			var filePath = Path.Combine(folderPath, uniqueFileName);
			using (FileStream fileStream = System.IO.File.Create(filePath))
			{
				file.CopyTo(fileStream);
			}
			return uniqueFileName;
		}

		public IActionResult Update(int id)
		{
			var product = _db.Products.FirstOrDefault(u => u.Id == id);

			if (product == null)
				return NotFound();
			ProductVM vm = new()
			{
				Id = product.Id,
				ProName = product.ProName,
				Image = product.Image,
				Description = product.Description,
				Price = product.Price,
				PerchasePrice = product.PerchasePrice,
				Quantity = product.Quality,
				CateID = product.CateID,
				BandID = product.BandID,
			};

			ViewBag.listCategories = new SelectList(_db.Categories.ToList(), "Id", "CateName");
			ViewBag.listBrands = new SelectList(_db.Brands.ToList(), "Id", "Name");
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(ProductVM vm, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				var product = _db.Products.FirstOrDefault(x => x.Id == vm.Id);
				if (file != null && file.Length > 0)
				{
					product.Image = UploadImage(file);
				}

				product.ProName = vm.ProName;
				product.Description = vm.Description;
				product.Price = vm.Price;
				product.PerchasePrice = vm.PerchasePrice;
				product.BandID = vm.BandID;
				product.CateID = vm.CateID;
				product.PerchasePrice = vm.PerchasePrice;

				//_db.Products.Update(model);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.listCategories = new SelectList(_db.Categories.ToList(), "Id", "CateName");
			ViewBag.listBrands = new SelectList(_db.Brands.ToList(), "Id", "Name");
			return View(vm);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var product = _db.Products.FirstOrDefault(x => x.Id == id);
			if (product != null)
			{
				_db.Products.Remove(product);
				_db.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		//public IActionResult Delete(int? id)
		//{
		//    if (id == 0 || id == null)
		//        return NotFound();
		//    var product = _db.Products.FirstOrDefault(u => u.Id == id);
		//    if (product == null)
		//        return NotFound();
		//    return PartialView("DeleteProduct", product);
		//}
		//[HttpPost]
		////[ValidateAntiForgeryToken]
		//public IActionResult Delete(Product a)
		//{
		//    _db.Products.Remove(a);
		//    _db.SaveChanges();
		//    return RedirectToAction("Index");

		//}
	}
}