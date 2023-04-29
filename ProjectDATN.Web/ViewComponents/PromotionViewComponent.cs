using Microsoft.AspNetCore.Mvc;
using ProjectDATN.Data.EF;
using ProjectDATN.Data.ViewModels;
using ProjectDATN.Data.Enums;
namespace ProjectDATN.Web.ViewComponents
{
    public class PromotionViewComponent:ViewComponent
    {
        private readonly ApplicationDBContext _db;
        public PromotionViewComponent(ApplicationDBContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            //var today = DateTime.Now;
            var promotionns = _db.Promotions.Where(x=> x.Created <= DateTime.Now && x.Finish >= DateTime.Now).ToList();
            var listPromotions = new List<PrmotionVM>();
            foreach (var promotion in promotionns)
            {
                PrmotionVM vm = new()
                {
                    Id = promotion.Id,
                    ProId = promotion.Id,
                    ProImage = _db.Products.FirstOrDefault(x => x.Id == promotion.ProId)?.Image
                };
                listPromotions.Add(vm);
            }
            return View(listPromotions);
        }
    }
}
