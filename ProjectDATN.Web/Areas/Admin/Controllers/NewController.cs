//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using ProjectDATN.Data.EF;
//using ProjectDATN.Data.Entities;
//using ProjectDATN.Data.ViewModels;
//using X.PagedList;

//namespace ProjectDATN.Web.Areas.Admin.Controllers
//{
//    public class NewController : Controller
//    {
//        private readonly ApplicationDBContext _db;

//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly UserManager<AppUser> _userManager;

//        public NewController(ApplicationDBContext context, IWebHostEnvironment webHostEnviroment, UserManager<AppUser> userManager)
//        {
//            _db = context;

//            _webHostEnvironment = webHostEnviroment;
//            _userManager = userManager;
//        }

//        public IActionResult Index(int? page)

//        {
//            var listProducts = new List<NewVM>();
//            _db.News.OrderByDescending(x => x.Id).ToList().ForEach(new =>
//            {
//                ListNew.Add(new NewVM(

//            });

//            int pageSize = 5;
//            int pageNumber = (page ?? 1);

//            return View(listProducts.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize));

//        }
//    }
//}