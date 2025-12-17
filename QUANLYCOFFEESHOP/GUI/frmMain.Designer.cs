namespace QUANLYCOFFEESHOP.GUI
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanHangMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBanHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLLoaiSP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuQLNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDSHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuXuatBaoCaoHTML = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongTinMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongTin = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusQuyen = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuBanHangMenu,
            this.mnuQuanLy,
            this.mnuHoaDon,
            this.mnuBaoCaoMenu,
            this.mnuThongTinMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoiMatKhau,
            this.mnuDangXuat,
            this.toolStripSeparator1,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(69, 20);
            this.mnuHeThong.Text = "Hệ thống";
            this.mnuHeThong.Click += new System.EventHandler(this.mnuHeThong_Click);
            // 
            // mnuDoiMatKhau
            // 
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(145, 22);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            this.mnuDoiMatKhau.Click += new System.EventHandler(this.mnuDoiMatKhau_Click);
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(145, 22);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(145, 22);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // mnuBanHangMenu
            // 
            this.mnuBanHangMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBanHang});
            this.mnuBanHangMenu.Name = "mnuBanHangMenu";
            this.mnuBanHangMenu.Size = new System.Drawing.Size(69, 20);
            this.mnuBanHangMenu.Text = "Bán hàng";
            // 
            // mnuBanHang
            // 
            this.mnuBanHang.Name = "mnuBanHang";
            this.mnuBanHang.Size = new System.Drawing.Size(124, 22);
            this.mnuBanHang.Text = "Bán hàng";
            this.mnuBanHang.Click += new System.EventHandler(this.mnuBanHang_Click);
            // 
            // mnuQuanLy
            // 
            this.mnuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQLSanPham,
            this.mnuQLLoaiSP,
            this.toolStripSeparator2,
            this.mnuQLNhanVien,
            this.mnuQLTaiKhoan});
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Size = new System.Drawing.Size(60, 20);
            this.mnuQuanLy.Text = "Quản lý";
            // 
            // mnuQLSanPham
            // 
            this.mnuQLSanPham.Name = "mnuQLSanPham";
            this.mnuQLSanPham.Size = new System.Drawing.Size(170, 22);
            this.mnuQLSanPham.Text = "Quản lý sản phẩm";
            this.mnuQLSanPham.Click += new System.EventHandler(this.mnuQLSanPham_Click);
            // 
            // mnuQLLoaiSP
            // 
            this.mnuQLLoaiSP.Name = "mnuQLLoaiSP";
            this.mnuQLLoaiSP.Size = new System.Drawing.Size(170, 22);
            this.mnuQLLoaiSP.Text = "Quản lý loại SP";
            this.mnuQLLoaiSP.Click += new System.EventHandler(this.mnuQLLoaiSP_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // mnuQLNhanVien
            // 
            this.mnuQLNhanVien.Name = "mnuQLNhanVien";
            this.mnuQLNhanVien.Size = new System.Drawing.Size(170, 22);
            this.mnuQLNhanVien.Text = "Quản lý nhân viên";
            this.mnuQLNhanVien.Click += new System.EventHandler(this.mnuQLNhanVien_Click);
            // 
            // mnuQLTaiKhoan
            // 
            this.mnuQLTaiKhoan.Name = "mnuQLTaiKhoan";
            this.mnuQLTaiKhoan.Size = new System.Drawing.Size(170, 22);
            this.mnuQLTaiKhoan.Text = "Quản lý tài khoản";
            this.mnuQLTaiKhoan.Click += new System.EventHandler(this.mnuQLTaiKhoan_Click);
            // 
            // mnuHoaDon
            // 
            this.mnuHoaDon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDSHoaDon});
            this.mnuHoaDon.Name = "mnuHoaDon";
            this.mnuHoaDon.Size = new System.Drawing.Size(65, 20);
            this.mnuHoaDon.Text = "Hóa đơn";
            // 
            // mnuDSHoaDon
            // 
            this.mnuDSHoaDon.Name = "mnuDSHoaDon";
            this.mnuDSHoaDon.Size = new System.Drawing.Size(176, 22);
            this.mnuDSHoaDon.Text = "Danh sách hóa đơn";
            this.mnuDSHoaDon.Click += new System.EventHandler(this.mnuDSHoaDon_Click);
            // 
            // mnuBaoCaoMenu
            // 
            this.mnuBaoCaoMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaoCao,
            this.mnuXuatBaoCaoHTML});
            this.mnuBaoCaoMenu.Name = "mnuBaoCaoMenu";
            this.mnuBaoCaoMenu.Size = new System.Drawing.Size(61, 20);
            this.mnuBaoCaoMenu.Text = "Báo cáo";
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(182, 22);
            this.mnuBaoCao.Text = "Thống kê doanh thu";
            this.mnuBaoCao.Click += new System.EventHandler(this.mnuBaoCao_Click);
            // 
            // mnuXuatBaoCaoHTML
            // 
            this.mnuXuatBaoCaoHTML.Name = "mnuXuatBaoCaoHTML";
            this.mnuXuatBaoCaoHTML.Size = new System.Drawing.Size(182, 22);
            this.mnuXuatBaoCaoHTML.Text = "Xuất báo cáo HTML";
            this.mnuXuatBaoCaoHTML.Click += new System.EventHandler(this.mnuXuatBaoCaoHTML_Click);
            // 
            // mnuThongTinMenu
            // 
            this.mnuThongTinMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuThongTin});
            this.mnuThongTinMenu.Name = "mnuThongTinMenu";
            this.mnuThongTinMenu.Size = new System.Drawing.Size(71, 20);
            this.mnuThongTinMenu.Text = "Thông tin";
            // 
            // mnuThongTin
            // 
            this.mnuThongTin.Name = "mnuThongTin";
            this.mnuThongTin.Size = new System.Drawing.Size(178, 22);
            this.mnuThongTin.Text = "Thông tin cửa hàng";
            this.mnuThongTin.Click += new System.EventHandler(this.mnuThongTin_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusUser,
            this.toolStripStatusQuyen,
            this.toolStripStatusTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusUser
            // 
            this.toolStripStatusUser.Name = "toolStripStatusUser";
            this.toolStripStatusUser.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusUser.Text = "Người dùng:";
            // 
            // toolStripStatusQuyen
            // 
            this.toolStripStatusQuyen.Name = "toolStripStatusQuyen";
            this.toolStripStatusQuyen.Size = new System.Drawing.Size(45, 17);
            this.toolStripStatusQuyen.Text = "Quyền:";
            // 
            // toolStripStatusTime
            // 
            this.toolStripStatusTime.Name = "toolStripStatusTime";
            this.toolStripStatusTime.Size = new System.Drawing.Size(1066, 17);
            this.toolStripStatusTime.Spring = true;
            this.toolStripStatusTime.Text = "00:00:00";
            this.toolStripStatusTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::QUANLYCOFFEESHOP.Properties.Resources.thiet_ke_quan_cafe_nho_40m2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Coffee Shop";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuBanHangMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuBanHang;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem mnuQLSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuQLLoaiSP;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuQLNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuQLTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem mnuHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuDSHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuXuatBaoCaoHTML;
        private System.Windows.Forms.ToolStripMenuItem mnuThongTinMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuThongTin;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusQuyen;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTime;
        private System.Windows.Forms.Timer timer1;
    }
}
