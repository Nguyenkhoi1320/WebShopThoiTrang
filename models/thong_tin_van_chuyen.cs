using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class thong_tin_van_chuyen
    {
        public int id { get; set; }
        public string diachi { get; set; }
        public decimal phivanchuyen { get; set; }
        public int donhang_id { get; set; }
    }
}