using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.DTO;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmTaiKhoan : Form
    {
        private TaiKhoanDAL taiKhoanDAL = new TaiKhoanDAL();
        private NhanVienDAL nhanVienDAL = new NhanVienDAL();

        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadComboBoxNhanVien();
            LoadDataGridView();
            ResetForm();
        }

        private void LoadComboBoxNhanVien()
        {
            try
            {
                List<NhanVienDTO> listNV = nhanVienDAL.GetAll();
                cboNhanVien.DataSource = listNV;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "MaNV";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhân viên: " + ex.Message);
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                List<TaiKhoanDTO> list = taiKhoanDAL.GetAll();
                dgvTaiKhoan.DataSource = list;

                dgvTaiKhoan.Columns["TaiKhoan"].HeaderText = "Tài khoản";
                dgvTaiKhoan.Columns["MatKhau"].HeaderText = "Mật khẩu";
                dgvTaiKhoan.Columns["Quyen"].HeaderText = "Quyền";
                dgvTaiKhoan.Columns["MaNV"].HeaderText = "Mã NV";

                dgvTaiKhoan.Columns["MatKhau"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                txtMatKhau.Text = "";
                
                int quyen = Convert.ToInt32(row.Cells["Quyen"].Value);
                cboQuyen.SelectedIndex = quyen;
                
                cboNhanVien.SelectedValue = row.Cells["MaNV"].Value.ToString();

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnResetMK.Enabled = true;
                txtTaiKhoan.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                string hashedPassword = Helper.MD5Hash(txtMatKhau.Text);

                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    TaiKhoan = txtTaiKhoan.Text.Trim(),
                    MatKhau = hashedPassword,
                    Quyen = cboQuyen.SelectedIndex,
                    MaNV = cboNhanVien.SelectedValue.ToString()
                };

                if (taiKhoanDAL.Insert(tk))
                {
                    MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại!\nTên tài khoản có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaiKhoan.Text))
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string matKhau = "";
                if (!string.IsNullOrEmpty(txtMatKhau.Text))
                {
                    matKhau = Helper.MD5Hash(txtMatKhau.Text);
                }
                else
                {
                    var currentAccount = taiKhoanDAL.GetAll().Find(x => x.TaiKhoan == txtTaiKhoan.Text);
                    matKhau = currentAccount.MatKhau;
                }

                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    TaiKhoan = txtTaiKhoan.Text.Trim(),
                    MatKhau = matKhau,
                    Quyen = cboQuyen.SelectedIndex,
                    MaNV = cboNhanVien.SelectedValue.ToString()
                };

                if (taiKhoanDAL.Update(tk))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaiKhoan.Text))
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtTaiKhoan.Text == SessionManager.CurrentUser.TaiKhoan)
                {
                    MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (taiKhoanDAL.Delete(txtTaiKhoan.Text))
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetMK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaiKhoan.Text))
                {
                    MessageBox.Show("Vui lòng chọn tài khoản cần reset mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Reset mật khẩu về mặc định (123)?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string defaultPassword = Helper.MD5Hash("123");
                    
                    if (taiKhoanDAL.ChangePassword(txtTaiKhoan.Text, defaultPassword))
                    {
                        MessageBox.Show("Reset mật khẩu thành công!\nMật khẩu mới: 123", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reset mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private bool ValidateInput()
        {
            if (!Validator.IsNotEmpty(txtTaiKhoan.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaiKhoan.Focus();
                return false;
            }

            if (btnThem.Enabled && !Validator.IsNotEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            cboQuyen.SelectedIndex = 0;
            if (cboNhanVien.Items.Count > 0) cboNhanVien.SelectedIndex = 0;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnResetMK.Enabled = false;
            txtTaiKhoan.Enabled = true;
            txtTaiKhoan.Focus();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
