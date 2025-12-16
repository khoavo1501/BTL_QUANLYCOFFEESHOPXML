/* ================================
   QUẢN LÝ COFFEE SHOP - SQL SCRIPT
   Database: QLCoffee
   Author: QUANLYCOFFEESHOP Team
   Date: 2025
================================ */

/* ================================
   1. TẠO DATABASE
================================ */
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'QLCoffee')
BEGIN
    ALTER DATABASE QLCoffee SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLCoffee;
END
GO

CREATE DATABASE QLCoffee
GO

USE QLCoffee
GO

/* ================================
   2. TẠO BẢNG
================================ */

-- Bảng Loại sản phẩm
CREATE TABLE LoaiSanPham (
    MaLoai NVARCHAR(5) PRIMARY KEY,
    TenLoai NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(200)
)
GO

-- Bảng Sản phẩm
CREATE TABLE SanPham (
    MaSP NVARCHAR(5) PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    Gia INT NOT NULL CHECK (Gia > 0),
    MaLoai NVARCHAR(5),
    TrangThai NVARCHAR(20) DEFAULT N'Còn bán',
    FOREIGN KEY (MaLoai) REFERENCES LoaiSanPham(MaLoai)
)
GO

-- Bảng Nhân viên
CREATE TABLE NhanVien (
    MaNV NVARCHAR(5) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    SDT NVARCHAR(15),
    DiaChi NVARCHAR(150),
    NgayVaoLam DATE DEFAULT GETDATE()
)
GO

-- Bảng Tài khoản
CREATE TABLE TaiKhoan (
    TaiKhoan NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(50) NOT NULL,
    Quyen INT DEFAULT 0, -- 0: Nhân viên, 1: Quản lý
    MaNV NVARCHAR(5),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
)
GO

-- Bảng Hóa đơn
CREATE TABLE HoaDon (
    MaHD NVARCHAR(5) PRIMARY KEY,
    MaNV NVARCHAR(5),
    ThoiGianLap DATETIME DEFAULT GETDATE(),
    TongTien INT DEFAULT 0,
    TrangThai NVARCHAR(20) DEFAULT N'Đã thanh toán',
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
)
GO

-- Bảng Chi tiết hóa đơn
CREATE TABLE CTHoaDon (
    MaHD NVARCHAR(5),
    MaSP NVARCHAR(5),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia INT NOT NULL,
    ThanhTien INT NOT NULL,
    PRIMARY KEY (MaHD, MaSP),
    FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
)
GO

-- Bảng Thông tin cửa hàng
CREATE TABLE ThongTinCuaHang (
    TenQuan NVARCHAR(100),
    DiaChi NVARCHAR(150),
    SDT NVARCHAR(15),
    Email NVARCHAR(100)
)
GO

/* ================================
   3. INSERT DỮ LIỆU MẪU
================================ */

-- Loại sản phẩm (5 dòng)
INSERT INTO LoaiSanPham (MaLoai, TenLoai, MoTa) VALUES
('L01', N'Cà phê', N'Các loại cà phê truyền thống và hiện đại'),
('L02', N'Trà', N'Các loại trà đặc sản'),
('L03', N'Nước ép', N'Nước ép trái cây tươi'),
('L04', N'Đá xay', N'Nước đá xay các loại'),
('L05', N'Bánh', N'Bánh ngọt và snack')
GO

-- Sản phẩm (10 dòng)
INSERT INTO SanPham (MaSP, TenSP, Gia, MaLoai, TrangThai) VALUES
('SP01', N'Cà phê đen', 20000, 'L01', N'Còn bán'),
('SP02', N'Cà phê sữa', 25000, 'L01', N'Còn bán'),
('SP03', N'Bạc xỉu', 30000, 'L01', N'Còn bán'),
('SP04', N'Trà đào', 35000, 'L02', N'Còn bán'),
('SP05', N'Trà sữa', 40000, 'L02', N'Còn bán'),
('SP06', N'Nước cam', 30000, 'L03', N'Còn bán'),
('SP07', N'Nước ép táo', 32000, 'L03', N'Còn bán'),
('SP08', N'Matcha đá xay', 45000, 'L04', N'Còn bán'),
('SP09', N'Socola đá xay', 45000, 'L04', N'Còn bán'),
('SP10', N'Bánh tiramisu', 50000, 'L05', N'Còn bán')
GO

