using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models
{
    public class modeldata
    {
        public chi_tiet_don_hang chiTietDonHang { get; set; }
        public List<chi_tiet_don_hang> chiTietDonHanglist { get; set; }
        public don_hang DonHangs { get; set; }
        public List<don_hang> donHanglist { get; set; }

        public IPagedList<don_hang> paliistdonhang { get; set; }
        public khach_hang khach_hang { get; set; }
        public lich_su_thanh_toan lich_Su_Thanh_Toan { get; set; }
        public ma_giam_gia ma_Giam_Gia { get; set; }
        public List<ma_giam_gia> ma_Giam_Gialist { get; set; }
        public nha_cung_cap nha_Cung_Cap { get; set; }
        public List<nha_cung_cap> nha_Cung_CapList { get; set; }
        public nhan_vien nhan_Vien { get; set; }
        public List<nhan_vien> nhanvienlist { get; set; }
        public quy_dinh_giam_gia quy_Dinh_Giam_Gia { get; set; }
        public List<quy_dinh_giam_gia> quy_Dinh_Giam_Gialist { get; set; }
        public san_pham san_Pham { get; set; }
        public List<san_pham> san_Phamlist { get; set; }
        public List<CartItem> cartItems { get; set; }
        public su_dung_ma_giam_gia su_Dung_Ma_Giam_Gia { get; set; }
        public List<sizes> sizesss { get; set; }
        public sizes Sizes { get; set; }
        public List<danh_muc> danh_Mucs { get; set; }
        public danh_muc danh_Muc { get; set; }

    }
}
