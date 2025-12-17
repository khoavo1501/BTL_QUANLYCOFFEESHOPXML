using System;
using System.IO;
using System.Windows.Forms;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.GUI
{
    public partial class frmXuatBaoCaoHTML : Form
    {
        public frmXuatBaoCaoHTML()
        {
            InitializeComponent();
        }

        private void frmXuatBaoCaoHTML_Load(object sender, EventArgs e)
        {
            // Đặt đường dẫn mặc định
            string defaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            txtOutputPath.Text = Path.Combine(defaultPath, $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.html");
        }

        private void btnChonThuMuc_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "HTML Files (*.html)|*.html|All Files (*.*)|*.*";
                saveFileDialog.Title = "Chọn vị trí lưu báo cáo HTML";
                saveFileDialog.FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.html";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = saveFileDialog.FileName;
                }
            }
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtOutputPath.Text))
                {
                    MessageBox.Show("Vui lòng chọn đường dẫn lưu file!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị progress
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                btnXuatBaoCao.Enabled = false;

                // Tạo báo cáo HTML
                bool success = HTMLReportHelper.GenerateHTMLReport(txtOutputPath.Text);

                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Continuous;
                btnXuatBaoCao.Enabled = true;

                if (success)
                {
                    // Tự động mở file HTML lên trình duyệt
                    System.Diagnostics.Process.Start(txtOutputPath.Text);
                    
                    MessageBox.Show(
                        $"Xuất báo cáo HTML thành công!\n\nFile đã được mở trên trình duyệt.\n\nĐường dẫn:\n{txtOutputPath.Text}",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xuất báo cáo thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Continuous;
                btnXuatBaoCao.Enabled = true;
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMoThuMucXML_Click(object sender, EventArgs e)
        {
            try
            {
                string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                if (Directory.Exists(xmlPath))
                {
                    System.Diagnostics.Process.Start("explorer.exe", xmlPath);
                }
                else
                {
                    MessageBox.Show("Thư mục Data chưa tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
