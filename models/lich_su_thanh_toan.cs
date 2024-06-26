using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class lich_su_thanh_toan
    {
        public int id { get; set; }
        public DateTime ngaythanhtoan { get; set; }
        public decimal sotienthanhtoan { get; set; }
        public string hinhthucthanhtoan { get; set; }
        public string trangthai { get; set; }
        public decimal phantramgiamgia { get; set; }
        public int donhang_id { get; set; }
        public int nhanvien_id { get; set; }
    }
}