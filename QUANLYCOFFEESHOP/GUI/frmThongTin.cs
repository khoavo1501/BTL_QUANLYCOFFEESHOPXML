using System;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmThongTin : Form
    {
        public frmThongTin()
        {
            InitializeComponent();
        }

        private void frmThongTin_Load(object sender, EventArgs e)
        {
            LoadThongTin();
        }

        private void LoadThongTin()
        {
            lblTenCH.Text = "QUÁN COFFEE XML";
            lblDiaChi.Text = "123 Đường ABC, Quận XYZ, TP.HCM";
            lblSDT.Text = "0123-456-789";
            lblEmail.Text = "coffeexml@gmail.com";
            lblWebsite.Text = "www.coffeexml.com";
        }

        private void btnBackupFull_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Backup toàn bộ database?\nQuá trình này có thể mất vài phút.",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    progressBar.Value = 0;
                    progressBar.Visible = true;

                    string[] tables = { "LoaiSanPham", "SanPham", "NhanVien", "TaiKhoan", "HoaDon", "CTHoaDon", "ThongTinCuaHang" };
                    int step = 100 / tables.Length;

                    foreach (string table in tables)
                    {
                        XMLHelper.ExportTableToXML(table, $"{table}.xml");
                        progressBar.Value += step;
                        Application.DoEvents();
                    }

                    progressBar.Value = 100;
                    MessageBox.Show(
                        $"Backup thành công!\nThư mục: {XMLHelper.GetBackupPath()}",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    progressBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                MessageBox.Show("Lỗi backup: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMoThuMuc_Click(object sender, EventArgs e)
        {
            try
            {
                string path = XMLHelper.GetBackupPath();
                if (System.IO.Directory.Exists(path))
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                else
                {
                    MessageBox.Show("Thư mục backup chưa tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGioiThieu_Click(object sender, EventArgs e)
        {
            string message = @"HỆ THỐNG QUẢN LÝ QUÁN COFFEE

Version: 1.0.0
Phát hành: 2025

Tính năng chính:
✓ Quản lý sản phẩm và loại sản phẩm
✓ Quản lý nhân viên và tài khoản
✓ Bán hàng POS
✓ Quản lý hóa đơn
✓ Báo cáo thống kê
✓ Backup/Restore dữ liệu XML

Công nghệ:
- WinForm C#
- SQL Server
- XML Database Backup
- 3-Layer Architecture

© 2025 CoffeeXML Team";

            MessageBox.Show(message, "Giới thiệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
