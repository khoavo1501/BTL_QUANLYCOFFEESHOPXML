# H??ng D?n: Ch?nh S?a Ch?c N?ng ??ng Xu?t

## V?n ?? ban ??u

Khi ng??i dùng click **??ng xu?t**, h? th?ng:
1. ?óng form Main
2. T?o form Login **M?I** và hi?n th?
3. ? **V?n ??**: Form Login m?i này không ph?i là form chính ???c kh?i t?o t? `Application.Run()`, nên khi ?óng nó, toàn b? ?ng d?ng có th? k?t thúc

## Gi?i pháp

### Lu?ng ho?t ??ng m?i:

```
Program.cs
    ?
[1] Application.Run(new frmLogin())  ? Form chính
    ?
[2] ??ng nh?p thành công
    ?
[3] frmLogin.Hide()
    ?
[4] frmMain.ShowDialog()  ? M? d?ng modal
    ?
[5] Ng??i dùng click "??ng xu?t"
    ?
[6] SessionManager.Logout()
    ?
[7] frmMain.Close()  ? ?óng dialog
    ?
[8] Quay l?i frmLogin (t? ??ng)
    ?
[9] frmLogin.Show()  ? Hi?n th? l?i
    ?
[10] Reset textbox và focus
    ?
[11] Có th? ??ng nh?p l?i
```

## Các thay ??i ?ã th?c hi?n

### 1. File: `frmMain.cs`

#### Tr??c:
```csharp
private void mnuDangXuat_Click(object sender, EventArgs e)
{
    DialogResult result = MessageBox.Show("B?n có ch?c mu?n ??ng xu?t?", "Xác nh?n",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

    if (result == DialogResult.Yes)
    {
        SessionManager.Logout();
        this.Close();
        frmLogin loginForm = new frmLogin();  // ? T?o form Login M?I
        loginForm.Show();                       // ? Hi?n th? form m?i
    }
}
```

#### Sau:
```csharp
private void mnuDangXuat_Click(object sender, EventArgs e)
{
    DialogResult result = MessageBox.Show("B?n có ch?c mu?n ??ng xu?t?", "Xác nh?n",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

    if (result == DialogResult.Yes)
    {
        SessionManager.Logout();
        this.Close();  // ? Ch? ?óng form Main
                       // ? Form Login s? t? ??ng hi?n th? l?i
    }
}
```

**Gi?i thích**:
- Xóa b? 2 dòng t?o và hi?n th? form Login m?i
- Ch? c?n ?óng `frmMain`
- Do `frmMain` ???c m? b?ng `ShowDialog()` trong `frmLogin`, khi ?óng nó s? t? ??ng quay v? `frmLogin`

### 2. File: `frmLogin.cs`

#### Tr??c:
```csharp
if (user != null)
{
    SessionManager.CurrentUser = user;
    SessionManager.CurrentEmployee = nhanVienDAL.GetByID(user.MaNV);

    MessageBox.Show("??ng nh?p thành công!", "Thông báo", 
        MessageBoxButtons.OK, MessageBoxIcon.Information);

    this.Hide();
    frmMain mainForm = new frmMain();
    mainForm.ShowDialog();
    this.Close();  // ? ?óng form Login sau khi ?óng Main
}
```

#### Sau:
```csharp
if (user != null)
{
    SessionManager.CurrentUser = user;
    SessionManager.CurrentEmployee = nhanVienDAL.GetByID(user.MaNV);

    MessageBox.Show("??ng nh?p thành công!", "Thông báo", 
        MessageBoxButtons.OK, MessageBoxIcon.Information);

    this.Hide();
    frmMain mainForm = new frmMain();
    mainForm.ShowDialog();
    
    // ? Khi ?óng frmMain (??ng xu?t), hi?n th? l?i form Login
    this.Show();
    txtPassword.Clear();
    txtUsername.Clear();
    txtUsername.Focus();
}
```

