using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class BrandController : Controller
    {
        private readonly NhaCungCapService nhaCungCapService;
        public BrandController(NhaCungCapService nhaCungCapService)
        {
            this.nhaCungCapService = nhaCungCapService;
        }
        public IActionResult ListBrand()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<nha_cung_cap> listnhacungcap = nhaCungCapService.GetAllNhaCungCap();
                modeldata modeldata = new modeldata()
                {
                    nha_Cung_CapList = listnhacungcap,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");

            }
        }
        public IActionResult AddBrand() {
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
        public IActionResult AddBrands(nha_cung_cap nha_Cung_Cap)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                nhaCungCapService.Add(nha_Cung_Cap);
                return RedirectToAction("listbrand", "brand");
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult XoaBrand(int id)
        {
            nhaCungCapService.Delete(id);
            return RedirectToAction("listbrand", "brand");
        }
        public IActionResult Updatebrand(int id)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int ids = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = ids;
                ViewData["hovaten"] = hovaten;
                nha_cung_cap nha_Cung_Cap = nhaCungCapService.GetById(id);
                modeldata modeldata = new modeldata
                {
                    nha_Cung_Cap = nha_Cung_Cap,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }

        }
        public IActionResult UpdateBrands(nha_cung_cap nha_Cung_Cap)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                nhaCungCapService.Update(nha_Cung_Cap);
                return RedirectToAction("listbrand", "brand");
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
    }

}
