namespace QUANLYCOFFEESHOP.DTO
{
    public class ThongTinCuaHangDTO
    {
        public string TenQuan { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }

        public ThongTinCuaHangDTO() { }

        public ThongTinCuaHangDTO(string tenQuan, string diaChi, string sdt, string email)
        {
            TenQuan = tenQuan;
            DiaChi = diaChi;
            SDT = sdt;
            Email = email;
        }
    }
}
