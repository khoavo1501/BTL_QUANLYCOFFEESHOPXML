namespace QUANLYCOFFEESHOP.DTO
{
    public class LoaiSanPhamDTO
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }

        public LoaiSanPhamDTO() { }

        public LoaiSanPhamDTO(string maLoai, string tenLoai, string moTa)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
            MoTa = moTa;
        }
    }
}
