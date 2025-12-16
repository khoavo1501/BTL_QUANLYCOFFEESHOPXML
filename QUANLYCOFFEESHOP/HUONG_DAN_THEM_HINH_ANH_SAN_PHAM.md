# H??ng D?n Thêm Hình ?nh S?n Ph?m

## T?ng quan
?ã thêm ch?c n?ng qu?n lý hình ?nh cho s?n ph?m trong h? th?ng. Ng??i dùng có th? ch?n, l?u và hi?n th? hình ?nh cho t?ng s?n ph?m.

## Các thay ??i ?ã th?c hi?n

### 1. C?p nh?t SanPhamDTO.cs
? **Thêm thu?c tính HinhAnh**
```csharp
public string HinhAnh { get; set; }
```

? **Thêm constructor m?i**
```csharp
public SanPhamDTO(string maSP, string tenSP, int gia, string maLoai, string trangThai, string hinhAnh)
{
    MaSP = maSP;
    TenSP = tenSP;
    Gia = gia;
    MaLoai = maLoai;
    TrangThai = trangThai;
    HinhAnh = hinhAnh;
}
```

### 2. C?p nh?t SanPhamDAL.cs
? **C?p nh?t t?t c? ph??ng th?c**:
- `GetAll()`: ??c HinhAnh t? XML
- `GetByID()`: ??c HinhAnh t? XML
- `Insert()`: L?u HinhAnh vào XML
- `Update()`: C?p nh?t HinhAnh trong XML
- `Search()`: ??c HinhAnh t? XML
- `GetByCategory()`: ??c HinhAnh t? XML
- `GetAvailableProducts()`: ??c HinhAnh t? XML

### 3. C?p nh?t frmSanPham.Designer.cs
? **Thêm controls m?i**:
- `PictureBox picHinhAnh`: Hi?n th? hình ?nh s?n ph?m
- `Button btnChonHinh`: Nút ch?n hình ?nh
- `Label label7`: Nhãn "Hình ?nh:"

? **?i?u ch?nh layout**:
- T?ng chi?u cao `groupBox1` t? 200 ? 250 pixels
- Thêm PictureBox (100x100 pixels) và nút Ch?n hình

### 4. C?p nh?t frmSanPham.cs
? **Thêm bi?n private**:
```csharp
private string selectedImagePath = "";
```

? **Thêm ph??ng th?c m?i**:

**btnChonHinh_Click()**: M? h?p tho?i ch?n file hình ?nh
```csharp
private void btnChonHinh_Click(object sender, EventArgs e)
{
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
        openFileDialog.Title = "Ch?n hình ?nh s?n ph?m";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            selectedImagePath = openFileDialog.FileName;
            LoadHinhAnh(selectedImagePath);
        }
    }
}
```

**LoadHinhAnh()**: Hi?n th? hình ?nh trong PictureBox
```csharp
private void LoadHinhAnh(string imagePath)
{
    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
    {
        using (var stream = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            picHinhAnh.Image = System.Drawing.Image.FromStream(stream);
        }
    }
    else
    {
        picHinhAnh.Image = null;
    }
}
```

? **C?p nh?t ph??ng th?c hi?n có**:
- `dgvSanPham_CellClick()`: Hi?n th? hình ?nh khi click vào s?n ph?m
- `btnThem_Click()`: L?u ???ng d?n hình ?nh khi thêm
- `btnSua_Click()`: C?p nh?t ???ng d?n hình ?nh khi s?a
- `ResetForm()`: Xóa hình ?nh khi reset
- `LoadDataGridView()`: ?n c?t HinhAnh trong DataGridView

### 5. C?p nh?t frmBanHang.cs
? **Thêm ph??ng th?c CreateProductPanel()**: T?o panel hi?n th? s?n ph?m v?i hình ?nh
```csharp
private Panel CreateProductPanel(SanPhamDTO sp)
{
    Panel pnlSP = new Panel
    {
        Width = 150,
        Height = 220,  // T?ng t? 180 lên 220 ?? ch?a hình ?nh
        BorderStyle = BorderStyle.FixedSingle,
        Margin = new Padding(10),
        Cursor = Cursors.Hand,
        Tag = sp
    };

    PictureBox picSP = new PictureBox
    {
        Dock = DockStyle.Top,
        Height = 100,
        SizeMode = PictureBoxSizeMode.Zoom
    };
    
    // Load hình ?nh n?u có
    if (!string.IsNullOrEmpty(sp.HinhAnh) && System.IO.File.Exists(sp.HinhAnh))
    {
        using (var stream = new System.IO.FileStream(sp.HinhAnh, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            picSP.Image = System.Drawing.Image.FromStream(stream);
        }
    }

    // Thêm Label tên, giá và Button...
    pnlSP.Controls.Add(lblGia);
    pnlSP.Controls.Add(lblTen);
    pnlSP.Controls.Add(picSP);
    pnlSP.Controls.Add(btnThem);

    return pnlSP;
}
```

