using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.ViewModels;

namespace ProjectDATN.Web.ViewComponents
{
	public class NewProductViewComponent : ViewComponent
	{
		private readonly ApplicationDBContext _db;
		public NewProductViewComponent(ApplicationDBContext db)
		{
			_db = db;
		}

		public IViewComponentResult Invoke(string name)
		{
			if(name=="NewProducts")
			{
				var products = _db.Products.OrderByDescending(x => x.Id).Take(30).ToList();
				var listProduct = new List<ProductVM>();
				foreach (var product in products)
				{
					ProductVM vm = new()
					{
						Id = product.Id,
						ProName = product.ProName,
						Image = product.Image,
						Price = product.Price,
						CateName = _db.Categories.FirstOrDefault(x => x.Id == product.CateID).CateName,
						PerchasePrice = product.Price

					};
					listProduct.Add(vm);
				}
				return View("NewProducts", listProduct);
			}

			else if (name == "TopSale")
			{
                var saleproducts = _db.Products.OrderByDescending(x => x.SaleQuatity).Take(30).ToList();
                var listSaleProduct = new List<ProductVM>();
                foreach (var product in saleproducts)
                {
                    ProductVM vm = new()
                    {
                        Id = product.Id,
                        ProName = product.ProName,
                        Image = product.Image,
                        Price = product.Price,
                        CateName = _db.Categories.FirstOrDefault(x => x.Id == product.CateID).CateName,
                        PerchasePrice = product.Price

                    };
                    listSaleProduct.Add(vm);
                }
                return View("TopSale", listSaleProduct);
            }

			return View();
			
		}
	}
}
