using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using QUANLYCOFFEESHOP.DAL;

namespace QUANLYCOFFEESHOP.Utils
{
    public class HTMLReportHelper
    {
        public static bool GenerateHTMLReport(string outputPath)
        {
            try
            {
                // Đảm bảo thư mục tồn tại
                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Đọc dữ liệu từ các file XML
                string dataPath = XMLHelper.GetDataPath();
                
                // Load các file XML
                XDocument sanPhamDoc = LoadXMLFile(Path.Combine(dataPath, "SanPham.xml"), "SanPhams");
                XDocument loaiSPDoc = LoadXMLFile(Path.Combine(dataPath, "LoaiSanPham.xml"), "LoaiSanPhams");
                XDocument hoaDonDoc = LoadXMLFile(Path.Combine(dataPath, "HoaDon.xml"), "HoaDons");
                XDocument nhanVienDoc = LoadXMLFile(Path.Combine(dataPath, "NhanVien.xml"), "NhanViens");

                // Tạo nội dung HTML
                StringBuilder html = new StringBuilder();
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html lang='vi'>");
                html.AppendLine("<head>");
                html.AppendLine("    <meta charset='UTF-8'>");
                html.AppendLine("    <meta name='viewport' content='width=device-width, initial-scale=1.0'>");
                html.AppendLine("    <title>Báo Cáo Dự Án - Quản Lý Coffee Shop XML</title>");
                html.AppendLine("    <style>");
                html.AppendLine(GetCSS());
                html.AppendLine("    </style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");
                
                // Header
                html.AppendLine("    <div class='header'>");
                html.AppendLine("        <div class='container'>");
                html.AppendLine("            <h1>&#128202; BÁO CÁO DỰ ÁN</h1>");
                html.AppendLine("            <h2>Hệ Thống Quản Lý Coffee Shop - XML</h2>");
                html.AppendLine($"            <p class='date'>Ngày tạo: {DateTime.Now:dd/MM/yyyy HH:mm:ss}</p>");
                html.AppendLine("        </div>");
                html.AppendLine("    </div>");

                // Main content
                html.AppendLine("    <div class='container'>");
                
                // Thông tin tổng quan
                html.AppendLine("        <div class='section'>");
                html.AppendLine("            <h3>&#128203; Thông Tin Tổng Quan</h3>");
                html.AppendLine("            <div class='stats-grid'>");
                html.AppendLine($"                <div class='stat-card'>");
                html.AppendLine($"                    <div class='stat-icon'>&#127991;</div>");
                html.AppendLine($"                    <div class='stat-value'>{loaiSPDoc?.Root?.Elements().Count() ?? 0}</div>");
                html.AppendLine($"                    <div class='stat-label'>Loại Sản Phẩm</div>");
                html.AppendLine($"                </div>");
                html.AppendLine($"                <div class='stat-card'>");
                html.AppendLine($"                    <div class='stat-icon'>&#9749;</div>");
                html.AppendLine($"                    <div class='stat-value'>{sanPhamDoc?.Root?.Elements().Count() ?? 0}</div>");
                html.AppendLine($"                    <div class='stat-label'>Sản Phẩm</div>");
                html.AppendLine($"                </div>");
                html.AppendLine($"                <div class='stat-card'>");
                html.AppendLine($"                    <div class='stat-icon'>&#128101;</div>");
                html.AppendLine($"                    <div class='stat-value'>{nhanVienDoc?.Root?.Elements().Count() ?? 0}</div>");
                html.AppendLine($"                    <div class='stat-label'>Nhân Viên</div>");
                html.AppendLine($"                </div>");
                html.AppendLine($"                <div class='stat-card'>");
                html.AppendLine($"                    <div class='stat-icon'>&#128196;</div>");
                html.AppendLine($"                    <div class='stat-value'>{hoaDonDoc?.Root?.Elements().Count() ?? 0}</div>");
                html.AppendLine($"                    <div class='stat-label'>Hóa Đơn</div>");
                html.AppendLine($"                </div>");
                html.AppendLine("            </div>");
                html.AppendLine("        </div>");

                // Tải xuống file XML
                html.AppendLine("        <div class='section'>");
                html.AppendLine("            <h3>&#128229; Tải Xuống File XML</h3>");
                html.AppendLine("            <p style='margin-bottom: 15px; color: #666;'>Nhấn vào nút để tải xuống file XML tương ứng</p>");
                html.AppendLine("            <div class='download-grid'>");
                
                // Nhúng dữ liệu XML vào data attributes
                string[] xmlFiles = { "LoaiSanPham", "SanPham", "NhanVien", "TaiKhoan", "HoaDon", "CTHoaDon" };
                foreach (string xmlName in xmlFiles)
                {
                    string xmlPath = Path.Combine(dataPath, $"{xmlName}.xml");
                    string xmlContent = "";
                    if (File.Exists(xmlPath))
                    {
                        xmlContent = File.ReadAllText(xmlPath).Replace("\"", "&quot;").Replace("'", "&#39;");
                    }
                    
                    html.AppendLine($"                <button class='download-btn' onclick=\"downloadXML('{xmlName}.xml')\" data-xml-{xmlName.ToLower()}='{xmlContent}'>");
                    html.AppendLine("                    <span class='btn-icon'>&#128193;</span>");
                    html.AppendLine($"                    <span>{xmlName}.xml</span>");
                    html.AppendLine("                </button>");
                }
                
                html.AppendLine("            </div>");
                html.AppendLine("        </div>");

                // Danh sách sản phẩm
                html.AppendLine(GenerateProductTable(sanPhamDoc, loaiSPDoc));

                // Danh sách nhân viên
                html.AppendLine(GenerateEmployeeTable(nhanVienDoc));

                // Danh sách hóa đơn gần đây
                html.AppendLine(GenerateInvoiceTable(hoaDonDoc));

                // Thông tin dự án
                html.AppendLine("        <div class='section'>");
                html.AppendLine("            <h3>&#8505; Thông Tin Dự Án</h3>");
                html.AppendLine("            <div class='info-grid'>");
                html.AppendLine("                <div><strong>Tên dự án:</strong> Quản Lý Coffee Shop XML</div>");
                html.AppendLine("                <div><strong>Công nghệ:</strong> C# WinForms + XML Database</div>");
                html.AppendLine("                <div><strong>Framework:</strong> .NET Framework 4.7.2</div>");
                html.AppendLine("                <div><strong>Kiến trúc:</strong> 3-Layer Architecture (DAL, DTO, GUI)</div>");
                html.AppendLine("                <div><strong>Lưu trữ:</strong> XML Files (Pure XML - No SQL)</div>");
                html.AppendLine("                <div><strong>Tính năng:</strong> CRUD Operations, Reports, Backup/Restore</div>");
                html.AppendLine("            </div>");
                html.AppendLine("        </div>");

                html.AppendLine("    </div>");

                // Footer
                html.AppendLine("    <div class='footer'>");
                html.AppendLine("        <div class='container'>");
                html.AppendLine("            <p>&copy; 2025 Coffee Shop XML Management System. All rights reserved.</p>");
                html.AppendLine("            <p>Generated by HTML Report System</p>");
                html.AppendLine("        </div>");
                html.AppendLine("    </div>");

                // JavaScript
                html.AppendLine("    <script>");
                html.AppendLine(GetJavaScript());
                html.AppendLine("    </script>");

                html.AppendLine("</body>");
                html.AppendLine("</html>");

                // Lưu file HTML
                File.WriteAllText(outputPath, html.ToString(), Encoding.UTF8);

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tạo báo cáo HTML: " + ex.Message);
                return false;
            }
        }