-- Nhân viên (3 dòng)
INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, NgaySinh, SDT, DiaChi, NgayVaoLam) VALUES
('NV01', N'Nguyễn Văn A', N'Nam', '2000-01-01', '0909123456', N'123 Lê Duẩn, Đà Nẵng', '2023-01-01'),
('NV02', N'Trần Thị B', N'Nữ', '2001-05-12', '0912345678', N'456 Trần Phú, Đà Nẵng', '2023-03-15'),
('NV03', N'Lê Văn C', N'Nam', '1999-08-20', '0987654321', N'789 Nguyễn Huệ, Đà Nẵng', '2023-06-01')
GO

-- Tài khoản (3 dòng)
-- Mật khẩu gốc: "123" -> MD5 Hash: "202CB962AC59075B964B07152D234B70"
INSERT INTO TaiKhoan (TaiKhoan, MatKhau, Quyen, MaNV) VALUES
('admin', '202CB962AC59075B964B07152D234B70', 1, 'NV01'),
('nv01', '202CB962AC59075B964B07152D234B70', 0, 'NV02'),
('nv02', '202CB962AC59075B964B07152D234B70', 0, 'NV03')
GO

-- Hóa đơn (10 dòng)
INSERT INTO HoaDon (MaHD, MaNV, ThoiGianLap, TongTien, TrangThai) VALUES
('HD01', 'NV02', GETDATE()-7, 75000, N'Đã thanh toán'),
('HD02', 'NV03', GETDATE()-6, 75000, N'Đã thanh toán'),
('HD03', 'NV02', GETDATE()-5, 80000, N'Đã thanh toán'),
('HD04', 'NV01', GETDATE()-4, 30000, N'Đã thanh toán'),
('HD05', 'NV01', GETDATE()-3, 60000, N'Đã thanh toán'),
('HD06', 'NV02', GETDATE()-2, 64000, N'Đã thanh toán'),
('HD07', 'NV03', GETDATE()-1, 90000, N'Đã thanh toán'),
('HD08', 'NV01', GETDATE(), 45000, N'Đã thanh toán'),
('HD09', 'NV02', GETDATE(), 85000, N'Đã thanh toán'),
('HD10', 'NV03', GETDATE(), 65000, N'Đã thanh toán')
GO

-- Chi tiết hóa đơn (10 dòng)
INSERT INTO CTHoaDon (MaHD, MaSP, SoLuong, DonGia, ThanhTien) VALUES
('HD01', 'SP01', 2, 20000, 40000),
('HD01', 'SP04', 1, 35000, 35000),
('HD02', 'SP02', 1, 25000, 25000),
('HD02', 'SP10', 1, 50000, 50000),
('HD03', 'SP05', 2, 40000, 80000),
('HD04', 'SP03', 1, 30000, 30000),
('HD05', 'SP06', 2, 30000, 60000),
('HD06', 'SP07', 2, 32000, 64000),
('HD07', 'SP08', 2, 45000, 90000),
('HD08', 'SP09', 1, 45000, 45000)
GO

-- Thông tin cửa hàng (1 dòng)
INSERT INTO ThongTinCuaHang (TenQuan, DiaChi, SDT, Email) VALUES
(N'Coffee XML', N'123 Lê Duẩn, Đà Nẵng', '0905123456', 'coffeexml@gmail.com')
GO

/* ================================
   4. TẠO VIEW VÀ STORED PROCEDURE (TÙY CHỌN)
================================ */

