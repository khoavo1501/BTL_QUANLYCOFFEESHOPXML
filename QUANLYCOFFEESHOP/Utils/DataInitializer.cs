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
                System.Windows.Forms.MessageBox.Show("Lỗi khởi tạo dữ liệu: " + ex.Message);
            }
        }

        private static void InitializeNhanVien()
        {
            NhanVienDAL nvDAL = new NhanVienDAL();
            
            // Kiểm tra xem đã có nhân viên chưa
            if (nvDAL.GetAll().Count > 0)
                return;

            // Tạo nhân viên Admin
            NhanVienDTO admin = new NhanVienDTO
            {
                MaNV = "ADMIN",
                HoTen = "Administrator",
                GioiTinh = "Nam",
                NgaySinh = new DateTime(1990, 1, 1),
                SDT = "0123456789",
                DiaChi = "Hệ thống",
                NgayVaoLam = DateTime.Now
            };
            nvDAL.Insert(admin);

            // Tạo nhân viên 1
            NhanVienDTO nv1 = new NhanVienDTO
            {
                MaNV = "NV01",
                HoTen = "Nguyễn Văn A",
                GioiTinh = "Nam",
                NgaySinh = new DateTime(1995, 5, 10),
                SDT = "0905123456",
                DiaChi = "Đà Nẵng",
                NgayVaoLam = new DateTime(2024, 1, 1)
            };
            nvDAL.Insert(nv1);

            // Tạo nhân viên 2
            NhanVienDTO nv2 = new NhanVienDTO
            {
                MaNV = "NV02",
                HoTen = "Trần Thị B",
                GioiTinh = "Nữ",
                NgaySinh = new DateTime(1998, 8, 20),
                SDT = "0907654321",
                DiaChi = "Đà Nẵng",
                NgayVaoLam = new DateTime(2024, 2, 1)
            };
            nvDAL.Insert(nv2);
        }

        private static void InitializeTaiKhoan()
        {
            TaiKhoanDAL tkDAL = new TaiKhoanDAL();
            
            // Kiểm tra xem đã có tài khoản chưa
            if (tkDAL.GetAll().Count > 0)
                return;

            // Mật khẩu "123" đã mã hóa MD5
            string hashedPassword = Helper.MD5Hash("123");

            // Tạo tài khoản Admin
            TaiKhoanDTO admin = new TaiKhoanDTO
            {
                TaiKhoan = "admin",
                MatKhau = hashedPassword,
                Quyen = Constants.QUYEN_ADMIN,
                MaNV = "ADMIN"
            };
            tkDAL.Insert(admin);

            // Tạo tài khoản nhân viên 1
            TaiKhoanDTO tk1 = new TaiKhoanDTO
            {
                TaiKhoan = "nv01",
                MatKhau = hashedPassword,
                Quyen = Constants.QUYEN_NHANVIEN,
                MaNV = "NV01"
            };
            tkDAL.Insert(tk1);

            // Tạo tài khoản nhân viên 2
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
            
            // Kiểm tra xem đã có loại sản phẩm chưa
            if (loaiDAL.GetAll().Count > 0)
                return;

            // Tạo loại Cà phê
            LoaiSanPhamDTO loai1 = new LoaiSanPhamDTO
            {
                MaLoai = "L01",
                TenLoai = "Cà phê",
                MoTa = "Các loại cà phê truyền thống"
            };
            loaiDAL.Insert(loai1);

            // Tạo loại Trà sữa
            LoaiSanPhamDTO loai2 = new LoaiSanPhamDTO
            {
                MaLoai = "L02",
                TenLoai = "Trà sữa",
                MoTa = "Trà sữa các loại"
            };
            loaiDAL.Insert(loai2);

            // Tạo loại Nước ép
            LoaiSanPhamDTO loai3 = new LoaiSanPhamDTO
            {
                MaLoai = "L03",
                TenLoai = "Nước ép",
                MoTa = "Nước ép trái cây tươi"
            };
            loaiDAL.Insert(loai3);

            // Tạo loại Sinh tố
            LoaiSanPhamDTO loai4 = new LoaiSanPhamDTO
            {
                MaLoai = "L04",
                TenLoai = "Sinh tố",
                MoTa = "Sinh tố các loại"
            };
            loaiDAL.Insert(loai4);
        }

        private static void InitializeSanPham()
        {
            SanPhamDAL spDAL = new SanPhamDAL();
            
            // Kiểm tra xem đã có sản phẩm chưa
            if (spDAL.GetAll().Count > 0)
                return;

            // Cà phê
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP01",
                TenSP = "Cà phê đen",
                Gia = 25000,
                MaLoai = "L01",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP02",
                TenSP = "Cà phê sữa",
                Gia = 30000,
                MaLoai = "L01",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP03",
                TenSP = "Bạc xỉu",
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

            // Trà sữa
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP05",
                TenSP = "Trà sữa truyền thống",
                Gia = 35000,
                MaLoai = "L02",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP06",
                TenSP = "Trà sữa trân châu",
                Gia = 40000,
                MaLoai = "L02",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP07",
                TenSP = "Trà sữa matcha",
                Gia = 42000,
                MaLoai = "L02",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            // Nước ép
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP08",
                TenSP = "Nước ép cam",
                Gia = 30000,
                MaLoai = "L03",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP09",
                TenSP = "Nước ép dưa hấu",
                Gia = 25000,
                MaLoai = "L03",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP10",
                TenSP = "Nước ép táo",
                Gia = 35000,
                MaLoai = "L03",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            // Sinh tố
            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP11",
                TenSP = "Sinh tố bơ",
                Gia = 40000,
                MaLoai = "L04",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP12",
                TenSP = "Sinh tố dâu",
                Gia = 38000,
                MaLoai = "L04",
                TrangThai = Constants.TRANGTHAI_CONBAN
            });

            spDAL.Insert(new SanPhamDTO
            {
                MaSP = "SP13",
                TenSP = "Sinh tố xoài",
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
