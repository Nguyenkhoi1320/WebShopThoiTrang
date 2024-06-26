using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class don_hang
    {
        public int id { get; set; }
        public DateTime ngaytao { get; set; }
        public string trangthai { get; set; }

        public int khachhang_id { get; set; }
    }
}