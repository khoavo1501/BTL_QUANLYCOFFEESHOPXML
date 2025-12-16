using System;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmDoiMatKhau : Form
    {
        private TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();

        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            lblTenDangNhap.Text = "Tài khoản: " + SessionManager.CurrentUser.TaiKhoan;
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            try
            {
                string matKhauCu = txtMatKhauCu.Text.Trim();
                string matKhauMoi = txtMatKhauMoi.Text.Trim();
                string xacNhanMatKhau = txtXacNhanMatKhau.Text.Trim();

                if (string.IsNullOrEmpty(matKhauCu))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauCu.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(matKhauMoi))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(xacNhanMatKhau))
                {
                    MessageBox.Show("Vui lòng xác nhận mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtXacNhanMatKhau.Focus();
                    return;
                }

                if (matKhauMoi != xacNhanMatKhau)
                {
                    MessageBox.Show("Mật khẩu mới và xác nhận không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtXacNhanMatKhau.Focus();
                    return;
                }

                if (matKhauMoi == matKhauCu)
                {
                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhauMoi.Focus();
                    return;
                }

                string hashedOldPassword = Helper.MD5Hash(matKhauCu);
                if (hashedOldPassword != SessionManager.CurrentUser.MatKhau)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Focus();
                    return;
                }

                string hashedNewPassword = Helper.MD5Hash(matKhauMoi);

                if (taiKhoanDAL.ChangePassword(SessionManager.CurrentUser.TaiKhoan, hashedNewPassword))
                {
                    MessageBox.Show("Đổi mật khẩu thành công!\nVui lòng đăng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SessionManager.CurrentUser.MatKhau = hashedNewPassword;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauCu.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            txtMatKhauMoi.UseSystemPasswordChar = !chkHienMatKhau.Checked;
            txtXacNhanMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
