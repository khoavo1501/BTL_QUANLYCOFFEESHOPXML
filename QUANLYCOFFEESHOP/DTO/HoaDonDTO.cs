using System;

namespace QUANLYCOFFEESHOP.DTO
{
    public class HoaDonDTO
    {
        public string MaHD { get; set; }
        public string MaNV { get; set; }
        public DateTime ThoiGianLap { get; set; }
        public int TongTien { get; set; }
        public string TrangThai { get; set; }

        public HoaDonDTO() { }

        public HoaDonDTO(string maHD, string maNV, DateTime thoiGianLap, int tongTien, string trangThai)
        {
            MaHD = maHD;
            MaNV = maNV;
            ThoiGianLap = thoiGianLap;
            TongTien = tongTien;
            TrangThai = trangThai;
        }
    }
}
