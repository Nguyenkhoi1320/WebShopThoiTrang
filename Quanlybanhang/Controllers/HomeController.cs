using Microsoft.AspNetCore.Mvc;
using models;
using service;
using System.Diagnostics;
using System.Text.Json;

namespace Quanlybanhang.Controllers
{
    public class HomeController : Controller
    {
        private readonly san_phamService san_PhamService;
        private readonly don_hangService don_HangService;
        private readonly chi_tiet_don_hangService chi_Tiet_Don_HangService;
        public HomeController(san_phamService san_PhamServices, don_hangService don_HangServices, chi_tiet_don_hangService chi_Tiet_Don_HangServices)
        {
            san_PhamService = san_PhamServices;
            don_HangService = don_HangServices;
            chi_Tiet_Don_HangService = chi_Tiet_Don_HangServices;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<san_pham> listsanpham = san_PhamService.GetAllSanPham();
                List<modeldata> modeldatas = new List<modeldata>();
                foreach(var sanpham in listsanpham)
                {
                    modeldata modeldata = new modeldata()
                    {
                        san_Pham = sanpham,
                    };
                    modeldatas.Add(modeldata);
                }
              
                return View(modeldatas);
            }
            else
            {
                List<san_pham> listsanpham = san_PhamService.GetAllSanPham();

                List<modeldata> modeldatas = new List<modeldata>();

                foreach (var sanpham in listsanpham)
                {
                    modeldata modeldata = new modeldata()
                    {
                        san_Pham = sanpham,
                    };
                    modeldatas.Add(modeldata);
                }
                return View(modeldatas);
            }
        }
        public IActionResult signup()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }
        public IActionResult changepassword()
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