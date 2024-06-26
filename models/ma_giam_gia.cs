using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class ma_giam_gia
    {
        public int id { get; set; }
        public string magiamgia { get; set; }
        public string mota { get; set; }
        public decimal phantramgiamgia { get; set; }
        public int solansudungtoida { get; set; }
        public int solandasudung { get; set; }
        public DateTime ngaybatdau { get; set; }
        public DateTime ngayketthuc { get; set; }
        public int quydinggiamgia_id { get; set; }

    }
}