? **C?p nh?t ph??ng th?c**:
- `LoadSanPham()`: S? d?ng `CreateProductPanel()`
- `btnTimKiem_Click()`: S? d?ng `CreateProductPanel()`

## Cách s? d?ng

### 1. Thêm s?n ph?m m?i v?i hình ?nh
1. M? form **Qu?n lý s?n ph?m**
2. Nh?p thông tin s?n ph?m (Mã SP, Tên SP, Giá, Lo?i SP, Tr?ng thái)
3. Click nút **"Ch?n hình"**
4. Ch?n file hình ?nh t? máy tính (h? tr?: .jpg, .jpeg, .png, .gif, .bmp)
5. Hình ?nh s? hi?n th? trong PictureBox
6. Click **"Thêm"** ?? l?u s?n ph?m

### 2. C?p nh?t hình ?nh s?n ph?m
1. Click vào s?n ph?m trong DataGridView
2. Hình ?nh hi?n t?i s? hi?n th? (n?u có)
3. Click **"Ch?n hình"** ?? ch?n hình ?nh m?i
4. Click **"S?a"** ?? c?p nh?t

### 3. Xóa hình ?nh s?n ph?m
1. Click vào s?n ph?m trong DataGridView
2. Click **"Ch?n hình"** nh?ng không ch?n file (Cancel)
3. Click **"S?a"** ? Hình ?nh s? b? xóa

### 4. Xem s?n ph?m trong form Bán hàng
1. M? form **Bán hàng**
2. S?n ph?m s? hi?n th? d?ng card v?i:
   - **Hình ?nh** (phía trên, 100x100 pixels)
   - **Tên s?n ph?m**
   - **Giá**
   - **Nút "Thêm vào gi?"** (phía d??i)

## L?u ý quan tr?ng

### ?? V? ???ng d?n hình ?nh
- H? th?ng l?u **???ng d?n tuy?t ??i** c?a file hình ?nh vào XML
- **Không copy** file hình ?nh vào th? m?c d? án
- N?u di chuy?n ho?c xóa file hình ?nh g?c, hình ?nh s? không hi?n th? ???c

### ? Khuy?n ngh?
**T?o th? m?c l?u hình ?nh c? ??nh**:
```
D:\WorkSpace\XML\QUANLYCOFFEESHOP\Images\
```

Ho?c t?o th? m?c trong project:
```
QUANLYCOFFEESHOP\Images\
```

### ?? C?i thi?n trong t??ng lai (optional)

#### 1. Copy hình ?nh vào th? m?c project
```csharp
private string CopyImageToProject(string sourcePath)
{
    string imagesFolder = Path.Combine(Application.StartupPath, "Images");
    if (!Directory.Exists(imagesFolder))
        Directory.CreateDirectory(imagesFolder);
    
    string fileName = Path.GetFileName(sourcePath);
    string destPath = Path.Combine(imagesFolder, fileName);
    
    File.Copy(sourcePath, destPath, true);
    return destPath;
}
```

#### 2. L?u hình ?nh d?ng Base64 trong XML
```csharp
// Convert hình ?nh sang Base64
private string ImageToBase64(string imagePath)
{
    byte[] imageBytes = File.ReadAllBytes(imagePath);
    return Convert.ToBase64String(imageBytes);
}

// Convert Base64 v? hình ?nh
private Image Base64ToImage(string base64String)
{
    byte[] imageBytes = Convert.FromBase64String(base64String);
    using (var ms = new MemoryStream(imageBytes))
    {
        return Image.FromStream(ms);
    }
}
```

