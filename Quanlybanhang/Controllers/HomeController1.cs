using Microsoft.AspNetCore.Mvc;

namespace Quanlybanhang.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
