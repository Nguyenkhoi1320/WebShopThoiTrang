using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using models;
using service;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Quanlybanhang.Controllers
{
    public class CartController : Controller
    {
        private readonly ShoppingCartService shoppingCartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly don_hangService don_HangService;
        private readonly chi_tiet_don_hangService chi_Tiet_Don_HangService;
        private readonly quy_dinh_giam_giaService quy_Dinh_Giam_GiaService;
        private readonly ma_giam_giaService ma_Giam_GiaService;
        private readonly thong_tin_van_chuyenService thong_Tin_Van_ChuyenService;
        private readonly khach_hangService khach_hangService;
        private readonly su_dung_ma_giam_giaService su_Dung_Ma_Giam_GiaService;
        private readonly lich_su_thanh_toanService lich_Su_Thanh_ToanService;
        private readonly san_phamService san_PhamService;
        public CartController(ShoppingCartService shoppingCartServices,
            IHttpContextAccessor httpContextAccessor,
            don_hangService don_HangServices,
            chi_tiet_don_hangService chi_Tiet_Don_HangServices,
            quy_dinh_giam_giaService quy_Dinh_Giam_GiaService,
            ma_giam_giaService ma_Giam_GiaService,
            thong_tin_van_chuyenService thong_Tin_Van_ChuyenService,
            khach_hangService khach_HangService, su_dung_ma_giam_giaService su_Dung_Ma_Giam_GiaService,
            lich_su_thanh_toanService lich_Su_Thanh_ToanService, san_phamService san_PhamService)
        {
            shoppingCartService = shoppingCartServices;
            _httpContextAccessor = httpContextAccessor;
            don_HangService = don_HangServices;
            chi_Tiet_Don_HangService = chi_Tiet_Don_HangServices;
            this.quy_Dinh_Giam_GiaService = quy_Dinh_Giam_GiaService;
            this.ma_Giam_GiaService = ma_Giam_GiaService;
            this.thong_Tin_Van_ChuyenService = thong_Tin_Van_ChuyenService;
            this.khach_hangService = khach_HangService;
            this.su_Dung_Ma_Giam_GiaService = su_Dung_Ma_Giam_GiaService;
            this.lich_Su_Thanh_ToanService = lich_Su_Thanh_ToanService;
            this.san_PhamService = san_PhamService;
        }

        public IActionResult Index()
        {
            string hovaten = HttpContext.Session.GetString("hovaten");
            ViewData["hovaten"] = hovaten;
            return View();
        }
        [HttpGet]
        public IActionResult GetCartItemsJson()
        {
            List<CartItem> cartItems = GetCartItems();
            return Json(cartItems);
        }
        private List<CartItem> GetCartItems()
        {
            var cartItemsJson = _httpContextAccessor.HttpContext.Session.GetString("CartItems");

            List<CartItem> cartItemList = new List<CartItem>();

            if (!string.IsNullOrEmpty(cartItemsJson))
            {
                cartItemList = JsonSerializer.Deserialize<List<CartItem>>(cartItemsJson);
            }

            return cartItemList;
        }
        //// hiển thị giá tiền khi có mã giảm giá
        public IActionResult duocgiamgia(string magiamgia)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                if (magiamgia != null)
                {
                    ma_giam_gia ma_Giam_Gia = ma_Giam_GiaService.GetMaGiamGiaByMaGiamGia(magiamgia);
                    if (ma_Giam_Gia != null)
                    {
                        if (ma_Giam_Gia.solandasudung < ma_Giam_Gia.solansudungtoida || DateTime.Now.Day > ma_Giam_Gia.ngayketthuc.Day)
                        {
                            int khachhangsudungmagiamgia = ma_Giam_GiaService.countkhachhangsu_dung_ma_giam_gia(id, ma_Giam_Gia.id);
                            if (khachhangsudungmagiamgia == 0)
                            {
                                List<CartItem> cartItems = GetCartItems();
                                Decimal tongtien = 0;
                                foreach (var cart in cartItems)
                                {
                                    tongtien += cart.productPrice;
                                }
                                Decimal tongtienduocgiamgia = tongtien * (1 - (ma_Giam_Gia.phantramgiamgia / 100));
                                return Json(new { discountAmount = tongtienduocgiamgia, totalAmount = tongtien });
                            }
                            else
                            {
                                ////
                            }
                        }
                        else
                        {
                            ////
                        }
                    }
                    else
                    {
                        ////
                    }
                }
                else
                {
                    ////
                }
                return Ok();
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        private void SaveCartItems(List<CartItem> cartItems)
        {
            var serializedCartItems = JsonSerializer.Serialize(cartItems);
            _httpContextAccessor.HttpContext.Session.SetString("CartItems", serializedCartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, Decimal price, int quantitys, string anhsps)
        {
            List<CartItem> cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(item => item.productId == productId);

            if (existingItem != null)
            {
                int previousQuantity = existingItem.quantity;

                existingItem.quantity += quantitys;

                if (previousQuantity != existingItem.quantity)
                {
                    existingItem.productPrice = existingItem.quantity * price;
                }
            }
            else
            {
                cartItems.Add(new CartItem { productId = productId, productName = productName, productPrice = quantitys * price, quantity = quantitys, anhsp = anhsps });
            }
            SaveCartItems(cartItems);
            return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng thành công!" });
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId)
        {
            List<CartItem> cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(item => item.productId == productId);

            if (existingItem != null)
            {
                Decimal oldProductPrice = existingItem.productPrice;
                existingItem.quantity += 1;
                existingItem.productPrice = existingItem.quantity * (oldProductPrice / (existingItem.quantity - 1));
            }

            SaveCartItems(cartItems);
            return Json(new { success = true, message = "Số lượng sản phẩm đã được cập nhật thành công!" });
        }

        public IActionResult DeleteQuantity(int productId)
        {
            List<CartItem> cartItems = GetCartItems();
            var existingItem = cartItems.FirstOrDefault(item => item.productId == productId);

            if (existingItem != null)
            {
                existingItem.productPrice = existingItem.productPrice / existingItem.quantity;

                existingItem.quantity -= 1;

                if (existingItem.quantity <= 0)
                {
                    cartItems.Remove(existingItem);
                }
                else
                {
                    existingItem.productPrice = existingItem.productPrice * existingItem.quantity;
                }
            }

            SaveCartItems(cartItems);
            return Json(new { success = true, message = "Số lượng sản phẩm đã được cập nhật thành công!" });
        }
        public IActionResult ThanhToan(don_hang don_Hang, thong_tin_van_chuyen thong_Tin_Van_Chuyen, string magiamgia)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<CartItem> cartItems = GetCartItems();
                don_Hang.ngaytao = DateTime.Now;
                don_Hang.khachhang_id = id;
                don_Hang.trangthai = "đã đặt hàng";
                int iddonhang = don_HangService.ThemDonHang(don_Hang);
                decimal tongtien = 0;
                khach_hang khach_Hang1 = khach_hangService.LayThongTinKhachHangTheoID(id);
                if (iddonhang > 0)
                {
                    StringBuilder contentBuilder = new StringBuilder();
                    contentBuilder.Append("<table style='width: 80%; margin: auto; border-collapse: collapse; text-align: center; border: 1px solid #ddd;'>");
                    contentBuilder.Append("<tr style='background-color: #f2f2f2;'><th style='padding: 10px; border: 1px solid #ddd;'>Tên sản phẩm</th><th style='border: 1px solid #ddd;'>Số lượng mua</th><th style='border: 1px solid #ddd;'>Thành tiền</th><th style='border: 1px solid #ddd;'>Ảnh</th></tr>");
                    foreach (var cartItem in cartItems)
                    {
                        chi_tiet_don_hang chi_Tiet_Don_Hang = new()
                        {
                            sanpham_id = cartItem.productId,
                            donhang_id = iddonhang,
                            thanhtien = cartItem.productPrice,
                            soluongmua = cartItem.quantity
                        };
                        chi_Tiet_Don_HangService.ThemChiTietDonHang(chi_Tiet_Don_Hang);
                        tongtien += cartItem.productPrice;
                        san_pham san_Pham = san_PhamService.GetSanPhamById(cartItem.productId);
                        contentBuilder.Append("<tr>");
                        contentBuilder.Append("<td style='border: 1px solid #ddd; padding: 10px;'>" + cartItem.productName + "</td>");
                        contentBuilder.Append("<td style='border: 1px solid #ddd;'>" + cartItem.quantity + "</td>");
                        contentBuilder.Append("<td style='border: 1px solid #ddd;'>" + cartItem.productPrice.ToString("N0") + " VND</td>");
                        contentBuilder.Append("<td style='border: 1px solid #ddd;'><img src='" + cartItem.anhsp + "' alt='Hình ảnh sản phẩm' style='width: 100px; height: 60px;'></td>");
                        contentBuilder.Append("</tr>");
                    }
                    contentBuilder.Append("</table>");
                    decimal phantramgiamgia = 0;
                    if (magiamgia != null)
                    {
                        ma_giam_gia ma_Giam_Gias = ma_Giam_GiaService.GetMaGiamGiaByMaGiamGia(magiamgia);
                        phantramgiamgia = ma_Giam_Gias.phantramgiamgia;
                        tongtien = tongtien * (1 - (phantramgiamgia / 100));
                    }
                    contentBuilder.Append("<div style='display: flex; flex-direction: column;text-align: center; align-items: center; margin-top: 20px;margin-right: 40px;'>");
                    contentBuilder.Append("<p><b>Tổng tiền: </b>" + tongtien.ToString("N0") + " VND" + "</p>");
                    if (magiamgia != null)
                    {
                        contentBuilder.Append("<p><b>Giảm giá: </b>" + phantramgiamgia + "%</p>");
                    }
                    else
                    {
                        contentBuilder.Append("<p><b>Giảm giá: </b>0%</p>");
                    }
                    contentBuilder.Append("</div>");
                    string content = contentBuilder.ToString();
                    don_HangService.GuiEmail(khach_Hang1, content);
                }
                if (magiamgia != null)
                {
                    ma_giam_gia ma_Giam_Gias = ma_Giam_GiaService.GetMaGiamGiaByMaGiamGia(magiamgia);
                    decimal phantramgiamgia = ma_Giam_Gias.phantramgiamgia;
                    decimal sotienthanhtoan = tongtien * (1 - (phantramgiamgia / 100));
                    lich_su_thanh_toan lich_Su_Thanh_Toan = new lich_su_thanh_toan
                    {
                        ngaythanhtoan = DateTime.Now,
                        sotienthanhtoan = sotienthanhtoan,
                        hinhthucthanhtoan = "chuyển khoản",
                        trangthai = "đã thanh toán",
                        phantramgiamgia = phantramgiamgia,
                        donhang_id = iddonhang
                    };
                    lich_Su_Thanh_ToanService.ThemLichSuThanhToan(lich_Su_Thanh_Toan);
                }
                else
                {
                    lich_su_thanh_toan lich_Su_Thanh_Toan = new lich_su_thanh_toan
                    {
                        ngaythanhtoan = DateTime.Now,
                        sotienthanhtoan = tongtien,
                        hinhthucthanhtoan = "chuyển khoản",
                        trangthai = "đã thanh toán",
                        phantramgiamgia = 0,
                        donhang_id = iddonhang
                    };
                    lich_Su_Thanh_ToanService.ThemLichSuThanhToan(lich_Su_Thanh_Toan);
                }
                thong_Tin_Van_Chuyen.phivanchuyen = 25000;
                thong_Tin_Van_Chuyen.donhang_id = iddonhang;
                thong_Tin_Van_ChuyenService.ThemThongTinVanChuyen(thong_Tin_Van_Chuyen);
                int solandathang = don_HangService.countsolandatphongtheokhachhangid(id);
                quy_dinh_giam_gia quydinhgiamgia = quy_Dinh_Giam_GiaService.GetQuyDinhGiasolandatphong(solandathang);
                if (quydinhgiamgia != null)
                {
                    if (quydinhgiamgia.solandathang == solandathang)
                    {
                        ma_giam_gia magiamgias = ma_Giam_GiaService.LayMaGiamGiaTheoID(quydinhgiamgia.id);
                        if (magiamgias != null)
                        {
                            khach_hang khach_Hang = khach_hangService.LayThongTinKhachHangTheoID(id);
                            ma_Giam_GiaService.GuiEmail(khach_Hang, magiamgias.magiamgia);
                            ma_giam_gia capnhatsolandasudung = ma_Giam_GiaService.LayMaGiamGiaTheoID(magiamgias.id);
                            capnhatsolandasudung.solandasudung++;
                            ma_Giam_GiaService.SuaMaGiamGia(capnhatsolandasudung);
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        /// ko có mã giảm giá
                    }
                }
                if (magiamgia != null)
                {
                    ma_giam_gia ma_Giam_Gia = ma_Giam_GiaService.GetMaGiamGiaByMaGiamGia(magiamgia);
                    if (ma_Giam_Gia.solandasudung < ma_Giam_Gia.solansudungtoida || DateTime.Now.Day > ma_Giam_Gia.ngayketthuc.Day)
                    {
                        int khachhangsudungmagiamgia = ma_Giam_GiaService.countkhachhangsu_dung_ma_giam_gia(id, ma_Giam_Gia.id);
                        if (khachhangsudungmagiamgia == 0)
                        {
                            su_dung_ma_giam_gia su_Dung_Ma_Giam_Gia = new()
                            {
                                magiamgia_id = ma_Giam_Gia.id,
                                donhang_id = iddonhang,
                                ngaysudung = DateTime.Now
                            };
                            su_Dung_Ma_Giam_GiaService.Themsu_dung_ma_giam_gia(su_Dung_Ma_Giam_Gia);
                        }
                        else
                        {
                            ///đã sử dụng mã giảm giá
                        }
                    }
                    else
                    {
                        /// mã giảm giá đã sử dụng
                    }
                }
                else
                {
                    // không có mã giảm giá
                }
                _httpContextAccessor.HttpContext.Session.Remove("CartItems");
                TempData["thanhtoanthanhcong"] = "";
                return RedirectToAction("guidonhang", "Cart");
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public IActionResult xoa()
        {
            _httpContextAccessor.HttpContext.Session.Remove("CartItems");
            return RedirectToAction("guidonhang", "Cart");

        }
        public IActionResult guidonhang()
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