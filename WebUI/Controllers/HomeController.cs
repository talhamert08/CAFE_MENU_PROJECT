using Business.Services;
using DataAccess.Concrete.SQL_EntityFrameWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebUI.Models;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductTableService _productTableService;
        private readonly ICategoryTableService _categoryTableService;
        public HomeController(ILogger<HomeController> logger, IProductTableService productTableService, ICategoryTableService categoryTableService)
        {
            _logger = logger;
            _productTableService = productTableService;
            _categoryTableService = categoryTableService;
        }

        
        public async Task<IActionResult> Index(Guid? id)
        {

            var res = await _categoryTableService.GetListAsync(x => x.ParentCategoryId == null);

            ViewData["categoryForSelect"] = res;

            var result = await _productTableService.GetListAsyncThenInclude(includes: query => query.Include(x => x.Category).Include(x => x.Properties.Where(y => y.IsDeleted == false)).ThenInclude(p => p.Property));
            if (id.HasValue)
            {
                var rest = await _categoryTableService.GetListAsync(x => x.ParentCategoryId == id);

                var products = result.Where(x => x.CategoryId == id).ToList();

                foreach (var item in rest)
                {
                    products.AddRange(result.Where(x => x.CategoryId == item.Id).ToList());
                }

                ViewData["products"] = products;
                ViewData["selectedId"] = id;
            }
            else
            {
                ViewData["selectedId"] = null;
                ViewData["products"] = result;
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