**Gi?i thích**:
- Thay `this.Close()` b?ng `this.Show()` ?? hi?n th? l?i form Login
- Reset các textbox ?? s?n sàng ??ng nh?p l?i:
  - `txtPassword.Clear()`: Xóa m?t kh?u
  - `txtUsername.Clear()`: Xóa tên ??ng nh?p
  - `txtUsername.Focus()`: ??t focus vào ô tên ??ng nh?p

## Cách ho?t ??ng chi ti?t

### Khi ?ng d?ng kh?i ??ng:

```csharp
// Program.cs
Application.Run(new frmLogin());
```
- Form Login ???c t?o và là **form chính** c?a ?ng d?ng
- ?ng d?ng s? ch?y cho ??n khi form này ?óng

### Khi ??ng nh?p thành công:

```csharp
// frmLogin.cs
this.Hide();                    // ?n form Login
frmMain mainForm = new frmMain();
mainForm.ShowDialog();          // M? form Main (Modal)
// Code d?ng ? ?ây cho ??n khi ?óng frmMain
this.Show();                    // Sau khi ?óng frmMain, ti?p t?c ? ?ây
```

**ShowDialog()**: Là ph??ng th?c modal
- Ch?n lu?ng th?c thi
- Ch? ti?p t?c khi form ???c ?óng
- Hoàn h?o cho tr??ng h?p này!

### Khi ??ng xu?t:

```csharp
// frmMain.cs
SessionManager.Logout();
this.Close();  // ?óng frmMain ? ShowDialog() k?t thúc
```

? Code trong `frmLogin.cs` ti?p t?c ch?y:
```csharp
this.Show();           // Hi?n th? l?i form Login
txtPassword.Clear();    // Xóa m?t kh?u
txtUsername.Clear();    // Xóa tên ??ng nh?p
txtUsername.Focus();    // Focus vào textbox
```

## L?i ích c?a gi?i pháp này

### ? ??n gi?n
- Không c?n t?o form Login m?i
- S? d?ng l?i form Login g?c
- Ít code h?n

### ? An toàn
- Form Login v?n là form chính (`Application.Run`)
- Không b? tình tr?ng ?ng d?ng k?t thúc ??t ng?t
- Lu?ng ?i?u khi?n rõ ràng

### ? Tr?i nghi?m ng??i dùng t?t
- Form Login xu?t hi?n ngay l?p t?c
- Các textbox ???c reset s?ch s?
- Focus t? ??ng vào ô nh?p tên ??ng nh?p
- S?n sàng ??ng nh?p l?i

### ? Qu?n lý b? nh? t?t
- Không t?o nhi?u instance c?a form Login
- Gi?i phóng tài nguyên c?a form Main khi ??ng xu?t

## K?ch b?n test

### Test 1: ??ng xu?t bình th??ng
1. ??ng nh?p v?i tài kho?n: `admin / 123`
2. Form Main hi?n th?
3. Click menu **H? th?ng** ? **??ng xu?t**
4. Xác nh?n ??ng xu?t
5. ? **K? v?ng**: Form Login hi?n th? l?i v?i textbox tr?ng

### Test 2: ??ng nh?p l?i sau khi ??ng xu?t
1. ??ng nh?p v?i tài kho?n: `admin / 123`
2. ??ng xu?t
3. ??ng nh?p l?i v?i tài kho?n: `nv01 / 123`
4. ? **K? v?ng**: ??ng nh?p thành công v?i tài kho?n m?i

### Test 3: H?y ??ng xu?t
1. ??ng nh?p thành công
2. Click menu **??ng xu?t**
3. Click **No** trong h?p tho?i xác nh?n
4. ? **K? v?ng**: Form Main v?n m?, không ??ng xu?t

### Test 4: ??ng xu?t nhi?u l?n
1. ??ng nh?p ? ??ng xu?t ? ??ng nh?p ? ??ng xu?t ? ??ng nh?p
2. ? **K? v?ng**: M?i l?n ??u ho?t ??ng bình th??ng

