using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class su_dung_ma_giam_gia
    {
        public int id { get; set; }
        public int magiamgia_id { get; set; }
        public int donhang_id { get; set; }
        public DateTime ngaysudung { get; set; }
    }
}