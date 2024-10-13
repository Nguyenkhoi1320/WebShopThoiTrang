using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class san_pham
    {
        public int id { get; set; }
        public string tensanpham { get; set; }
        public decimal giaban { get; set; }
        public string mota { get; set; }
        public string anh { get; set; }
        public int soluongcon { get; set; }
        public int nhacungcap_id { get; set; }
        public int danhmuc_id { get; set; }

        
    }
}