## So sánh v?i gi?i pháp khác

### Gi?i pháp 1: T?o form Login m?i (C? - Có v?n ??)
```csharp
SessionManager.Logout();
this.Close();
frmLogin loginForm = new frmLogin();
loginForm.Show();
```
? **V?n ??**:
- T?o nhi?u instance form Login
- Form Login m?i không ph?i là form chính
- Có th? gây l?i khi ?óng

### Gi?i pháp 2: Hide/Show (?ã ch?n - T?t nh?t)
```csharp
// frmMain
SessionManager.Logout();
this.Close();

// frmLogin
mainForm.ShowDialog();
this.Show();  // T? ??ng hi?n th? l?i
```
? **?u ?i?m**:
- S? d?ng l?i form Login g?c
- Lu?ng ?i?u khi?n rõ ràng
- ??n gi?n và hi?u qu?

### Gi?i pháp 3: Application.Restart() (Quá m?nh)
```csharp
SessionManager.Logout();
Application.Restart();
```
? **Nh??c ?i?m**:
- Kh?i ??ng l?i toàn b? ?ng d?ng
- M?t th?i gian
- Có th? m?t d? li?u ch?a l?u

### Gi?i pháp 4: Singleton Pattern
```csharp
public static frmLogin Instance { get; private set; }

// Program.cs
Application.Run(frmLogin.Instance = new frmLogin());

// frmMain
frmLogin.Instance.Show();
```
? **?u ?i?m**: ??m b?o ch? có 1 instance
? **Nh??c ?i?m**: Ph?c t?p h?n, không c?n thi?t trong tr??ng h?p này

## L?u ý khi s? d?ng

### 1. ShowDialog() vs Show()
- **ShowDialog()**: Modal, ch?n lu?ng th?c thi
  - Dùng cho form Main
  - ??m b?o form Login ch? ??n khi Main ?óng
  
- **Show()**: Non-modal, không ch?n
  - Dùng khi mu?n m? nhi?u form cùng lúc
  - Không phù h?p cho tr??ng h?p này

### 2. SessionManager.Logout()
??m b?o ph??ng th?c này:
- Xóa thông tin user hi?n t?i
- Xóa thông tin nhân viên
- Reset các bi?n session

```csharp
public static void Logout()
{
    CurrentUser = null;
    CurrentEmployee = null;
    // Reset các bi?n khác n?u có
}
```

### 3. Reset form Login
Quan tr?ng ?? reset:
- `txtPassword.Clear()`: B?o m?t, không gi? m?t kh?u
- `txtUsername.Clear()`: Tránh ??ng nh?p nh?m
- `txtUsername.Focus()`: UX t?t, s?n sàng nh?p

## Troubleshooting

### ? Form Login không hi?n th? l?i
**Nguyên nhân**: Có th? do `this.Close()` trong frmLogin
**Gi?i pháp**: ??m b?o ?ã thay `this.Close()` thành `this.Show()`

### ? ?ng d?ng k?t thúc khi ??ng xu?t
**Nguyên nhân**: Form Login b? ?óng
**Gi?i pháp**: Ki?m tra không có `Application.Exit()` ho?c `this.Close()` trong frmLogin

### ? Textbox không reset
**Nguyên nhân**: Quên g?i `.Clear()`
**Gi?i pháp**: Thêm code clear sau `this.Show()`

## K?t lu?n

? **Gi?i pháp hoàn ch?nh**:
- S? d?ng `ShowDialog()` ?? form Login ch? form Main ?óng
- Khi ??ng xu?t, ch? c?n ?óng form Main
- Form Login t? ??ng hi?n th? l?i và reset textbox

? **??n gi?n, hi?u qu?, d? maintain**

---
**Ngày c?p nh?t**: 2024
**Ng??i th?c hi?n**: GitHub Copilot
**Phiên b?n**: 1.0
