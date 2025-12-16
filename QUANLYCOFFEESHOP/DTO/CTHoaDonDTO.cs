namespace QUANLYCOFFEESHOP.DTO
{
    public class CTHoaDonDTO
    {
        public string MaHD { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public int ThanhTien { get; set; }

        public CTHoaDonDTO() { }

        public CTHoaDonDTO(string maHD, string maSP, int soLuong, int donGia, int thanhTien)
        {
            MaHD = maHD;
            MaSP = maSP;
            SoLuong = soLuong;
            DonGia = donGia;
            ThanhTien = thanhTien;
        }
    }
}
