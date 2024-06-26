using Microsoft.AspNetCore.Mvc;

namespace Quanlybanhang.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                return View();

            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
    }
}
