using Microsoft.AspNetCore.Mvc;
using models;
using PagedList;
using service;

namespace Quanlybanhang.Controllers
{
    public class DonhangController : Controller
    {
        private readonly don_hangService don_HangService;
        private readonly khach_hangService khach_HangService;
        private readonly chi_tiet_don_hangService chi_Tiet_Don_HangService;
        private readonly san_phamService san_PhamService;
        public DonhangController(don_hangService don_HangService, khach_hangService khach_HangService, chi_tiet_don_hangService chi_Tiet_Don_HangService, san_phamService san_PhamService)
        {
            this.don_HangService = don_HangService;
            this.khach_HangService = khach_HangService;
            this.chi_Tiet_Don_HangService = chi_Tiet_Don_HangService;
            this.san_PhamService = san_PhamService;
        }
        public IActionResult XacNhan(int donHangId)
        {
            don_hang dh = don_HangService.GetById(donHangId);
            dh.trangthai = "đã thanh toán";
            don_HangService.Update(dh);

            return RedirectToAction("index", "donhang");

        }
        public IActionResult Index(int? sotrang)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<don_hang> listdonhang = don_HangService.LayDanhSachDonHang().OrderByDescending(dh => dh.ngaytao).ToList();
                int soluong = listdonhang.Count();
                int sptrangs = sotrang ?? 1;
                List<modeldata> modeldataslist = new List<modeldata>();
                PagedList.IPagedList<don_hang> palistdonahng = listdonhang.ToPagedList(sptrangs, soluong);
                foreach (var donhang in palistdonahng)
                {
                    if (donhang.trangthai == "đã thanh toán")
                    {
                        khach_hang khach_Hang = khach_HangService.LayThongTinKhachHangTheoID(donhang.khachhang_id);
                        if (khach_Hang != null)
                        {
                            modeldata modeldata = new modeldata
                            {
                                DonHangs = donhang,
                                khach_hang = khach_Hang,
                            };
                            modeldataslist.Add(modeldata);
                        }
                    }
                    else
                    {
                        /// trạng thái không thanh toán
                    }
                }
                return View(modeldataslist);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult Huydon(int? sotrang)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<don_hang> listdonhang = don_HangService.LayDanhSachDonHang();
                int soluong = 0;
                foreach (var donhang in listdonhang)
                {
                    donhang.id = soluong;
                    soluong++;
                }
                List<modeldata> modeldataslist = new List<modeldata>();
                PagedList.IPagedList<don_hang> palistdonahng = listdonhang.ToPagedList(sotrang ?? 1, soluong);
                foreach (var donhang in palistdonahng)
                {
                    if (donhang.trangthai == "đã huỷ")
                    {
                        khach_hang khach_Hang = khach_HangService.LayThongTinKhachHangTheoID(donhang.khachhang_id);
                        if (khach_Hang != null)
                        {
                            modeldata modeldata = new modeldata
                            {
                                DonHangs = donhang,
                                khach_hang = khach_Hang,
                            };
                            modeldataslist.Add(modeldata);
                        }
                    }
                    else
                    {
                        /// trạng thái không thanh toán
                    }
                }
                return View(modeldataslist);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }

        public IActionResult orderdetails()
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
                    khach_hang khach_Hang = khach_HangService.LayThongTinKhachHangTheoID(donhang.khachhang_id);
                    foreach (var chitietdonhang in chi_Tiet_Don_Hangs)
                    {
                        san_pham san_Pham = san_PhamService.GetSanPhamById(chitietdonhang.sanpham_id);
                        modeldata modeldata = new modeldata
                        {
                            DonHangs = donhang,
                            chiTietDonHang = chitietdonhang,
                            khach_hang = khach_Hang,
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
        public IActionResult Donhangkh()
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
                    khach_hang khach_Hang = khach_HangService.LayThongTinKhachHangTheoID(donhang.khachhang_id);
                    modeldata modeldata = new modeldata
                    {
                        DonHangs = donhang,
                        khach_hang = khach_Hang,
                    };
                    modeldataslist.Add(modeldata);
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