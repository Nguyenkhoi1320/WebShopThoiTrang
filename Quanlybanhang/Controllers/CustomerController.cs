using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class CustomerController : Controller
    {
        private readonly don_hangService don_HangService;
        private readonly khach_hangService khach_HangService;
        private readonly san_phamService san_PhamService;
        private readonly chi_tiet_don_hangService chi_Tiet_Don_HangService;
        public CustomerController(don_hangService don_HangService,
            khach_hangService khach_HangService,
            san_phamService san_PhamService,
            chi_tiet_don_hangService chi_Tiet_Don_HangService)
        {
            this.don_HangService = don_HangService;
            this.khach_HangService = khach_HangService;
            this.san_PhamService = san_PhamService;
            this.chi_Tiet_Don_HangService = chi_Tiet_Don_HangService;
        }
        public IActionResult customerdetails()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<don_hang> listdonhang = don_HangService.LayDanhSachDonHangtheoidkhachhang(id);
                List<modeldata> modeldataslist = new List<modeldata>();
                foreach (var donhang in listdonhang)
                {
                    List<chi_tiet_don_hang> chi_Tiet_Don_Hangs = chi_Tiet_Don_HangService.LayDanhSachChiTietDonHangTheoDonHangId(donhang.id);
                    foreach (var chitietdonhang in chi_Tiet_Don_Hangs)
                    {
                        san_pham san_Pham = san_PhamService.GetSanPhamById(chitietdonhang.sanpham_id);
                        modeldata modeldata = new modeldata
                        {
                            DonHangs = donhang,
                            chiTietDonHang = chitietdonhang,
                            san_Pham = san_Pham,
                        };
                        modeldataslist.Add(modeldata);
                    }
                }
                return View(modeldataslist);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
    }
}