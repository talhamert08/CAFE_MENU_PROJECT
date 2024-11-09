using Business.Services;
using Core.CustomExceptions;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{

    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryTableService _categoryTableService;
        private readonly IProductTableService _productTableService;
        public CategoryController(ICategoryTableService categoryTableService,IProductTableService productTableService)
        {
            _categoryTableService = categoryTableService;
            _productTableService = productTableService;
        }


        public async Task<IActionResult> Index()
        {
            var res = await _categoryTableService.GetListAsync();
            return View(res);
        }
        public async Task<IActionResult> AddCategory()
        {
            ViewData["categorys"] = await _categoryTableService.GetListAsync(x => x.ParentCategoryId == null);
            return View();
        }

        public async Task<IActionResult> AddCategoryMethod(Guid? categoryId, string name)
        {
            try
            {
                var res = await _categoryTableService.AddOrUpdateAsync(new CategoryTableDto() { ParentCategoryId = categoryId, CategoryName = name });

                if (res.CategoryName != null)
                {
                    return RedirectToAction("Success", "Result");
                }
                else
                {
                    return RedirectToAction("Error", "Result");
                }
            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }
        }

        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {

                var result = await _productTableService.GetListAsync(x => x.CategoryId == id);
                var rest = await _categoryTableService.GetListAsync(x => x.ParentCategoryId == id);

                var model = await _categoryTableService.GetByIdAsync(id);

                if (model != null && result.Count == 0 && rest.Count == 0)
                {

                    await _categoryTableService.DeleteAsync(model);
                    return RedirectToAction("Success", "Result");

                }
                else
                {
                    return RedirectToAction("Error", "Result", new { message = "This category is actively used" });
                }

            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }

        }
        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var rest = await _categoryTableService.GetListAsync();
            var own = rest.FirstOrDefault(x => x.Id == id);
            if(own != null)
            {
                rest.Remove(own);
            }
            ViewData["categorysForUpd"] = rest;
            var res = await _categoryTableService.GetByIdAsync(id);
            return View(res);
        }

        public async Task<IActionResult> UpdateCategoryMethod(CategoryTableDto categoryTableDto)
        {
            try
            {
                var res = await _categoryTableService.AddOrUpdateAsync(categoryTableDto);
                if (res != null && res.CategoryName != null)
                {
                    return RedirectToAction("Success", "Result");
                }
                else
                {
                    return RedirectToAction("Error", "Result");
                }
            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }
        }
    }
}