        private static XDocument LoadXMLFile(string filePath, string rootName)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return XDocument.Load(filePath);
                }
                else
                {
                    return new XDocument(new XElement(rootName));
                }
            }
            catch
            {
                return new XDocument(new XElement(rootName));
            }
        }

        private static string GenerateProductTable(XDocument sanPhamDoc, XDocument loaiSPDoc)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("        <div class='section'>");
            html.AppendLine("            <h3>&#9749; Danh Sách Sản Phẩm</h3>");
            html.AppendLine("            <div class='table-responsive'>");
            html.AppendLine("                <table class='data-table'>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <th>Mã SP</th>");
            html.AppendLine("                            <th>Tên Sản Phẩm</th>");
            html.AppendLine("                            <th>Giá</th>");
            html.AppendLine("                            <th>Loại</th>");
            html.AppendLine("                            <th>Trạng Thái</th>");
            html.AppendLine("                        </tr>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");

            if (sanPhamDoc?.Root != null)
            {
                foreach (var sp in sanPhamDoc.Root.Elements())
                {
                    string maSP = sp.Element("MaSP")?.Value ?? "";
                    string tenSP = sp.Element("TenSP")?.Value ?? "";
                    string gia = sp.Element("Gia")?.Value ?? "0";
                    string maLoai = sp.Element("MaLoai")?.Value ?? "";
                    string trangThai = sp.Element("TrangThai")?.Value ?? "";

                    // Tìm tên loại
                    string tenLoai = maLoai;
                    if (loaiSPDoc?.Root != null)
                    {
                        var loai = loaiSPDoc.Root.Elements().FirstOrDefault(l => l.Element("MaLoai")?.Value == maLoai);
                        if (loai != null)
                        {
                            tenLoai = loai.Element("TenLoai")?.Value ?? maLoai;
                        }
                    }

                    html.AppendLine("                        <tr>");
                    html.AppendLine($"                            <td>{maSP}</td>");
                    html.AppendLine($"                            <td>{tenSP}</td>");
                    html.AppendLine($"                            <td class='price'>{int.Parse(gia):N0} đ</td>");
                    html.AppendLine($"                            <td>{tenLoai}</td>");
                    html.AppendLine($"                            <td><span class='badge badge-success'>{trangThai}</span></td>");
                    html.AppendLine("                        </tr>");
                }
            }

            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            return html.ToString();
        }

        private static string GenerateEmployeeTable(XDocument nhanVienDoc)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("        <div class='section'>");
            html.AppendLine("            <h3>&#128101; Danh Sách Nhân Viên</h3>");
            html.AppendLine("            <div class='table-responsive'>");
            html.AppendLine("                <table class='data-table'>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <th>Mã NV</th>");
            html.AppendLine("                            <th>Họ Tên</th>");
            html.AppendLine("                            <th>Giới Tính</th>");
            html.AppendLine("                            <th>Số Điện Thoại</th>");
            html.AppendLine("                            <th>Ngày Vào Làm</th>");
            html.AppendLine("                        </tr>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");

            if (nhanVienDoc?.Root != null)
            {
                foreach (var nv in nhanVienDoc.Root.Elements())
                {
                    string maNV = nv.Element("MaNV")?.Value ?? "";
                    string hoTen = nv.Element("HoTen")?.Value ?? "";
                    string gioiTinh = nv.Element("GioiTinh")?.Value ?? "";
                    string sdt = nv.Element("SDT")?.Value ?? "";
                    string ngayVaoLam = nv.Element("NgayVaoLam")?.Value ?? "";

                    // Format ngày
                    DateTime date;
                    if (DateTime.TryParse(ngayVaoLam, out date))
                    {
                        ngayVaoLam = date.ToString("dd/MM/yyyy");
                    }

                    html.AppendLine("                        <tr>");
                    html.AppendLine($"                            <td>{maNV}</td>");
                    html.AppendLine($"                            <td>{hoTen}</td>");
                    html.AppendLine($"                            <td>{gioiTinh}</td>");
                    html.AppendLine($"                            <td>{sdt}</td>");
                    html.AppendLine($"                            <td>{ngayVaoLam}</td>");
                    html.AppendLine("                        </tr>");
                }
            }

            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            return html.ToString();
        }

        private static string GenerateInvoiceTable(XDocument hoaDonDoc)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("        <div class='section'>");
            html.AppendLine("            <h3>&#128196; Hóa Đơn Gần Đây (Top 10)</h3>");
            html.AppendLine("            <div class='table-responsive'>");
            html.AppendLine("                <table class='data-table'>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <th>Mã HĐ</th>");
            html.AppendLine("                            <th>Mã NV</th>");
            html.AppendLine("                            <th>Thời Gian</th>");
            html.AppendLine("                            <th>Tổng Tiền</th>");
            html.AppendLine("                            <th>Trạng Thái</th>");
            html.AppendLine("                        </tr>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");

            if (hoaDonDoc?.Root != null)
            {
                var hoaDons = hoaDonDoc.Root.Elements().Take(10);
                foreach (var hd in hoaDons)
                {
                    string maHD = hd.Element("MaHD")?.Value ?? "";
                    string maNV = hd.Element("MaNV")?.Value ?? "";
                    string thoiGian = hd.Element("ThoiGianLap")?.Value ?? "";
                    string tongTien = hd.Element("TongTien")?.Value ?? "0";
                    string trangThai = hd.Element("TrangThai")?.Value ?? "";

                    // Format ngày giờ
                    DateTime dateTime;
                    if (DateTime.TryParse(thoiGian, out dateTime))
                    {
                        thoiGian = dateTime.ToString("dd/MM/yyyy HH:mm");
                    }

                    html.AppendLine("                        <tr>");
                    html.AppendLine($"                            <td>{maHD}</td>");
                    html.AppendLine($"                            <td>{maNV}</td>");
                    html.AppendLine($"                            <td>{thoiGian}</td>");
                    html.AppendLine($"                            <td class='price'>{int.Parse(tongTien):N0} đ</td>");
                    html.AppendLine($"                            <td><span class='badge badge-success'>{trangThai}</span></td>");
                    html.AppendLine("                        </tr>");
                }
            }

            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            return html.ToString();
        }

        private static string GetCSS()
        {
            return @"
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
        }

        .container {
            max-width: 1200px;
            margin: 0 auto;
        }

        .header {
            background: white;
            padding: 40px 20px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
            margin-bottom: 30px;
            text-align: center;
        }

        .header h1 {
            color: #667eea;
            font-size: 2.5em;
            margin-bottom: 10px;
        }

        .header h2 {
            color: #555;
            font-size: 1.5em;
            font-weight: normal;
            margin-bottom: 10px;
        }

        .date {
            color: #999;
            font-size: 0.9em;
        }

        .section {
            background: white;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
            margin-bottom: 30px;
        }

        .section h3 {
            color: #667eea;
            font-size: 1.8em;
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 3px solid #667eea;
        }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin-top: 20px;
        }

        .stat-card {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 30px;
            border-radius: 10px;
            text-align: center;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            transition: transform 0.3s ease;
        }

        .stat-card:hover {
            transform: translateY(-5px);
        }

        .stat-icon {
            font-size: 3em;
            margin-bottom: 10px;
        }

        .stat-value {
            font-size: 2.5em;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .stat-label {
            font-size: 1em;
            opacity: 0.9;
        }

        .download-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 15px;
            margin-top: 20px;
        }

        .download-btn {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 15px 20px;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            font-size: 1em;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 10px;
            transition: all 0.3s ease;
            box-shadow: 0 3px 10px rgba(0,0,0,0.2);
        }

        .download-btn:hover {
            transform: translateY(-3px);
            box-shadow: 0 5px 15px rgba(0,0,0,0.3);
        }

        .btn-icon {
            font-size: 1.5em;
        }

        .table-responsive {
            overflow-x: auto;
            margin-top: 20px;
        }

        .data-table {
            width: 100%;
            border-collapse: collapse;
            background: white;
        }

        .data-table thead {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }

        .data-table th,
        .data-table td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        .data-table tbody tr:hover {
            background: #f5f5f5;
        }

        .price {
            color: #667eea;
            font-weight: bold;
            text-align: right;
        }

        .badge {
            padding: 5px 15px;
            border-radius: 20px;
            font-size: 0.85em;
            display: inline-block;
        }

        .badge-success {
            background: #10b981;
            color: white;
        }

        .info-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 15px;
            margin-top: 20px;
        }

        .info-grid div {
            padding: 15px;
            background: #f8f9fa;
            border-radius: 8px;
            border-left: 4px solid #667eea;
        }

        .footer {
            background: rgba(255,255,255,0.9);
            padding: 30px 20px;
            border-radius: 15px;
            text-align: center;
            color: #555;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
        }

        .footer p {
            margin: 5px 0;
        }

        @media print {
            body {
                background: white;
            }
            .download-btn {
                display: none;
            }
        }";
        }

        private static string GetJavaScript()
        {
            return @"
        // Hàm download file XML
        function downloadXML(filename) {
            try {
                // Tìm button tương ứng để lấy nội dung XML
                const buttons = document.querySelectorAll('.download-btn');
                let xmlContent = '';
                
                buttons.forEach(btn => {
                    if (btn.textContent.includes(filename)) {
                        // Lấy tên file không có extension để tìm data attribute
                        const dataAttr = 'data-xml-' + filename.replace('.xml', '').toLowerCase();
                        xmlContent = btn.getAttribute(dataAttr);
                    }
                });
                
                if (!xmlContent) {
                    alert('Không tìm thấy nội dung file XML: ' + filename);
                    return;
                }
                
                // Decode HTML entities
                xmlContent = xmlContent.replace(/&quot;/g, '""').replace(/&#39;/g, ""'"");
                
                // Tạo Blob từ nội dung XML
                const blob = new Blob([xmlContent], { type: 'application/xml' });
                
                // Tạo URL cho blob
                const url = window.URL.createObjectURL(blob);
                
                // Tạo thẻ a ẩn để trigger download
                const a = document.createElement('a');
                a.style.display = 'none';
                a.href = url;
                a.download = filename;
                
                // Thêm vào DOM, click, rồi xóa
                document.body.appendChild(a);
                a.click();
                
                // Cleanup
                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
                
                // Thông báo thành công
                showNotification('Đã tải xuống: ' + filename, 'success');
            } catch (error) {
                console.error('Lỗi khi download:', error);
                alert('Lỗi khi tải xuống file XML: ' + error.message);
            }
        }

        // Hiển thị thông báo
        function showNotification(message, type) {
            const notification = document.createElement('div');
            notification.className = 'notification notification-' + type;
            notification.textContent = message;
            notification.style.cssText = `
                position: fixed;
                top: 20px;
                right: 20px;
                background: ${type === 'success' ? '#10b981' : '#ef4444'};
                color: white;
                padding: 15px 25px;
                border-radius: 8px;
                box-shadow: 0 4px 12px rgba(0,0,0,0.15);
                z-index: 10000;
                animation: slideIn 0.3s ease;
            `;
            
            document.body.appendChild(notification);
            
            setTimeout(() => {
                notification.style.animation = 'slideOut 0.3s ease';
                setTimeout(() => document.body.removeChild(notification), 300);
            }, 3000);
        }

        // Animation khi scroll
        window.addEventListener('scroll', function() {
            const sections = document.querySelectorAll('.section');
            sections.forEach(section => {
                const position = section.getBoundingClientRect().top;
                const screenPosition = window.innerHeight / 1.3;
                
                if(position < screenPosition) {
                    section.style.animation = 'fadeInUp 0.6s ease';
                }
            });
        });

        // Print function
        function printReport() {
            window.print();
        }

        // Thêm keyframe animations
        const style = document.createElement('style');
        style.textContent = `
            @keyframes slideIn {
                from { transform: translateX(100%); opacity: 0; }
                to { transform: translateX(0); opacity: 1; }
            }
            @keyframes slideOut {
                from { transform: translateX(0); opacity: 1; }
                to { transform: translateX(100%); opacity: 0; }
            }
        `;
        document.head.appendChild(style);";
        }
    }
}
