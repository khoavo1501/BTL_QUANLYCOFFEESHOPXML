using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using QUANLYCOFFEESHOP.DTO;

namespace QUANLYCOFFEESHOP.DAL
{
    public class NhanVienDAL
    {
        private const string FILE_NAME = "NhanVien.xml";
        private const string ROOT_NAME = "NhanViens";
        private const string ELEMENT_NAME = "NhanVien";
        public List<NhanVienDTO> GetAll()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();

            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                foreach (XElement element in doc.Root.Elements(ELEMENT_NAME))
                {
                    NhanVienDTO nv = new NhanVienDTO
                    {
                        MaNV = element.Element("MaNV")?.Value ?? "",
                        HoTen = element.Element("HoTen")?.Value ?? "",
                        GioiTinh = element.Element("GioiTinh")?.Value ?? "",
                        NgaySinh = DateTime.Parse(element.Element("NgaySinh")?.Value ?? DateTime.Now.ToString()),
                        SDT = element.Element("SDT")?.Value ?? "",
                        DiaChi = element.Element("DiaChi")?.Value ?? "",
                        NgayVaoLam = DateTime.Parse(element.Element("NgayVaoLam")?.Value ?? DateTime.Now.ToString())
                    };
                    list.Add(nv);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi đọc dữ liệu nhân viên: " + ex.Message);
            }

            return list;
        }

        public NhanVienDTO GetByID(string maNV)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                XElement element = doc.Root.Elements(ELEMENT_NAME)
                    .FirstOrDefault(x => x.Element("MaNV")?.Value == maNV);

                if (element != null)
                {
                    return new NhanVienDTO
                    {
                        MaNV = element.Element("MaNV")?.Value ?? "",
                        HoTen = element.Element("HoTen")?.Value ?? "",
                        GioiTinh = element.Element("GioiTinh")?.Value ?? "",
                        NgaySinh = DateTime.Parse(element.Element("NgaySinh")?.Value ?? DateTime.Now.ToString()),
                        SDT = element.Element("SDT")?.Value ?? "",
                        DiaChi = element.Element("DiaChi")?.Value ?? "",
                        NgayVaoLam = DateTime.Parse(element.Element("NgayVaoLam")?.Value ?? DateTime.Now.ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tìm nhân viên: " + ex.Message);
            }

            return null;
        }

        public bool Insert(NhanVienDTO nhanVien)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                if (doc.Root.Elements(ELEMENT_NAME).Any(x => x.Element("MaNV")?.Value == nhanVien.MaNV))
                {
                    System.Windows.Forms.MessageBox.Show("Mã nhân viên đã tồn tại!");
                    return false;
                }

                XElement newElement = new XElement(ELEMENT_NAME,
                    new XElement("MaNV", nhanVien.MaNV),
                    new XElement("HoTen", nhanVien.HoTen),
                    new XElement("GioiTinh", nhanVien.GioiTinh),
                    new XElement("NgaySinh", nhanVien.NgaySinh.ToString("yyyy-MM-dd")),
                    new XElement("SDT", nhanVien.SDT),
                    new XElement("DiaChi", nhanVien.DiaChi),
                    new XElement("NgayVaoLam", nhanVien.NgayVaoLam.ToString("yyyy-MM-dd"))
                );

                doc.Root.Add(newElement);
                
                bool saved = XMLHelper.SaveXML(doc, FILE_NAME);
                if (saved)
                {
                    XMLHelper.BackupToDatabase("NhanVien", doc);
                }
                return saved;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message);
                return false;
            }
        }

        public bool Update(NhanVienDTO nhanVien)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                XElement element = doc.Root.Elements(ELEMENT_NAME)
                    .FirstOrDefault(x => x.Element("MaNV")?.Value == nhanVien.MaNV);

                if (element != null)
                {
                    element.Element("HoTen").Value = nhanVien.HoTen;
                    element.Element("GioiTinh").Value = nhanVien.GioiTinh;
                    element.Element("NgaySinh").Value = nhanVien.NgaySinh.ToString("yyyy-MM-dd");
                    element.Element("SDT").Value = nhanVien.SDT;
                    element.Element("DiaChi").Value = nhanVien.DiaChi;
                    element.Element("NgayVaoLam").Value = nhanVien.NgayVaoLam.ToString("yyyy-MM-dd");

                    bool saved = XMLHelper.SaveXML(doc, FILE_NAME);
                    if (saved)
                    {
                        XMLHelper.BackupToDatabase("NhanVien", doc);
                    }
                    return saved;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi cập nhật nhân viên: " + ex.Message);
                return false;
            }
        }

        public bool Delete(string maNV)
        {
            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                XElement element = doc.Root.Elements(ELEMENT_NAME)
                    .FirstOrDefault(x => x.Element("MaNV")?.Value == maNV);

                if (element != null)
                {
                    element.Remove();
                    
                    bool saved = XMLHelper.SaveXML(doc, FILE_NAME);
                    if (saved)
                    {
                        XMLHelper.BackupToDatabase("NhanVien", doc);
                    }
                    return saved;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message);
                return false;
            }
        }

        public List<NhanVienDTO> Search(string keyword)
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();

            try
            {
                XDocument doc = XMLHelper.LoadOrCreateXML(FILE_NAME, ROOT_NAME);

                var elements = doc.Root.Elements(ELEMENT_NAME)
                    .Where(x => x.Element("HoTen")?.Value.ToLower().Contains(keyword.ToLower()) == true ||
                               x.Element("MaNV")?.Value.ToLower().Contains(keyword.ToLower()) == true ||
                               x.Element("SDT")?.Value.ToLower().Contains(keyword.ToLower()) == true);

                foreach (XElement element in elements)
                {
                    NhanVienDTO nv = new NhanVienDTO
                    {
                        MaNV = element.Element("MaNV")?.Value ?? "",
                        HoTen = element.Element("HoTen")?.Value ?? "",
                        GioiTinh = element.Element("GioiTinh")?.Value ?? "",
                        NgaySinh = DateTime.Parse(element.Element("NgaySinh")?.Value ?? DateTime.Now.ToString()),
                        SDT = element.Element("SDT")?.Value ?? "",
                        DiaChi = element.Element("DiaChi")?.Value ?? "",
                        NgayVaoLam = DateTime.Parse(element.Element("NgayVaoLam")?.Value ?? DateTime.Now.ToString())
                    };
                    list.Add(nv);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Lỗi tìm kiếm nhân viên: " + ex.Message);
            }

            return list;
        }
    }
}
