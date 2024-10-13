using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly danh_mucService danh_MucService;
        public DanhMucController(danh_mucService danh_MucService)
        {
            this.danh_MucService = danh_MucService;
        }
        public IActionResult Listcategory()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<danh_muc> danh_Mucs = danh_MucService.GetAll();
                modeldata modeldata = new modeldata()
                {
                    danh_Mucs = danh_Mucs,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");

            }
        }
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }
        public IActionResult AddCategorys(danh_muc danh_Muc)
        {
            danh_MucService.Create(danh_Muc);
            return RedirectToAction("Listcategory", "danhmuc");
        }
    }
}
