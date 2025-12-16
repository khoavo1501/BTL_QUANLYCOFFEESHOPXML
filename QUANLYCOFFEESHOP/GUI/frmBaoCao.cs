using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmBaoCao : Form
    {
        private HoaDonDAL hoaDonDAL = new HoaDonDAL();
        private SanPhamDAL sanPhamDAL = new SanPhamDAL();

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            LoadThongKe();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Từ ngày phải nhỏ hơn đến ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var listHD = hoaDonDAL.GetByDateRange(tuNgay, denNgay);
                int tongHD = listHD.Count;
                int doanhThu = 0;
                foreach (var hd in listHD)
                {
                    doanhThu += hd.TongTien;
                }

                lblTongHD.Text = tongHD.ToString();
                lblDoanhThu.Text = Helper.FormatCurrency(doanhThu);

                var listSP = sanPhamDAL.GetAll();
                lblTongSP.Text = listSP.Count.ToString();

                if (tongHD > 0)
                {
                    int trungBinh = doanhThu / tongHD;
                    lblTrungBinh.Text = Helper.FormatCurrency(trungBinh);
                }
                else
                {
                    lblTrungBinh.Text = "0 đ";
                }

                LoadTopSanPham(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê: " + ex.Message);
            }
        }

        private void LoadTopSanPham(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                string query = @"SELECT TOP 10 sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSL, SUM(ct.ThanhTien) AS TongTien
                               FROM CTHoaDon ct
                               INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
                               INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                               WHERE CAST(hd.ThoiGianLap AS DATE) BETWEEN @TuNgay AND @DenNgay
                               GROUP BY sp.MaSP, sp.TenSP
                               ORDER BY TongSL DESC";

                System.Data.SqlClient.SqlParameter[] parameters = {
                    new System.Data.SqlClient.SqlParameter("@TuNgay", tuNgay),
                    new System.Data.SqlClient.SqlParameter("@DenNgay", denNgay)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                dgvTopSP.DataSource = dt;

                if (dt.Columns.Count > 0)
                {
                    dgvTopSP.Columns["MaSP"].HeaderText = "Mã SP";
                    dgvTopSP.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvTopSP.Columns["TongSL"].HeaderText = "Tổng SL";
                    dgvTopSP.Columns["TongTien"].HeaderText = "Doanh thu";

                    dgvTopSP.Columns["TongTien"].DefaultCellStyle.Format = "#,##0 đ";
                    dgvTopSP.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load top sản phẩm: " + ex.Message);
            }
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                string fileName = $"BaoCao_{tuNgay:yyyyMMdd}_{denNgay:yyyyMMdd}.xml";
                
                if (XMLHelper.ExportTableToXML("HoaDon", fileName))
                {
                    MessageBox.Show($"Xuất báo cáo thành công!\nFile: {fileName}\nThư mục: {XMLHelper.GetBackupPath()}", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xuất báo cáo thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
