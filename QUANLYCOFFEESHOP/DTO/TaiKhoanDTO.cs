namespace QUANLYCOFFEESHOP.DTO
{
    public class TaiKhoanDTO
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public int Quyen { get; set; }
        public string MaNV { get; set; }

        public TaiKhoanDTO() { }

        public TaiKhoanDTO(string taiKhoan, string matKhau, int quyen, string maNV)
        {
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
            Quyen = quyen;
            MaNV = maNV;
        }
    }
}