#### 3. Resize hình ?nh t? ??ng
```csharp
private Image ResizeImage(Image img, int maxWidth, int maxHeight)
{
    double ratioX = (double)maxWidth / img.Width;
    double ratioY = (double)maxHeight / img.Height;
    double ratio = Math.Min(ratioX, ratioY);

    int newWidth = (int)(img.Width * ratio);
    int newHeight = (int)(img.Height * ratio);

    Bitmap newImage = new Bitmap(newWidth, newHeight);
    using (Graphics graphics = Graphics.FromImage(newImage))
    {
        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        graphics.DrawImage(img, 0, 0, newWidth, newHeight);
    }
    return newImage;
}
```

## C?u trúc XML

### Tr??c khi thêm hình ?nh:
```xml
<SanPhams>
  <SanPham>
    <MaSP>SP001</MaSP>
    <TenSP>Cà phê ?en</TenSP>
    <Gia>25000</Gia>
    <MaLoai>L001</MaLoai>
    <TrangThai>Còn bán</TrangThai>
  </SanPham>
</SanPhams>
```

### Sau khi thêm hình ?nh:
```xml
<SanPhams>
  <SanPham>
    <MaSP>SP001</MaSP>
    <TenSP>Cà phê ?en</TenSP>
    <Gia>25000</Gia>
    <MaLoai>L001</MaLoai>
    <TrangThai>Còn bán</TrangThai>
    <HinhAnh>D:\Images\cafe-den.jpg</HinhAnh>
  </SanPham>
</SanPhams>
```

## ??nh d?ng hình ?nh h? tr?
- ? .jpg / .jpeg
- ? .png
- ? .gif
- ? .bmp

## K?t qu?

### Form Qu?n lý S?n ph?m
```
???????????????????????????????????????????????????????????
?  Thông tin s?n ph?m                                     ?
???????????????????????????????????????????????????????????
?  Mã SP:     [SP001]                  Tên SP: [Cà phê ?en] ?
?  Giá:       [25000]                  Lo?i SP: [?? u?ng]    ?
?  Hình ?nh:  [Ch?n hình]  [   Hình ?nh   ]                 ?
?                          [   100x100    ]                 ?
?                          [    Preview   ]                 ?
?                                                           ?
?           [Thêm] [S?a] [Xóa]                             ?
?           [Làm m?i] [Xu?t XML]                           ?
???????????????????????????????????????????????????????????
```

### Form Bán hàng
```
????????? ????????? ?????????
? [IMG] ? ? [IMG] ? ? [IMG] ?
? Cà phê? ? Trà s?a? ?  Sinh ?
?  ?en  ? ?       ? ?  t?   ?
? 25,000? ? 30,000? ? 35,000?
?[Thêm] ? ?[Thêm] ? ?[Thêm] ?
????????? ????????? ?????????
```

## Ki?m tra

### 1. Test thêm s?n ph?m v?i hình ?nh
- [x] Ch?n ???c file hình ?nh
- [x] Hi?n th? preview hình ?nh
- [x] L?u ???ng d?n vào XML
- [x] Hi?n th? hình ?nh khi click vào DataGridView

### 2. Test c?p nh?t hình ?nh
- [x] Thay ??i ???c hình ?nh
- [x] C?p nh?t ???ng d?n trong XML

### 3. Test form Bán hàng
- [x] Hi?n th? hình ?nh trên card s?n ph?m
- [x] Hình ?nh Scale ?úng t? l? (Zoom)
- [x] Không b? l?i n?u file hình ?nh không t?n t?i

## Troubleshooting

### ? Hình ?nh không hi?n th?
**Nguyên nhân**: File hình ?nh không t?n t?i ho?c ???ng d?n sai
**Gi?i pháp**: Ki?m tra ???ng d?n file trong XML

### ? L?i "File is being used by another process"
**Nguyên nhân**: File ?ang ???c m? b?i ch??ng trình khác
**Gi?i pháp**: S? d?ng `FileStream` v?i `FileAccess.Read`

### ? Hình ?nh b? méo
**Nguyên nhân**: SizeMode không ?úng
**Gi?i pháp**: S? d?ng `SizeMode = PictureBoxSizeMode.Zoom`

---
**Ngày t?o**: 2024
**Ng??i th?c hi?n**: GitHub Copilot
**Phiên b?n**: 1.0