-- View: Thông tin sản phẩm với tên loại
CREATE VIEW vw_SanPham AS
SELECT 
    sp.MaSP,
    sp.TenSP,
    sp.Gia,
    sp.MaLoai,
    lsp.TenLoai,
    sp.TrangThai
FROM SanPham sp
INNER JOIN LoaiSanPham lsp ON sp.MaLoai = lsp.MaLoai
GO

-- View: Thông tin hóa đơn với tên nhân viên
CREATE VIEW vw_HoaDon AS
SELECT 
    hd.MaHD,
    hd.MaNV,
    nv.HoTen AS TenNhanVien,
    hd.ThoiGianLap,
    hd.TongTien,
    hd.TrangThai
FROM HoaDon hd
INNER JOIN NhanVien nv ON hd.MaNV = nv.MaNV
GO

-- Stored Procedure: Thống kê doanh thu theo ngày
CREATE PROCEDURE sp_ThongKeDoanhThu
    @TuNgay DATE,
    @DenNgay DATE
AS
BEGIN
    SELECT 
        CAST(ThoiGianLap AS DATE) AS Ngay,
        COUNT(*) AS SoHoaDon,
        SUM(TongTien) AS TongDoanhThu,
        AVG(TongTien) AS TrungBinhHoaDon
    FROM HoaDon
    WHERE CAST(ThoiGianLap AS DATE) BETWEEN @TuNgay AND @DenNgay
        AND TrangThai = N'Đã thanh toán'
    GROUP BY CAST(ThoiGianLap AS DATE)
    ORDER BY Ngay DESC
END
GO

-- Stored Procedure: Top sản phẩm bán chạy
CREATE PROCEDURE sp_TopSanPhamBanChay
    @Top INT = 10
AS
BEGIN
    SELECT TOP (@Top)
        sp.MaSP,
        sp.TenSP,
        SUM(ct.SoLuong) AS TongSoLuongBan,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM CTHoaDon ct
    INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
    INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
    WHERE hd.TrangThai = N'Đã thanh toán'
    GROUP BY sp.MaSP, sp.TenSP
    ORDER BY TongSoLuongBan DESC
END
GO

/* ================================
   5. KIỂM TRA DỮ LIỆU
================================ */
PRINT '========================================='
PRINT 'DATABASE QLCoffee ĐÃ ĐƯỢC TẠO THÀNH CÔNG'
PRINT '========================================='
PRINT ''
PRINT 'Thông tin tài khoản đăng nhập:'
PRINT '--------------------------------'
SELECT 
    tk.TaiKhoan,
    '123' AS MatKhau,
    CASE WHEN tk.Quyen = 1 THEN N'Quản lý' ELSE N'Nhân viên' END AS Quyen,
    nv.HoTen AS TenNhanVien
FROM TaiKhoan tk
INNER JOIN NhanVien nv ON tk.MaNV = nv.MaNV
PRINT ''
PRINT 'Tổng số bản ghi:'
PRINT '--------------------------------'
SELECT 'LoaiSanPham' AS BangDuLieu, COUNT(*) AS SoBanGhi FROM LoaiSanPham
UNION ALL
SELECT 'SanPham', COUNT(*) FROM SanPham
UNION ALL
SELECT 'NhanVien', COUNT(*) FROM NhanVien
UNION ALL
SELECT 'TaiKhoan', COUNT(*) FROM TaiKhoan
UNION ALL
SELECT 'HoaDon', COUNT(*) FROM HoaDon
UNION ALL
SELECT 'CTHoaDon', COUNT(*) FROM CTHoaDon
UNION ALL
SELECT 'ThongTinCuaHang', COUNT(*) FROM ThongTinCuaHang
GO

PRINT ''
PRINT '========================================='
PRINT 'SẴN SÀNG SỬ DỤNG!'
PRINT '========================================='
