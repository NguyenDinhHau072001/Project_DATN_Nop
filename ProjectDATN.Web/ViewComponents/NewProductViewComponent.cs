using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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

				//var products = _db.Products.LeftJoin(_db.Promotions.DefaultIfEmpty(), p => p.Id, pr => pr.ProId, (p, pr) => new
				//{
				//	p.Id,
				//	p.ProName,
				//	p.Image,
				//	p.Price,
				//	p.PerchasePrice,
				//	p.CateID,
				//	pr.Promo,
				//}).OrderByDescending(p=>p.Id).Take(30).ToList();

				//var products = from p in _db.Products
				//			   join pr in _db.Promotions
				//				on p.Id equals pr.ProId into pp
				//			   select new
				//			   {
				//				   p.Id,
				//				   p.ProName,
				//				   p.Image,
				//				   p.Price,
				//				   p.PerchasePrice,
				//				   p.CateID,

				//			   };
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
						//Promotion = product.Promo,
						PerchasePrice = product.PerchasePrice

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
                        PerchasePrice = product.PerchasePrice

                    };
                    listSaleProduct.Add(vm);
                }
                return View("TopSale", listSaleProduct);
            }

			return View();
			
		}
	}
}
