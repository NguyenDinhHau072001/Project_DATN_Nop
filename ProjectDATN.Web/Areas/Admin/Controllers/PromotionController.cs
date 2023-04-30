using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.Entities;
using ProjectDATN.Data.ViewModels;
using System.Collections.Generic;
using X.PagedList;

namespace ProjectDATN.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PromotionController : Controller
	{
		private readonly ApplicationDBContext _db;
		private readonly IWebHostEnvironment _environment;

		public PromotionController(ApplicationDBContext db, IWebHostEnvironment environment)
		{
			_db = db;
			_environment = environment;
		}

		public IActionResult Index(int? page, string search)
		{
           
           
           
            var listOfPromotions = new List<PrmotionVM>();
			List<Product> productIDs;

            if (search != null && search != "")
			{
                productIDs = _db.Products.Where(p => p.ProName.Contains(search)).ToList();
            }
			else
			{
				productIDs = null;
			}
			if(productIDs != null)
			{
                foreach (var item in productIDs)
                {
                    _db.Promotions.Where(x=>x.ProId == item.Id).ToList().ForEach(promotion =>
                    {
                        listOfPromotions.Add(new PrmotionVM
                        (
                             promotion.Id,
                             promotion.ProId,
                                proName: _db.Products.Find(promotion.ProId).ProName,
                                proImage: _db.Products.Find(promotion.ProId).Image,

                             promotion.Promo,
                             promotion.Created,
                             promotion.Finish
                        ));
                    });
                }
            }
			else
			{
                _db.Promotions.OrderByDescending(x => x.Id).ToList().ForEach(promotion =>
                {
                    listOfPromotions.Add(new PrmotionVM
                    (
                         promotion.Id,
                         promotion.ProId,
                            proName: _db.Products.Find(promotion.ProId).ProName,
                            proImage: _db.Products.Find(promotion.ProId).Image,

                         promotion.Promo,
                         promotion.Created,
                         promotion.Finish
                    ));
                });
            }
			
			int pageSize = 5;
			int pageNumber = (page ?? 1);

			return View(listOfPromotions.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));
		}

		public IActionResult Create()
		{
			ViewBag.Products = new SelectList(_db.Products.ToList(), "Id", "ProName");
			ViewBag.Image = _db.Products.Join(_db.Promotions, x => x.Id, p => p.ProId, (x, p) => x.Image);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PromotionV1 a)
		{
			if (ModelState.IsValid)
			{
				_db.Promotions.Add(a);
				_db.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(_db.Promotions);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			if (id == 0 || id == null)
				return NotFound();
			ViewBag.Products = new SelectList(_db.Products.ToList(), "Id", "ProName");
			var PromotionId = _db.Promotions.FirstOrDefault(u => u.Id == id);
			if (PromotionId == null)
				return NotFound();
			return View(PromotionId);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(PromotionV1 a)
		{
			if (ModelState.IsValid)
			{
				_db.Promotions.Update(a);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(a);
		}

		public IActionResult Delete(int? id)
		{
			if (id == 0 || id == null)
				return NotFound();
			var PromotionId = _db.Promotions.FirstOrDefault(u => u.Id == id);
			if (PromotionId == null)
				return NotFound();
			return View(PromotionId);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var a = _db.Promotions.Find(id);
			if (a == null)
			{
				return NotFound();
			}
			_db.Promotions.Remove(a);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}