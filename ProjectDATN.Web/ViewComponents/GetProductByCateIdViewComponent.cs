using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.ViewModels;

namespace ProjectDATN.Web.ViewComponents
{
	public class GetProductByCateIdViewComponent : ViewComponent
	{
		private readonly ApplicationDBContext _db;
		public GetProductByCateIdViewComponent(ApplicationDBContext db)
		{
			_db = db;
		}

		public IViewComponentResult Invoke(int id)
		{
			var products = _db.Products.Where(x => x.CateID == id).ToList();
			var listProduct = new List<ProductVM>();
			foreach (var product in products)
			{
				ProductVM vm = new()
				{
					Id = product.Id,
					ProName = product.ProName,
					Image = product.Image,
					Price = product.Price,
					CateName = _db.Categories.FirstOrDefault(c=>c.Id == product.CateID).CateName,
					PerchasePrice = product.PerchasePrice

				};
				listProduct.Add(vm);
			}
			return View(listProduct);
		}
	}
}
