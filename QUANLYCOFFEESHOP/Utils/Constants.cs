namespace QUANLYCOFFEESHOP.Utils
{
    public static class Constants
    {
        // Connection String
        public const string CONNECTION_STRING = "Data Source=.;Initial Catalog=QLCoffee;Integrated Security=True";

        // Quyền
        public const int QUYEN_ADMIN = 1;
        public const int QUYEN_NHANVIEN = 0;

        // Trạng thái sản phẩm
        public const string TRANGTHAI_CONBAN = "Còn bán";
        public const string TRANGTHAI_NGUNGBAN = "Ngừng bán";

        // Trạng thái hóa đơn
        public const string TRANGTHAI_DATHANHTOAN = "Đã thanh toán";
        public const string TRANGTHAI_HUY = "Đã hủy";

        // Đường dẫn backup
        public const string BACKUP_FOLDER = "Backup";
    }
}
