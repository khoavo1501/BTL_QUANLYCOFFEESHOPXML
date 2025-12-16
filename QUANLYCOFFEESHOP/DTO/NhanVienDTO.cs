using System;

namespace QUANLYCOFFEESHOP.DTO
{
    public class NhanVienDTO
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }

        public NhanVienDTO() { }

        public NhanVienDTO(string maNV, string hoTen, string gioiTinh, DateTime ngaySinh, string sdt, string diaChi, DateTime ngayVaoLam)
        {
            MaNV = maNV;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            SDT = sdt;
            DiaChi = diaChi;
            NgayVaoLam = ngayVaoLam;
        }
    }
}
