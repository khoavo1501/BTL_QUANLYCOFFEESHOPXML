using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.GUI;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Khởi tạo dữ liệu mẫu nếu chưa có
            if (!DataInitializer.HasData())
            {
                DataInitializer.InitializeDefaultData();
                MessageBox.Show("Đã khởi tạo dữ liệu mẫu thành công!\n\nTài khoản:\n- admin / 123 (Quản lý)\n- nv01 / 123 (Nhân viên)\n- nv02 / 123 (Nhân viên)", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            Application.Run(new frmLogin());
        }
    }
}
