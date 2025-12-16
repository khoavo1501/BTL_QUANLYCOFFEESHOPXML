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

        public static void EnsureDataFolderExists()
        {
            if (!Directory.Exists(xmlPath))
            {
                Directory.CreateDirectory(xmlPath);
            }
        }

        public static string GetXMLFilePath(string fileName)
        {
            EnsureDataFolderExists();
            return Path.Combine(xmlPath, fileName);
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
                System.Windows.Forms.MessageBox.Show("L?i l?u XML: " + ex.Message);
                return false;
            }
        }

        public static bool BackupToDatabase(string tableName, XDocument xmlDoc)
        {
            
            return true;
            
            
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
                System.Windows.Forms.MessageBox.Show("L?i restore t? database: " + ex.Message);
                return false;
            }
        }

        public static bool BackupAllToDatabase()
        {
            try
            {
                // XÓA THEO TH? T? NG??C (Child tr??c, Parent sau)
                DatabaseHelper.ExecuteNonQuery("DELETE FROM CTHoaDon");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM HoaDon");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM TaiKhoan");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM SanPham");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM LoaiSanPham");
                DatabaseHelper.ExecuteNonQuery("DELETE FROM NhanVien");
                DatabaseHelper.ExecuteNonQuery("IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ThongTinCuaHang') DELETE FROM ThongTinCuaHang");

                // THÊM THEO TH? T? ?ÚNG (Parent tr??c, Child sau)
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
                System.Windows.Forms.MessageBox.Show("L?i backup t?t c?: " + ex.Message);
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
                // B? qua l?i n?u b?ng không t?n t?i
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
                System.Windows.Forms.MessageBox.Show("L?i restore t?t c?: " + ex.Message);
                return false;
            }
        }

        public static string GetDataPath()
        {
            return xmlPath;
        }

        public static string GetBackupPath()
        {
            return xmlPath;
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
                System.Windows.Forms.MessageBox.Show("L?i xu?t XML: " + ex.Message);
                return false;
            }
        }
    }
}
