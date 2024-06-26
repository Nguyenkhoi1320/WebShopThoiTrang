using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class quy_dinh_giam_gia
    {
        public int id { get; set; }
        public int solandathang { get; set; }
        public Decimal tongtientoithieu { get; set; }
        public Decimal phantramgiamgia { get; set; }
        public DateTime ngaythemquydinh { get; set; }

    }
}