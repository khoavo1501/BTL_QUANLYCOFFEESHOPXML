using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.DTO;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmLoaiSanPham : Form
    {
        private LoaiSanPhamDAL loaiSanPhamDAL = new LoaiSanPhamDAL();

        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            ResetForm();
        }

        private void LoadDataGridView()
        {
            try
            {
                List<LoaiSanPhamDTO> list = loaiSanPhamDAL.GetAll();
                dgvLoaiSP.DataSource = list;

                dgvLoaiSP.Columns["MaLoai"].HeaderText = "Mã loại";
                dgvLoaiSP.Columns["TenLoai"].HeaderText = "Tên loại";
                dgvLoaiSP.Columns["MoTa"].HeaderText = "Mô tả";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLoaiSP.Rows[e.RowIndex];
                txtMaLoai.Text = row.Cells["MaLoai"].Value.ToString();
                txtTenLoai.Text = row.Cells["TenLoai"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();

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

                LoaiSanPhamDTO loai = new LoaiSanPhamDTO
                {
                    MaLoai = txtMaLoai.Text.Trim(),
                    TenLoai = txtTenLoai.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                if (loaiSanPhamDAL.Insert(loai))
                {
                    MessageBox.Show("Thêm loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm loại sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!ValidateInput()) return;

                LoaiSanPhamDTO loai = new LoaiSanPhamDTO
                {
                    MaLoai = txtMaLoai.Text.Trim(),
                    TenLoai = txtTenLoai.Text.Trim(),
                    MoTa = txtMoTa.Text.Trim()
                };

                if (loaiSanPhamDAL.Update(loai))
                {
                    MessageBox.Show("Cập nhật loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật loại sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (string.IsNullOrEmpty(txtMaLoai.Text))
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa loại sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (loaiSanPhamDAL.Delete(txtMaLoai.Text))
                    {
                        MessageBox.Show("Xóa loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGridView();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa loại sản phẩm thất bại!\nLoại này có thể đang được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadDataGridView();
        }

        private bool ValidateInput()
        {
            if (!Validator.IsNotEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập mã loại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoai.Focus();
                return false;
            }

            if (!Validator.IsNotEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtMoTa.Clear();

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaLoai.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
