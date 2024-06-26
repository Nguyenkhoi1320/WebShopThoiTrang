using Microsoft.AspNetCore.Mvc;
using models;
using service;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Quanlybanhang.Controllers
{
    public class GiamGiaController : Controller
    {
        private readonly quy_dinh_giam_giaService quy_Dinh_Giam_GiaService;
        private readonly ma_giam_giaService ma_Giam_GiaService;
        public GiamGiaController(quy_dinh_giam_giaService quy_Dinh_Giam_GiaService, ma_giam_giaService ma_Giam_GiaService)
        {
            this.quy_Dinh_Giam_GiaService = quy_Dinh_Giam_GiaService;
            this.ma_Giam_GiaService = ma_Giam_GiaService;
        }

        public IActionResult quydinhgiamgia()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<quy_dinh_giam_gia> listquydinhgiamgia = quy_Dinh_Giam_GiaService.LayDanhSachQuyDinh();
                modeldata modeldata = new modeldata()
                {
                    quy_Dinh_Giam_Gialist = listquydinhgiamgia,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");

            }
        }
        public IActionResult addquydinh()
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
        public IActionResult addquydinhgiamgia(quy_dinh_giam_gia quy_Dinh_Giam_Gia)
        
            {
                if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
                {
                    int id = HttpContext.Session.GetInt32("id").Value;
                    string hovaten = HttpContext.Session.GetString("hovaten");
                    ViewData["id"] = id;
                    ViewData["hovaten"] = hovaten;
                    quy_Dinh_Giam_GiaService.ThemQuyDinh(quy_Dinh_Giam_Gia);
                    return RedirectToAction("quydinhgiamgia", "giamgia");
                }
                else
                {
                    return RedirectToAction("login", "Home");

                }
            }
        
        public IActionResult xoaquydinh(int id)
        {
            quy_Dinh_Giam_GiaService.XoaQuyDinh(id);
            return RedirectToAction("quydinhgiamgia", "giamgia");
        }
        public IActionResult updatequydinh(int id)
            {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int ids = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = ids;
                ViewData["hovaten"] = hovaten;
                quy_dinh_giam_gia quy_Dinh_Giam_Gia = quy_Dinh_Giam_GiaService.LayQuyDinhTheoID(id);
                modeldata modeldata = new modeldata()
                {
                    quy_Dinh_Giam_Gia = quy_Dinh_Giam_Gia,
                };
                return View(modeldata);
            }
            else {
                    return RedirectToAction("login", "Home");
            }

        }
        public IActionResult updatequydinhgiamgia(quy_dinh_giam_gia quy_Dinh_Giam_Gia)

        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int ids = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = ids;
                ViewData["hovaten"] = hovaten;
                quy_Dinh_Giam_GiaService.SuaQuyDinh(quy_Dinh_Giam_Gia);
                return RedirectToAction("quydinhgiamgia", "giamgia");
            }
            else
            {
                return RedirectToAction("login", "Home");

            }
        }
        public IActionResult magiamgia()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<ma_giam_gia> listmagiamgia = ma_Giam_GiaService.LayDanhSachMaGiamGia();
                modeldata modeldata = new modeldata()
                {
                    ma_Giam_Gialist = listmagiamgia,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult addmagiamgia()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<quy_dinh_giam_gia> listquydinhgiamgia = quy_Dinh_Giam_GiaService.LayDanhSachQuyDinh();
                modeldata modeldata = new modeldata()
                {
                    quy_Dinh_Giam_Gialist = listquydinhgiamgia,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult addmagiamgias(ma_giam_gia ma_Giam_Gia, int quydinggiamgia_id)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                quy_dinh_giam_gia quy_Dinh_Giam_Gia = quy_Dinh_Giam_GiaService.LayQuyDinhTheoID(quydinggiamgia_id);
                ma_Giam_Gia.phantramgiamgia = quy_Dinh_Giam_Gia.phantramgiamgia;
                ma_Giam_Gia.ngaybatdau = DateTime.Now;
                ma_Giam_Gia.solandasudung = 0;
                ma_Giam_GiaService.ThemMaGiamGia(ma_Giam_Gia);
                return RedirectToAction("magiamgia", "giamgia");
            }
            else
            {
                return RedirectToAction("login", "Home");

            }
        }
        public IActionResult xoamagiamgia(int id)
        {
            ma_Giam_GiaService.XoaMaGiamGia(id);
            return RedirectToAction("magiamgia", "giamgia");
        }
    }
}
