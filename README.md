# ? QU?N LÝ COFFEE SHOP - XML PROJECT

H? th?ng qu?n lý c?a hàng Coffee Shop s? d?ng XML làm c? s? d? li?u.

## ?? Tính n?ng

- ? Qu?n lý s?n ph?m (CRUD)
- ? Qu?n lý lo?i s?n ph?m
- ? Qu?n lý nhân viên
- ? Qu?n lý tài kho?n (Admin/Nhân viên)
- ? Bán hàng (POS)
- ? Qu?n lý hóa ??n
- ? Báo cáo doanh thu
- ? T? ??ng kh?i t?o d? li?u m?u

## ??? Công ngh?

- **Framework**: .NET Framework 4.7.2
- **Ngôn ng?**: C# 7.3
- **Database**: XML Files
- **UI**: Windows Forms
- **IDE**: Visual Studio 2022

## ?? Cài ??t

### Yêu c?u
- Windows 7/8/10/11
- .NET Framework 4.7.2 tr? lên
- Visual Studio 2019/2022 (?? build)

### B??c 1: Clone repository
```bash
git clone https://github.com/khoavo1501/BTL_QUANLYCOFFEESHOPXML.git
cd BTL_QUANLYCOFFEESHOPXML
```

### B??c 2: M? solution
- M? file `QUANLYCOFFEESHOP.sln` b?ng Visual Studio

### B??c 3: Build
- Nh?n `Ctrl + Shift + B` ho?c
- Menu: `Build` ? `Build Solution`

### B??c 4: Ch?y
- Nh?n `F5` ho?c
- Menu: `Debug` ? `Start Debugging`

## ?? Tài kho?n m?c ??nh

| Username | Password | Quy?n |
|----------|----------|-------|
| admin    | 123      | Qu?n lý |
| nv01     | 123      | Nhân viên |
| nv02     | 123      | Nhân viên |

## ?? C?u trúc d? án

```
QUANLYCOFFEESHOP/
??? DAL/                  # Data Access Layer
?   ??? XMLHelper.cs      # X? lý XML
?   ??? DatabaseHelper.cs # SQL Server (optional)
?   ??? TaiKhoanDAL.cs
?   ??? NhanVienDAL.cs
?   ??? SanPhamDAL.cs
?   ??? LoaiSanPhamDAL.cs
?   ??? HoaDonDAL.cs
?   ??? CTHoaDonDAL.cs
?
??? DTO/                  # Data Transfer Objects
?   ??? TaiKhoanDTO.cs
?   ??? NhanVienDTO.cs
?   ??? SanPhamDTO.cs
?   ??? LoaiSanPhamDTO.cs
?   ??? HoaDonDTO.cs
?   ??? CTHoaDonDTO.cs
?
??? GUI/                  # User Interface
?   ??? frmLogin.cs
?   ??? frmMain.cs
?   ??? frmBanHang.cs
?   ??? frmSanPham.cs
?   ??? frmLoaiSanPham.cs
?   ??? frmNhanVien.cs
?   ??? frmTaiKhoan.cs
?   ??? frmHoaDon.cs
?   ??? frmDoiMatKhau.cs
?
??? Utils/                # Utilities
?   ??? Constants.cs
?   ??? Helper.cs
?   ??? Validator.cs
?   ??? SessionManager.cs
?   ??? DataInitializer.cs
?
??? Data/                 # XML Database (auto-generated)
?   ??? TaiKhoan.xml
?   ??? NhanVien.xml
?   ??? SanPham.xml
?   ??? LoaiSanPham.xml
?   ??? HoaDon.xml
?   ??? CTHoaDon.xml
?
??? Program.cs            # Entry point
```

## ?? Database Schema (XML)

### TaiKhoan.xml
```xml
<TaiKhoans>
  <TaiKhoan>
    <TaiKhoan>admin</TaiKhoan>
    <MatKhau>202cb962ac59075b964b07152d234b70</MatKhau>
    <Quyen>1</Quyen>
    <MaNV>ADMIN</MaNV>
  </TaiKhoan>
</TaiKhoans>
```

### SanPham.xml
```xml
<SanPhams>
  <SanPham>
    <MaSP>SP01</MaSP>
    <TenSP>Cà phê ?en</TenSP>
    <Gia>25000</Gia>
    <MaLoai>L01</MaLoai>
    <TrangThai>Còn bán</TrangThai>
  </SanPham>
</SanPhams>
```

## ?? Commit và Push

### Cách 1: S? d?ng script t? ??ng (Khuy?n ngh?)
```bash
# Ch?y file batch
commit_changes.bat
```

### Cách 2: Th? công
```bash
# Stage file
git add QUANLYCOFFEESHOP/*.cs
git add QUANLYCOFFEESHOP/DAL/*.cs
git add QUANLYCOFFEESHOP/DTO/*.cs
git add QUANLYCOFFEESHOP/GUI/*.cs
git add .gitignore

# Commit
git commit -m "Fix: Mô t? thay ??i"

# Push
git push origin main
```

## ?? Các l?i ?ã kh?c ph?c

### v2.0.3 (2025-01-15)
- ? Fix: L?i mã hóa ??n trùng khi thanh toán
  - S?a thu?t toán `GenerateNewMaHD()` 
  - T? ??ng t?o l?i mã n?u phát hi?n trùng

- ? Fix: L?i Foreign Key Constraint khi backup
  - T?t backup t? ??ng vào SQL Server
  - X? lý ?úng th? t? xóa/thêm (Parent-Child)

- ? Fix: ComboBox không c?p nh?t khi thêm lo?i SP m?i
  - Thêm `OnActivated()` ?? t? ??ng reload
  - Gi? l?i item ?ang ch?n sau khi reload

- ? Fix: Không th?y hóa ??n sau thanh toán
  - Xóa export XML không c?n thi?t
  - D? li?u ?ã l?u tr?c ti?p vào XML

- ? Add: T? ??ng kh?i t?o d? li?u m?u
  - Class `DataInitializer`
  - T? ??ng ch?y l?n ??u

### v1.0.0 (2025-01-10)
- ? Kh?i t?o d? án
- ? CRUD c? b?n
- ? Qu?n lý XML

## ?? H??ng d?n chi ti?t

Xem các file tài li?u:
- `HUONG_DAN_SU_DUNG.txt` - H??ng d?n s? d?ng
- `HUONG_DAN_GIT_COMMIT.txt` - H??ng d?n Git
- `FIX_LOI_MA_HOA_DON.txt` - Fix l?i mã hóa ??n
- `FIX_LOI_FOREIGN_KEY.txt` - Fix l?i FK
- `FIX_LOI_COMBOBOX_KHONG_CAP_NHAT.txt` - Fix l?i ComboBox

## ?? Tác gi?

- **Võ Anh Khoa** - [khoavo1501](https://github.com/khoavo1501)
- Email: khoamiden15012005@gmail.com

## ?? License

MIT License - Xem file [LICENSE](LICENSE) ?? bi?t thêm chi ti?t

## ?? ?óng góp

M?i ?óng góp ??u ???c chào ?ón! Vui lòng:
1. Fork repository
2. T?o branch m?i (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add: AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. T?o Pull Request

## ?? Liên h?

- GitHub: [@khoavo1501](https://github.com/khoavo1501)
- Email: khoamiden15012005@gmail.com
- Project Link: [https://github.com/khoavo1501/BTL_QUANLYCOFFEESHOPXML](https://github.com/khoavo1501/BTL_QUANLYCOFFEESHOPXML)

---

**© 2025 Coffee Shop XML Management System**  
**Made with ?? and ?**
