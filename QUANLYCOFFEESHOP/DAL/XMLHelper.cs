using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Linq;
using QUANLYCOFFEESHOP.Utils;

namespace QUANLYCOFFEESHOP.DAL
{
    public class XMLHelper
    {
        private static string xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static string backupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.BACKUP_FOLDER);

        public static void EnsureDataFolderExists()
        {
            if (!Directory.Exists(xmlPath))
            {
                Directory.CreateDirectory(xmlPath);
            }
        }

        public static void EnsureBackupFolderExists()
        {
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
        }

        public static string GetXMLFilePath(string fileName)
        {
            EnsureDataFolderExists();
            return Path.Combine(xmlPath, fileName);
        }

        public static string GetBackupFilePath(string fileName)
        {
            EnsureBackupFolderExists();
            return Path.Combine(backupPath, fileName);
        }

        public static XDocument LoadOrCreateXML(string fileName, string rootName)
        {
            string filePath = GetXMLFilePath(fileName);
            if (File.Exists(filePath))
            {
                return XDocument.Load(filePath);
            }
            else
            {
                XDocument doc = new XDocument(new XElement(rootName));
                doc.Save(filePath);
                return doc;
            }
        }

        public static bool SaveXML(XDocument doc, string fileName)
        {
            try
            {
                string filePath = GetXMLFilePath(fileName);
                doc.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lưu XML: " + ex.Message);
                return false;
            }
        }

