using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;

namespace ProjectDATN.Web.ViewComponents
{

	public class ViewCategoryAndBrandViewComponent : ViewComponent
	{
		ApplicationDBContext _db;
		public ViewCategoryAndBrandViewComponent(ApplicationDBContext db)
		{
			_db = db;
		}

		public IViewComponentResult Invoke(string name)
		{
			if (name == "Category")
			{
				var listCategory = _db.Categories.OrderBy(x=>x.Id).Take(6).ToList();
				return View(listCategory);
			}
			else if(name== "Brand")
			{
				var listBrand = _db.Brands.OrderByDescending(x => x.Id).Take(6).ToList();
				return View(listBrand);
			}
			return View();
		}
	}
}
