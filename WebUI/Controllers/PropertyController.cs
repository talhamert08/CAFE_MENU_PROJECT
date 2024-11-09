using Business.Services;
using Core.CustomExceptions;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        private readonly IPropertyTableService _propertyTableService;
        private readonly IProductPropertyTableService _productPropertyTableService;
        public PropertyController(IPropertyTableService propertyTableService, IProductPropertyTableService productPropertyTableService)
        {
            _propertyTableService = propertyTableService;
            _productPropertyTableService = productPropertyTableService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["propertys"] = await _propertyTableService.GetListAsync();
            return View();
        }


        public IActionResult AddProperty()
        {
            return View();
        }

        public async Task<IActionResult> AddPropertyMethod(string key, string value)
        {
            try
            {
                var res = await _propertyTableService.AddOrUpdateAsync(new PropertyTableDto() { Key = key, Value = value });
                if (res != null && res.Value != null)
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

        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            try
            {

                var result = await _productPropertyTableService.GetListAsync(x => x.PropertyId == id);
                var model = await _propertyTableService.GetByIdAsync(id);

                if (model != null && result.Count == 0)
                {

                    await _propertyTableService.DeleteAsync(model);
                    return RedirectToAction("Success", "Result");

                }
                else
                {
                    return RedirectToAction("Error", "Result", new { message = "This feature is actively used" });
                }

            }
            catch
            {
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }

        }
        public async Task<IActionResult> UpdateProperty(Guid id)
        {
            var res = await _propertyTableService.GetByIdAsync(id);
            return View(res);
        }

        public async Task<IActionResult> UpdatePropertyMethod(PropertyTableDto propertyTableDto)
        {
            try
            {
                var res = await _propertyTableService.AddOrUpdateAsync(propertyTableDto);
                if (res != null && res.Value != null)
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
