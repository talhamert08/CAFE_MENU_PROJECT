using Business.Services;
using Core.CustomExceptions;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace WebUI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductTableService _productTableService;
        private readonly ICategoryTableService _categoryTableService;
        private readonly IProductPropertyTableService _productPropertyTableService;
        private readonly IPropertyTableService _propertyTableService;

        public ProductController(IProductTableService productTableService, ICategoryTableService categoryTableService, IProductPropertyTableService productPropertyTableService, IPropertyTableService propertyTableService)
        {
            _productTableService = productTableService;
            _categoryTableService = categoryTableService;
            _productPropertyTableService = productPropertyTableService;
            _propertyTableService = propertyTableService;
        }
        public async Task<IActionResult> AddProduct()
        {
            ViewData["categorysForProduct"] = await _categoryTableService.GetListAsync();
            return View();
        }

        public async Task<IActionResult> AddProductMethod(string procutName, Guid categoryId, decimal price, IFormFile photo)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedPhotos", photo.FileName);

                // Dizini kontrol et ve oluştur
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                var res = await _productTableService.AddOrUpdateAsync(new ProductTableDto() { CategoryId = categoryId, ProductName = procutName, Price = price, ImagePath = $"UploadedPhotos/{photo.FileName}" });

                if (res != null && res.ProductName != null)
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


        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var model = await _productTableService.GetByIdAsync(id);
                    if (model == null)
                    {
                        throw new Exception("Ürün bulunamadı.");
                    }
                    var res = await _productPropertyTableService.GetListAsync(x => x.ProductId == id);

                    foreach (var item in res)
                    {
                        await _productPropertyTableService.DeleteAsync(item);
                    }

                    await _productTableService.DeleteAsync(model);

                    // Eğer her şey başarılıysa transaction'ı commit et
                    transaction.Complete();
                    return RedirectToAction("Success", "Result");
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemi geri al
                    return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
                }
            }

        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            ViewData["categorysForProductUpd"] = await _categoryTableService.GetListAsync();

            return View(await _productTableService.GetByIdAsync(id));
        }

        public async Task<IActionResult> UpdateProductMethod(ProductTableDto productTableDto, IFormFile photo)
        {
            try
            {
                var model = await _productTableService.GetByIdAsync(productTableDto.Id);
                if (photo != null && $"UploadedPhotos/{photo.FileName}" != model.ImagePath)
                {


                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedPhotos", photo.FileName);

                    // Dizini kontrol et ve oluştur
                    var directory = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(fileStream);
                    }
                    productTableDto.ImagePath = $"UploadedPhotos/{photo.FileName}";
                }
                if (productTableDto.ImagePath == null)
                {
                    productTableDto.ImagePath = model.ImagePath;
                }
                var res = await _productTableService.AddOrUpdateAsync(productTableDto);

                if (res != null && res.ProductName != null)
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

        public async Task<IActionResult> AddProperty(Guid id)
        {
            var propertys = await _productPropertyTableService.GetListAsync(x => x.ProductId == id);

            var res = await _propertyTableService.GetListAsync();

            foreach (var item in propertys)
            {
                res.Remove(res.Find(x => x.Id == item.PropertyId));
            }

            ViewData["propertysForProduct"] = res;


            return View(id);
        }

        public async Task<IActionResult> AddPropertyMethod(Guid id, Guid propertyId)
        {
            var res = await _productPropertyTableService.AddOrUpdateAsync(new ProductPropertyTableDto() { PropertyId = propertyId, ProductId = id });
            if (res != null && res.ProductId != null)
            {
                return RedirectToAction("Success", "Result");
            }
            else
            {
                return RedirectToAction("Error", "Result");
            }
        }

        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            try
            {
                var res = await _productPropertyTableService.GetByIdAsync(id);
                await _productPropertyTableService.DeleteAsync(res);
                return RedirectToAction("Success", "Result");
            }
            catch (Exception ex)
            {
                // Hata durumunda işlemi geri al
                return RedirectToAction("Error", "Result", new { message = BusinessExceptionMessage.General });
            }
        }


    }
}
