using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class SizeController : Controller
    {
        private readonly SizeService _sizeService;
        private readonly san_phamService san_PhamService;
        public SizeController(SizeService sizeService, san_phamService san_PhamService)
        {
            _sizeService = sizeService;
            this.san_PhamService = san_PhamService;
        }
        public async Task<IActionResult> Index()
        {
            List<sizes> sizelist = await _sizeService.GetAllSizesAsync();
            modeldata modeldata = new modeldata
            {
                sizesss = sizelist,
            };
            return View(modeldata);
        }
        public IActionResult AddSize()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<san_pham> san_Phams = san_PhamService.GetAllSanPham();
                modeldata modeldata = new modeldata()
                {
                    san_Phamlist = san_Phams
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
            
        }
        public async Task< IActionResult> AddSizes(sizes size, int sanPhamId)
        {
            size.sanPhamId = sanPhamId;
         await  _sizeService.CreateSizeAsync(size);

            return RedirectToAction("index", "size");
        }
        public async Task <IActionResult> Updatesizebyid(int id)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int ids = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = ids;
                ViewData["hovaten"] = hovaten;
                sizes size = await _sizeService.GetSizeByIdAsync(id);
                List<san_pham> san_Phams = san_PhamService.GetAllSanPham();

                modeldata modeldata = new modeldata()
                {
                    Sizes = size,
                    san_Phamlist = san_Phams
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            }

        }
        public async Task<IActionResult> UpdateSize(sizes size, int sanPhamId)
        {
            size.sanPhamId = sanPhamId;
            await _sizeService.UpdateSizeAsync(size);

            return RedirectToAction("index", "size");
        }
    }

}
