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
        private readonly NhaCungCapService nhaCungCapService;
        private readonly danh_mucService danh_MucService;
        public HomeController(san_phamService san_PhamServices,
            don_hangService don_HangServices, chi_tiet_don_hangService chi_Tiet_Don_HangServices, NhaCungCapService nhaCungCapService, danh_mucService danh_MucService)
        {
            san_PhamService = san_PhamServices;
            don_HangService = don_HangServices;
            chi_Tiet_Don_HangService = chi_Tiet_Don_HangServices;
            this.nhaCungCapService = nhaCungCapService;
            this.danh_MucService = danh_MucService;

        }

        public IActionResult Index(string name, int idnhancungcap, int iddanhmuc)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
            }

            // Nếu người dùng có nhập từ khóa tìm kiếm, thực hiện tìm kiếm
            List<san_pham> listsanpham = new List<san_pham>();
            if (!string.IsNullOrEmpty(name))
            {
                listsanpham = san_PhamService.GetAllSanPham()
                .Where(sp => string.IsNullOrEmpty(name) || sp.tensanpham.ToLower().Contains(name.ToLower()))
                .ToList();


            }
            else
            {
                listsanpham = san_PhamService.GetAllSanPham(); // Hiển thị tất cả sản phẩm nếu không có tìm kiếm
            }

            if (idnhancungcap > 0)
            {
                listsanpham = san_PhamService.GetAllSanPham().Where(sp => sp.nhacungcap_id == idnhancungcap).ToList();
            }
            if (iddanhmuc > 0)
            {
                listsanpham = san_PhamService.GetAllSanPham().Where(sp => sp.danhmuc_id == iddanhmuc).ToList();
            }

            List<nha_cung_cap> nha_Cung_Caps = nhaCungCapService.GetAllNhaCungCap();
            List<danh_muc> danh_Mucs = danh_MucService.GetAll();

            List<modeldata> modeldatas = new List<modeldata>();

            
                modeldata modeldata = new modeldata()
                {
                    san_Phamlist = listsanpham,
                    nha_Cung_CapList = nha_Cung_Caps,
                    danh_Mucs = danh_Mucs
                };
            modeldatas.Add(modeldata);
            

            return View(modeldatas);
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