using Microsoft.AspNetCore.Mvc;
using WebUI.Api_Manager;

namespace WebUI.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly Api_Service_Manager _api_Service_Manager;
        public ExchangeController(Api_Service_Manager api_Service_Manager)
        {
            _api_Service_Manager = api_Service_Manager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _api_Service_Manager.GetExchangeRate());
        }
    }
}
