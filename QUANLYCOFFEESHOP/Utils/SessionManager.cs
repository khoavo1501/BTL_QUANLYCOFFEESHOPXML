using QUANLYCOFFEESHOP.DTO;

namespace QUANLYCOFFEESHOP.Utils
{
    public static class SessionManager
    {
        public static TaiKhoanDTO CurrentUser { get; set; }
        public static NhanVienDTO CurrentEmployee { get; set; }

        public static bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public static bool IsAdmin()
        {
            return CurrentUser != null && CurrentUser.Quyen == 1;
        }

        public static void Logout()
        {
            CurrentUser = null;
            CurrentEmployee = null;
        }
    }
}
