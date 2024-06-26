using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class ChitietdonhangController : Controller
    {
        private readonly chi_tiet_don_hangService chi_Tiet_Don_HangService;
        private readonly san_phamService san_phamService;
        public ChitietdonhangController(chi_tiet_don_hangService chi_Tiet_Don_HangService, san_phamService san_phamService)
        {
            this.chi_Tiet_Don_HangService = chi_Tiet_Don_HangService;
            this.san_phamService = san_phamService;
        }

        public IActionResult Index(int donHangId)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<chi_tiet_don_hang> chi_Tiet_Don_Hangs = chi_Tiet_Don_HangService.LayDanhSachChiTietDonHangTheoDonHangId(donHangId);
                List<modeldata> modeldatas = new List<modeldata>();
                foreach (var item in chi_Tiet_Don_Hangs)
                {
                    san_pham san_Pham = san_phamService.GetSanPhamById(item.sanpham_id);
                    modeldata data = new modeldata
                    {
                        chiTietDonHang = item,
                        san_Pham = san_Pham,
                    };
                    modeldatas.Add(data);
                }
                return View(modeldatas);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult Chitietdonhangkh(int donHangId)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<chi_tiet_don_hang> chi_Tiet_Don_Hangs = chi_Tiet_Don_HangService.LayDanhSachChiTietDonHangTheoDonHangId(donHangId);
                List<modeldata> modeldatas = new List<modeldata>();
                foreach (var item in chi_Tiet_Don_Hangs)
                {
                    san_pham san_Pham = san_phamService.GetSanPhamById(item.sanpham_id);
                    modeldata data = new modeldata
                    {
                        chiTietDonHang = item,
                        san_Pham = san_Pham,
                    };
                    modeldatas.Add(data);
                }
                return View(modeldatas);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
    }
}
