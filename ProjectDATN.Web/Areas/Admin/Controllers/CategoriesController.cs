using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using X.PagedList;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly ApplicationDBContext _db;

		public CategoriesController(ApplicationDBContext db)
		{
			_db = db;
		}

		public IActionResult Index(int? page)
		{
			var listOfCategories = _db.Categories.ToList();

			int pageSize = 5;
			int pageNumber = (page ?? 1);

			return View(listOfCategories.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category a)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Add(a);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(_db.Categories);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			if (id == 0 || id == null)
				return NotFound();
			var categoryID = _db.Categories.FirstOrDefault(u => u.Id == id);
			if (categoryID == null)
				return NotFound();
			return View(categoryID);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category a)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(a);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(a);
		}

		public IActionResult Delete(int? id)
		{
			if (id == 0 || id == null)
				return NotFound();
			var categoryID = _db.Categories.FirstOrDefault(u => u.Id == id);
			if (categoryID == null)
				return NotFound();
			return PartialView("DeleteCategory", categoryID);
		}

		[HttpPost]
		public IActionResult Delete(Category a)
		{
			_db.Categories.Remove(a);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}