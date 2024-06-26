using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class chi_tiet_don_hang
    {
        public int id { get; set; }
        public int donhang_id { get; set; }
        public int sanpham_id { get; set; }
        public int soluongmua { get; set; }
        public Decimal thanhtien { get; set; }
    }
}