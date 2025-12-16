using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using QUANLYCOFFEESHOP.DTO;

namespace QUANLYCOFFEESHOP.DAL
{
    public class HoaDonDAL
    {
        private const string FILE_NAME = "HoaDon.xml";
        private const string ROOT_NAME = "HoaDons";
        private const string ELEMENT_NAME = "HoaDon";

        public List<HoaDonDTO> GetAll()
        {
            List<HoaDonDTO> list = new List<HoaDonDTO>();

            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                var sortedElements = doc.Root.Elements(ELEMENT_NAME)
                    .OrderByDescending(x => DateTime.Parse(x.Element("ThoiGianLap")?.Value ?? DateTime.Now.ToString()));

                foreach (XElement element in sortedElements)
                {
                    HoaDonDTO hd = new HoaDonDTO
                    {
                        MaHD = element.Element("MaHD")?.Value ?? "",
                        MaNV = element.Element("MaNV")?.Value ?? "",
                        ThoiGianLap = DateTime.Parse(element.Element("ThoiGianLap")?.Value ?? DateTime.Now.ToString()),
                        TongTien = int.Parse(element.Element("TongTien")?.Value ?? "0"),
                        TrangThai = element.Element("TrangThai")?.Value ?? ""
                    };
                    list.Add(hd);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("L?i ??c d? li?u hóa ??n: " + ex.Message);
            }

            return list;
        }

        public HoaDonDTO GetByID(string maHD)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                XElement element = doc.Root.Elements(ELEMENT_NAME)
                    .FirstOrDefault(x => x.Element("MaHD")?.Value == maHD);

                if (element != null)
                {
                    return new HoaDonDTO
                    {
                        MaHD = element.Element("MaHD")?.Value ?? "",
                        MaNV = element.Element("MaNV")?.Value ?? "",
                        ThoiGianLap = DateTime.Parse(element.Element("ThoiGianLap")?.Value ?? DateTime.Now.ToString()),
                        TongTien = int.Parse(element.Element("TongTien")?.Value ?? "0"),
                        TrangThai = element.Element("TrangThai")?.Value ?? ""
                    };
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("L?i tìm hóa ??n: " + ex.Message);
            }

            return null;
        }

        public bool Insert(HoaDonDTO hoaDon)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                // Không c?n ki?m tra trùng vì mã ???c t?o t? ??ng
                // Nh?ng v?n ki?m tra ?? an toàn
                var existingElement = doc.Root.Elements(ELEMENT_NAME)
                    .FirstOrDefault(x => x.Element("MaHD")?.Value == hoaDon.MaHD);
                
                if (existingElement != null)
                {
                    // N?u trùng, t?o l?i mã m?i
                    hoaDon.MaHD = GenerateNewMaHD();
                }

                XElement newElement = new XElement(ELEMENT_NAME,
                    new XElement("MaHD", hoaDon.MaHD),
                    new XElement("MaNV", hoaDon.MaNV),
                    new XElement("ThoiGianLap", hoaDon.ThoiGianLap.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("TongTien", hoaDon.TongTien),
                    new XElement("TrangThai", hoaDon.TrangThai)
                );

                doc.Root.Add(newElement);
                
                bool saved = XMLHelper.SaveXML(doc, FILE_NAME);
                if (saved)
                {
                    XMLHelper.BackupToDatabase("HoaDon", doc);
                }
                return saved;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("L?i thêm hóa ??n: " + ex.Message);
                return false;
            }
        }

        public List<HoaDonDTO> GetByDateRange(DateTime tuNgay, DateTime denNgay)
        {
            List<HoaDonDTO> list = new List<HoaDonDTO>();

            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                var filteredElements = doc.Root.Elements(ELEMENT_NAME)
                    .Where(x => 
                    {
                        DateTime thoiGian = DateTime.Parse(x.Element("ThoiGianLap")?.Value ?? DateTime.Now.ToString());
                        return thoiGian.Date >= tuNgay.Date && thoiGian.Date <= denNgay.Date;
                    })
                    .OrderByDescending(x => DateTime.Parse(x.Element("ThoiGianLap")?.Value ?? DateTime.Now.ToString()));

                foreach (XElement element in filteredElements)
                {
                    HoaDonDTO hd = new HoaDonDTO
                    {
                        MaHD = element.Element("MaHD")?.Value ?? "",
                        MaNV = element.Element("MaNV")?.Value ?? "",
                        ThoiGianLap = DateTime.Parse(element.Element("ThoiGianLap")?.Value ?? DateTime.Now.ToString()),
                        TongTien = int.Parse(element.Element("TongTien")?.Value ?? "0"),
                        TrangThai = element.Element("TrangThai")?.Value ?? ""
                    };
                    list.Add(hd);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("L?i l?y hóa ??n theo ngày: " + ex.Message);
            }

            return list;
        }

        public string GenerateNewMaHD()
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                var elements = doc.Root.Elements(ELEMENT_NAME).ToList();
                
                if (elements.Count == 0)
                {
                    return "HD001";
                }

                // Tìm s? l?n nh?t
                int maxNumber = 0;
                foreach (var element in elements)
                {
                    string maHD = element.Element("MaHD")?.Value ?? "HD000";
                    if (maHD.StartsWith("HD"))
                    {
                        string numberPart = maHD.Substring(2);
                        if (int.TryParse(numberPart, out int currentNumber))
                        {
                            if (currentNumber > maxNumber)
                            {
                                maxNumber = currentNumber;
                            }
                        }
                    }
                }

                // T?o mã m?i
                int newNumber = maxNumber + 1;
                return "HD" + newNumber.ToString("D3");
            }
            catch (Exception ex)
            {
                // Log l?i ?? debug
                System.Windows.Forms.MessageBox.Show("L?i t?o mã hóa ??n: " + ex.Message);
                // T?o mã d?a trên timestamp ?? tránh trùng
                return "HD" + DateTime.Now.ToString("HHmmss");
            }
        }
    }
}
