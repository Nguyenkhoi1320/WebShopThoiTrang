using Microsoft.AspNetCore.Mvc;
using models;
using service;

namespace Quanlybanhang.Controllers
{
    public class UserController : Controller
    {
        private readonly khach_hangService khach_HangService;
        private readonly nhan_vienService nhan_VienService;
        public UserController(khach_hangService khach_HangServices, nhan_vienService nhan_VienService)
        {
            khach_HangService = khach_HangServices;
            this.nhan_VienService = nhan_VienService;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                List<nhan_vien> listnhanvien = nhan_VienService.LayDanhSachNhanVien();
                modeldata modeldata = new modeldata()
                {
                    nhanvienlist = listnhanvien,
                };
                return View(modeldata);
            }
            else
            {
                return RedirectToAction("login", "Home");
            
        }
            }
            
    public IActionResult Addnhanvien()
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
        public IActionResult Addnhanviens(nhan_vien nhan_Vien)
        {
            nhan_VienService.AddNhanVien(nhan_Vien);
            return RedirectToAction("Index", "user");
        }
        public IActionResult UpdateNhanvienbyid(int id)
        {
                if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
                {
                    int ids = HttpContext.Session.GetInt32("id").Value;
                    string hovaten = HttpContext.Session.GetString("hovaten");
                    ViewData["id"] = ids;
                    ViewData["hovaten"] = hovaten;
                    nhan_vien nhan_Vien = nhan_VienService.nhanvienbyid(id);
                    modeldata modeldata = new modeldata()
                    {
                        nhan_Vien = nhan_Vien,
                    };
                    return View(modeldata);
                }
                else
                {
                    return RedirectToAction("login", "Home");
                }

            }
        public IActionResult updatenhanvien(nhan_vien nhan_Vien)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int ids = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = ids;
                ViewData["hovaten"] = hovaten;
                nhan_VienService.updatenhanvien(nhan_Vien);
             return RedirectToAction("index", "user");
            }
            else
            {
                return RedirectToAction("login", "Home");
            }

        }
        public IActionResult Xoanhanvien(int id)
        {
            nhan_VienService.deletenhanvien(id);
            return RedirectToAction("index", "user");
        }
        public IActionResult logins(string email, string matkhau)
        {
            khach_hang khach_Hang = khach_HangService.check_thongtin_login(email, matkhau);
            nhan_vien nhan_Vien = nhan_VienService.check_thongtin_login(email, matkhau);
            if (khach_Hang != null)
            {
                HttpContext.Session.SetInt32("id", khach_Hang.id);
                HttpContext.Session.SetString("email", khach_Hang.email);
                HttpContext.Session.SetString("hovaten", khach_Hang.hovaten);
                return RedirectToAction("Index", "Home");
            }
            if (nhan_Vien != null)
            {
                HttpContext.Session.SetInt32("id", nhan_Vien.id);
                HttpContext.Session.SetString("email", nhan_Vien.email);
                HttpContext.Session.SetString("hovaten", nhan_Vien.hovaten);
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("login", "Home");
        }
        public IActionResult RegisterKhachHang(khach_hang khach_Hang, string lmatkhau)
        {
            if (khach_Hang.matkhau == lmatkhau)
            {
                khach_HangService.RegisterKhachHang(khach_Hang);
                return RedirectToAction("login", "Home");
            }
            else
            {
               /* ModelState.AddModelError(string.Empty, "Mật khẩu không trùng khớp");*/
                return RedirectToAction("signup", "Home");
            }
        }
        public IActionResult changepassword(string matkhaucu, string matkhaumoi, string lmatkhaumoi)
        {
            if (HttpContext.Session.GetInt32("id") != null && HttpContext.Session.GetString("hovaten") != null)
            {
                int id = HttpContext.Session.GetInt32("id").Value;
                string hovaten = HttpContext.Session.GetString("hovaten");
                ViewData["id"] = id;
                ViewData["hovaten"] = hovaten;
                khach_hang khach_Hang = khach_HangService.GetKhachHangById(id);
                if (khach_Hang.matkhau == matkhaucu)
                {
                    if (matkhaumoi == lmatkhaumoi)
                    {
                        khach_Hang.matkhau = matkhaumoi;
                        khach_HangService.UpdateKhachHang(khach_Hang);
                        return RedirectToAction("login", "Home");
                    }
                    else
                    {
                        return RedirectToAction("changepassword", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("changepassword", "Home");
                }
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        /* public string GenerateRandomCode()
         {
             // Hàm này sinh ra một mã xác nhận ngẫu nhiên, bạn có thể thay đổi cách sinh mã tùy ý
             Random random = new Random();
             const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
             return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
         }

         public void SendConfirmationEmail(string recipientEmail, string confirmationCode)
         {
             // Sử dụng thư viện Gmail để gửi email xác nhận
             // Thiết lập thông tin xác thực Gmail của bạn tại đây
             var credential = GoogleCredential.FromFile("path_to_your_json_file_containing_credentials.json");
             var service = new GmailService(new BaseClientService.Initializer()
             {
                 HttpClientInitializer = credential,
                 ApplicationName = "YourAppName",
             });

             var msg = new MimeMessage();
             msg.From.Add(new MailboxAddress("YourAppName", "your_email@gmail.com"));
             msg.To.Add(new MailboxAddress("", recipientEmail));
             msg.Subject = "Xác nhận đăng ký";
             msg.Body = new TextPart("plain")
             {
                 Text = $"Mã xác nhận của bạn là: {confirmationCode}"
             };

             using (var memoryStream = new MemoryStream())
             {
                 msg.WriteTo(memoryStream);
                 var result = service.Users.Messages.Send(new Message
                 {
                     Raw = Base64UrlEncode(memoryStream.ToArray())
                 }, "me").Execute();
             }
         }
 */
        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");
        }
        
    }
}