        public static bool BackupToDatabase(string tableName, XDocument xmlDoc)
        {
            try
            {
                // Xóa dữ liệu cũ trong bảng
                DatabaseHelper.ExecuteNonQuery($"DELETE FROM {tableName}");

                // Insert dữ liệu từ XML vào database
                foreach (XElement element in xmlDoc.Root.Elements())
                {
                    List<string> columns = new List<string>();
                    List<string> values = new List<string>();

                    foreach (XElement field in element.Elements())
                    {
                        columns.Add(field.Name.LocalName);
                        values.Add("@" + field.Name.LocalName);
                    }

                    string insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";
                    
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    foreach (XElement field in element.Elements())
                    {
                        parameters.Add(new SqlParameter("@" + field.Name.LocalName, field.Value ?? ""));
                    }

                    DatabaseHelper.ExecuteNonQuery(insertQuery, parameters.ToArray());
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi backup to database: " + ex.Message);
                return false;
            }
        }

        public static bool RestoreFromDatabase(string tableName, string fileName, string rootName)
        {
            try
            {
                EnsureDataFolderExists();
                string query = $"SELECT * FROM {tableName}";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                XDocument doc = new XDocument(new XElement(rootName));

                foreach (DataRow row in dt.Rows)
                {
                    XElement element = new XElement(tableName);
                    foreach (DataColumn col in dt.Columns)
                    {
                        element.Add(new XElement(col.ColumnName, row[col]));
                    }
                    doc.Root.Add(element);
                }

                string filePath = GetXMLFilePath(fileName);
                doc.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi restore từ database: " + ex.Message);
                return false;
            }
        }

        public static bool BackupAllToDatabase()
        {
            try
            {
                // XÓA THEO THỨ TỰ NGƯỢC (Child trước, Parent sau)
                DatabaseHelper.ExecuteNonQuery("DELETE FROM CTHoaDon");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM HoaDon");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM TaiKhoan");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM SanPham");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM LoaiSanPham");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM NhanVien");
                DatabaseHelper.ExecuteNonQuery("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ThongTinCuaHang') DELETE FROM ThongTinCuaHang");

                // THÊM THEO THỨ TỰ ĐÚNG (Parent trước, Child sau)
                BackupTableToDatabaseDirect("LoaiSanPham", "LoaiSanPham.xml");
                BackupTableToDatabaseDirect("NhanVien", "NhanVien.xml");
                BackupTableToDatabaseDirect("TaiKhoan", "TaiKhoan.xml");
                BackupTableToDatabaseDirect("SanPham", "SanPham.xml");
                BackupTableToDatabaseDirect("HoaDon", "HoaDon.xml");
                BackupTableToDatabaseDirect("CTHoaDon", "CTHoaDon.xml");
                BackupTableToDatabaseDirect("ThongTinCuaHang", "ThongTinCuaHang.xml");
                
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi backup tất cả: " + ex.Message);
                return false;
            }
        }

        private static bool BackupTableToDatabaseDirect(string tableName, string fileName)
        {
            try
            {
                string filePath = GetXMLFilePath(fileName);
                if (!File.Exists(filePath))
                    return false;

                XDocument doc = XDocument.Load(filePath);

                foreach (XElement element in doc.Root.Elements())
                {
                    List<string> columns = new List<string>();
                    List<string> values = new List<string>();

                    foreach (XElement field in element.Elements())
                    {
                        columns.Add(field.Name.LocalName);
                        values.Add("@" + field.Name.LocalName);
                    }

                    string insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";
                    
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    foreach (XElement field in element.Elements())
                    {
                        parameters.Add(new SqlParameter("@" + field.Name.LocalName, field.Value));
                    }

                    DatabaseHelper.ExecuteNonQuery(insertQuery, parameters.ToArray());
                }

                return true;
            }
            catch (Exception ex)
            {
                // Bỏ qua lỗi nếu bảng không tồn tại
                return false;
            }
        }

        private static bool BackupTableToDatabase(string tableName, string fileName, string elementName)
        {
            try
            {
                string filePath = GetXMLFilePath(fileName);
                if (!File.Exists(filePath))
                    return false;

                XDocument doc = XDocument.Load(filePath);
                return BackupToDatabase(tableName, doc);
            }
            catch
            {
                return false;
            }
        }

        public static bool RestoreAllFromDatabase()
        {
            try
            {
                RestoreFromDatabase("LoaiSanPham", "LoaiSanPham.xml", "LoaiSanPhams");
                RestoreFromDatabase("SanPham", "SanPham.xml", "SanPhams");
                RestoreFromDatabase("NhanVien", "NhanVien.xml", "NhanViens");
                RestoreFromDatabase("TaiKhoan", "TaiKhoan.xml", "TaiKhoans");
                RestoreFromDatabase("HoaDon", "HoaDon.xml", "HoaDons");
                RestoreFromDatabase("CTHoaDon", "CTHoaDon.xml", "CTHoaDons");
                RestoreFromDatabase("ThongTinCuaHang", "ThongTinCuaHang.xml", "ThongTinCuaHangs");
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi restore tất cả: " + ex.Message);
                return false;
            }
        }

        public static string GetDataPath()
        {
            return xmlPath;
        }

        public static string GetBackupPath()
        {
            return backupPath;
        }

        public static bool BackupXMLFile(string fileName)
        {
            try
            {
                string sourceFile = GetXMLFilePath(fileName);
                string backupFile = GetBackupFilePath(fileName);

                if (File.Exists(sourceFile))
                {
                    // Copy file từ Data sang Backup với timestamp
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string backupFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + timestamp + Path.GetExtension(fileName);
                    string timestampBackupFile = Path.Combine(backupPath, backupFileName);
                    
                    File.Copy(sourceFile, timestampBackupFile, true);
                    
                    // Copy file gốc (không timestamp) để dễ restore
                    File.Copy(sourceFile, backupFile, true);
                    
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi backup XML file: " + ex.Message);
                return false;
            }
        }

        public static bool BackupAllXMLFiles()
        {
            try
            {
                EnsureBackupFolderExists();
                
                string[] xmlFiles = {
                    "LoaiSanPham.xml",
                    "SanPham.xml", 
                    "NhanVien.xml",
                    "TaiKhoan.xml",
                    "HoaDon.xml",
                    "CTHoaDon.xml",
                    "ThongTinCuaHang.xml"
                };

                foreach (string fileName in xmlFiles)
                {
                    BackupXMLFile(fileName);
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi backup tất cả XML files: " + ex.Message);
                return false;
            }
        }

        public static bool FullBackupProcess()
        {
            try
            {
                // Bước 1: Backup XML files từ Data sang Backup folder
                if (!BackupAllXMLFiles())
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi backup XML files!");
                    return false;
                }

                // Bước 2: Backup dữ liệu lên Database
                if (!BackupAllToDatabase())
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi backup to database!");
                    return false;
                }

                System.Windows.Forms.MessageBox.Show("Backup hoàn tất!\n- XML files đã được backup vào thư mục Backup\n- Dữ liệu đã được đồng bộ lên Database", 
                    "Thông báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi quá trình backup: " + ex.Message);
                return false;
            }
        }

        public static bool RestoreXMLFile(string fileName)
        {
            try
            {
                string backupFile = GetBackupFilePath(fileName);
                string dataFile = GetXMLFilePath(fileName);

                if (File.Exists(backupFile))
                {
                    File.Copy(backupFile, dataFile, true);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi restore XML file: " + ex.Message);
                return false;
            }
        }

        public static bool RestoreAllXMLFiles()
        {
            try
            {
                string[] xmlFiles = {
                    "LoaiSanPham.xml",
                    "SanPham.xml",
                    "NhanVien.xml", 
                    "TaiKhoan.xml",
                    "HoaDon.xml",
                    "CTHoaDon.xml",
                    "ThongTinCuaHang.xml"
                };

                foreach (string fileName in xmlFiles)
                {
                    RestoreXMLFile(fileName);
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi restore tất cả XML files: " + ex.Message);
                return false;
            }
        }

        public static bool ExportTableToXML(string tableName, string fileName)
        {
            try
            {
                EnsureDataFolderExists();
                string query = $"SELECT * FROM {tableName}";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);

                XDocument doc = new XDocument(new XElement(tableName + "s"));

                foreach (DataRow row in dt.Rows)
                {
                    XElement element = new XElement(tableName);
                    foreach (DataColumn col in dt.Columns)
                    {
                        element.Add(new XElement(col.ColumnName, row[col]));
                    }
                    doc.Root.Add(element);
                }

                string filePath = Path.Combine(xmlPath, fileName);
                doc.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi xuất XML: " + ex.Message);
                return false;
            }
        }
    }
}
