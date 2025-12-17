using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using QUANLYCOFFEESHOP.DTO;

namespace QUANLYCOFFEESHOP.DAL
{
    public class CTHoaDonDAL
    {
        private const string FILE_NAME = "CTHoaDon.xml";
        private const string ROOT_NAME = "CTHoaDons";
        private const string ELEMENT_NAME = "CTHoaDon";

        public List<CTHoaDonDTO> GetByMaHD(string maHD)
        {
            List<CTHoaDonDTO> list = new List<CTHoaDonDTO>();

            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                var elements = doc.Root.Elements(ELEMENT_NAME)
                    .Where(x => x.Element("MaHD")?.Value == maHD);

                foreach (XElement element in elements)
                {
                    CTHoaDonDTO ct = new CTHoaDonDTO
                    {
                        MaHD = element.Element("MaHD")?.Value ?? "",
                        MaSP = element.Element("MaSP")?.Value ?? "",
                        SoLuong = int.Parse(element.Element("SoLuong")?.Value ?? "0"),
                        DonGia = int.Parse(element.Element("DonGia")?.Value ?? "0"),
                        ThanhTien = int.Parse(element.Element("ThanhTien")?.Value ?? "0")
                    };
                    list.Add(ct);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi đọc chi tiết hóa đơn: " + ex.Message);
            }

            return list;
        }

        public bool Insert(CTHoaDonDTO chiTiet)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                XElement newElement = new XElement(ELEMENT_NAME,
                    new XElement("MaHD", chiTiet.MaHD),
                    new XElement("MaSP", chiTiet.MaSP),
                    new XElement("SoLuong", chiTiet.SoLuong),
                    new XElement("DonGia", chiTiet.DonGia),
                    new XElement("ThanhTien", chiTiet.ThanhTien)
                );

                doc.Root.Add(newElement);
                
                bool saved = XMLHelper.SaveXML(doc, FILE_NAME);
                if (saved)
                {
                    XMLHelper.BackupToDatabase("CTHoaDon", doc);
                }
                return saved;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi thêm chi tiết hóa đơn: " + ex.Message);
                return false;
            }
        }

        public DataTable GetDetailedByMaHD(string maHD)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("MaHD", typeof(string));
                dt.Columns.Add("MaSP", typeof(string));
                dt.Columns.Add("TenSP", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DonGia", typeof(int));
                dt.Columns.Add("ThanhTien", typeof(int));

                XDocument ctDoc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);
                XDocument spDoc = XMLHelper.LoadOrCreateXML("SanPham.xml", "SanPhams");

                var chiTietElements = ctDoc.Root.Elements(ELEMENT_NAME)
                    .Where(x => x.Element("MaHD")?.Value == maHD);

                foreach (XElement ctElement in chiTietElements)
                {
                    string maSP = ctElement.Element("MaSP")?.Value ?? "";
                    
                    XElement spElement = spDoc.Root.Elements("SanPham")
                        .FirstOrDefault(x => x.Element("MaSP")?.Value == maSP);

                    string tenSP = spElement?.Element("TenSP")?.Value ?? "";

                    DataRow row = dt.NewRow();
                    row["MaHD"] = ctElement.Element("MaHD")?.Value ?? "";
                    row["MaSP"] = maSP;
                    row["TenSP"] = tenSP;
                    row["SoLuong"] = int.Parse(ctElement.Element("SoLuong")?.Value ?? "0");
                    row["DonGia"] = int.Parse(ctElement.Element("DonGia")?.Value ?? "0");
                    row["ThanhTien"] = int.Parse(ctElement.Element("ThanhTien")?.Value ?? "0");
                    
                    dt.Rows.Add(row);
                }

                return dt;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi lấy chi tiết hóa đơn: " + ex.Message);
                return new DataTable();
            }
        }
    }
}
