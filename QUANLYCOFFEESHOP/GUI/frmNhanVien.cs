using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.DTO;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmNhanVien : Form
    {
        private NhanVienDAL nhanVienDAL = new NhanVienDAL();

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            ResetForm();
        }

        private void LoadDataGridView()
        {
            try
            {
                List<NhanVienDTO> list = nhanVienDAL.GetAll();
                dgvNhanVien.DataSource = list;

                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                dgvNhanVien.Columns["HoTen"].HeaderText = "H? tên";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Gi?i tính";
                dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["SDT"].HeaderText = "S? ?i?n tho?i";
                dgvNhanVien.Columns["DiaChi"].HeaderText = "??a ch?";
                dgvNhanVien.Columns["NgayVaoLam"].HeaderText = "Ngày vào làm";

                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvNhanVien.Columns["NgayVaoLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i load d? li?u: " + ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                    rdoNam.Checked = true;
                else
                    rdoNu.Checked = true;

                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNV = txtMaNV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdoNam.Checked ? "Nam" : "N?",
                    NgaySinh = dtpNgaySinh.Value,
                    SDT = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    NgayVaoLam = dtpNgayVaoLam.Value
                };

                if (nhanVienDAL.Insert(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên th?t b?i!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNV = txtMaNV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = rdoNam.Checked ? "Nam" : "N?",
                    NgaySinh = dtpNgaySinh.Value,
                    SDT = txtSDT.Text.Trim(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    NgayVaoLam = dtpNgayVaoLam.Value
                };

                if (nhanVienDAL.Update(nv))
                {
                    MessageBox.Show("C?p nh?t nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("C?p nh?t nhân viên th?t b?i!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                {
                    MessageBox.Show("Vui lòng ch?n nhân viên c?n xóa!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("B?n có ch?c mu?n xóa nhân viên này?", "Xác nh?n", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (nhanVienDAL.Delete(txtMaNV.Text))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên th?t b?i!\nNhân viên có th? có tài kho?n ho?c hóa ??n.", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i: " + ex.Message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadDataGridView();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadDataGridView();
                }
                else
                {
                    List<NhanVienDTO> list = nhanVienDAL.Search(keyword);
                    dgvNhanVien.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("L?i tìm ki?m: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            if (!Validator.IsNotEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng nh?p mã nhân viên!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Focus();
                return false;
            }

            if (!Validator.IsNotEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nh?p h? tên!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (dtpNgaySinh.Value >= DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không h?p l?!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Validator.IsValidPhone(txtSDT.Text))
            {
                MessageBox.Show("S? ?i?n tho?i không h?p l?!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (dtpNgayVaoLam.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày vào làm không ???c trong t??ng lai!", "C?nh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            rdoNam.Checked = true;
            dtpNgaySinh.Value = DateTime.Now.AddYears(-20);
            txtSDT.Clear();
            txtDiaChi.Clear();
            dtpNgayVaoLam.Value = DateTime.Now;
            txtTimKiem.Clear();

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNV.Focus();
        }
    }
}
