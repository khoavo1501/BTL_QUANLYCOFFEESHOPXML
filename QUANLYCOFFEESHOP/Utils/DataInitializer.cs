using System;
using System.Xml.Linq;
using QUANLYCOFFEESHOP.DAL;
using QUANLYCOFFEESHOP.DTO;

namespace QUANLYCOFFEESHOP.Utils
{
    public static class DataInitializer
    {
        public static void InitializeDefaultData()
        {
            try
            {
                InitializeNhanVien();
                InitializeTaiKhoan();
                InitializeLoaiSanPham();
                InitializeSanPham();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("L?i kh?i t?o d? li?u: " + ex.Message);
            }
        }

        private static void InitializeNhanVien()
        {
            NhanVienDAL nvDAL = new NhanVienDAL();
            
            // Ki?m tra xem ?ã có nhân viên ch?a
            if (nvDAL.GetAll().Count > 0)
                return;

            // T?o nhân viên Admin
            NhanVienDTO admin = new NhanVienDTO
            {
                MaNV = "ADMIN",
                HoTen = "Administrator",
                GioiTinh = "Nam",
                NgaySinh = new DateTime(1990, 1, 1),
                SDT = "0123456789",
                DiaChi = "H? th?ng",
                NgayVaoLam = DateTime.Now
            };
            nvDAL.Insert(admin);

            // T?o nhân viên 1
            NhanVienDTO nv1 = new NhanVienDTO
            {
                MaNV = "NV01",
                HoTen = "Nguy?n V?n A",
                GioiTinh = "Nam",
                NgaySinh = new DateTime(1995, 5, 10),
                SDT = "0905123456",
                DiaChi = "?à N?ng",
                NgayVaoLam = new DateTime(2024, 1, 1)
            };
            nvDAL.Insert(nv1);

            // T?o nhân viên 2
            NhanVienDTO nv2 = new NhanVienDTO
            {
                MaNV = "NV02",
                HoTen = "Tr?n Th? B",
                GioiTinh = "N?",
                NgaySinh = new DateTime(1998, 8, 20),
                SDT = "0907654321",
                DiaChi = "?à N?ng",
                NgayVaoLam = new DateTime(2024, 2, 1)
            };
            nvDAL.Insert(nv2);
        }

        private static void InitializeTaiKhoan()
        {
            TaiKhoanDAL tkDAL = new TaiKhoanDAL();
            
            // Ki?m tra xem ?ã có tài kho?n ch?a
            if (tkDAL.GetAll().Count > 0)
                return;

            // M?t kh?u "123" ?ã mã hóa MD5
            string hashedPassword = Helper.MD5Hash("123");

            // T?o tài kho?n Admin
            TaiKhoanDTO admin = new TaiKhoanDTO
            {
                TaiKhoan = "admin",
                MatKhau = hashedPassword,
                Quyen = Constants.QUYEN_ADMIN,
                MaNV = "ADMIN"
            };
            tkDAL.Insert(admin);

            // T?o tài kho?n nhân viên 1
            TaiKhoanDTO tk1 = new TaiKhoanDTO
            {
                TaiKhoan = "nv01",
                MatKhau = hashedPassword,
                Quyen = Constants.QUYEN_NHANVIEN,
                MaNV = "NV01"
            };
            tkDAL.Insert(tk1);

            // T?o tài kho?n nhân viên 2
            TaiKhoanDTO tk2 = new TaiKhoanDTO
            {
                TaiKhoan = "nv02",
                MatKhau = hashedPassword,
                Quyen = Constants.QUYEN_NHANVIEN,
                MaNV = "NV02"
            };
            tkDAL.Insert(tk2);
        }

        private static void InitializeLoaiSanPham()
        {
            LoaiSanPhamDAL loaiDAL = new LoaiSanPhamDAL();
            
            // Ki?m tra xem ?ã có lo?i s?n ph?m ch?a
            if (loaiDAL.GetAll().Count > 0)
                return;

            // T?o lo?i Cà phê
            LoaiSanPhamDTO loai1 = new LoaiSanPhamDTO
            {
                MaLoai = "L01",
                TenLoai = "Cà phê",
                MoTa = "Các lo?i cà phê truy?n th?ng"
            };
            loaiDAL.Insert(loai1);

            // T?o lo?i Trà s?a
            LoaiSanPhamDTO loai2 = new LoaiSanPhamDTO
            {
                MaLoai = "L02",
                TenLoai = "Trà s?a",
                MoTa = "Trà s?a các lo?i"
            };
            loaiDAL.Insert(loai2);

            // T?o lo?i N??c ép
            LoaiSanPhamDTO loai3 = new LoaiSanPhamDTO
            {
                MaLoai = "L03",
                TenLoai = "N??c ép",
                MoTa = "N??c ép trái cây t??i"
            };
            loaiDAL.Insert(loai3);

            // T?o lo?i Sinh t?
            LoaiSanPhamDTO loai4 = new LoaiSanPhamDTO
            {
                MaLoai = "L04",
                TenLoai = "Sinh t?",
                MoTa = "Sinh t? các lo?i"
            };
            loaiDAL.Insert(loai4);
        }

        private static void InitializeSanPham()
        {
            SanPhamDAL spDAL = new SanPhamDAL();
            
            // Ki?m tra xem ?ã có s?n ph?m ch?a
            if (spDAL.GetAll().Count > 0)
                return;

            // Cà phê
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP01",
                TenSP = "Cà phê ?en",
                Gia = 25000,
                MaLoai = "L01",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP02",
                TenSP = "Cà phê s?a",
                Gia = 30000,
                MaLoai = "L01",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP03",
                TenSP = "B?c x?u",
                Gia = 28000,
                MaLoai = "L01",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP04",
                TenSP = "Cappuccino",
                Gia = 35000,
                MaLoai = "L01",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            // Trà s?a
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP05",
                TenSP = "Trà s?a truy?n th?ng",
                Gia = 35000,
                MaLoai = "L02",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP06",
                TenSP = "Trà s?a trân châu",
                Gia = 40000,
                MaLoai = "L02",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP07",
                TenSP = "Trà s?a matcha",
                Gia = 42000,
                MaLoai = "L02",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            // N??c ép
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP08",
                TenSP = "N??c ép cam",
                Gia = 30000,
                MaLoai = "L03",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP09",
                TenSP = "N??c ép d?a h?u",
                Gia = 25000,
                MaLoai = "L03",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP10",
                TenSP = "N??c ép táo",
                Gia = 35000,
                MaLoai = "L03",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            // Sinh t?
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP11",
                TenSP = "Sinh t? b?",
                Gia = 40000,
                MaLoai = "L04",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP12",
                TenSP = "Sinh t? dâu",
                Gia = 38000,
                MaLoai = "L04",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP13",
                TenSP = "Sinh t? xoài",
                Gia = 38000,
                MaLoai = "L04",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });
        }

        public static bool HasData()
        {
            try
            {
                TaiKhoanDAL tkDAL = new TaiKhoanDAL();
                return tkDAL.GetAll().Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
