using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class ProductController : Controller
    { private readonly san_phamService san_PhamService;
        private readonly NhaCungCapService nhaCungCapService;


        public ProductController(san_phamService san_PhamService,NhaCungCapService nha_CungCapService)
        {
            this.san_PhamService = san_PhamService;
            this.nhaCungCapService = nha_CungCapService;

        }  
        public IActionResult ProductDetail(int id)
        {
            string hovaten = HttpContext.Session.GetString("hovaten");
            ViewData["hovaten"] = hovaten;
            san_pham sp = san_PhamService.GetSanPhamById(id);
            List<modeldata> modeldatas = new List<modeldata>();

            modeldata modeldata = new modeldata()
            {
                san_Pham = sp
            };
            modeldatas.Add(modeldata);
            return View(modeldatas);
        } 
        public IActionResult ListProduct()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<san_pham> san_phamlist = san_PhamService.GetAllSanPham();
                      modeldata modeldata = new modeldata()
                      {
                          san_Phamlist = san_phamlist,
                      };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult AddProduct()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<nha_cung_cap> nha_Cung_CapList = nhaCungCapService.GetAllNhaCungCap();
                modeldata modeldata = new modeldata()
                {
                    nha_Cung_CapList = nha_Cung_CapList,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
      
        public IActionResult AddProducts(san_pham san_Pham, int nhacungcap_id) {
            san_Pham.nhacungcap_id = nhacungcap_id;
            san_PhamService.AddSanPham(san_Pham);
            return RedirectToAction("ListProduct", "product");
        }
        public IActionResult UpdateProductbyID(int id)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int ids = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = ids;
                ViewData["hovaten"] = hovaten;
               san_pham san_Pham = san_PhamService.GetSanPhamById(id);
                List<nha_cung_cap> nha_Cung_CapList = nhaCungCapService.GetAllNhaCungCap();

                modeldata modeldata = new modeldata()
                {
                    san_Pham = san_Pham,
                    nha_Cung_CapList = nha_Cung_CapList
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }

        }
        public IActionResult Xoasanpham(int id)
        {
            san_PhamService.DeleteSanPham(id);
            return RedirectToAction("listproduct", "product");
        }
        public IActionResult UpdateProduct(san_pham san_Pham, int nhacungcap_id)
        {
            san_Pham.nhacungcap_id = nhacungcap_id;
            san_PhamService.UpdateSanPham(san_Pham);
            return RedirectToAction("ListProduct", "product");
        }
    }
}
