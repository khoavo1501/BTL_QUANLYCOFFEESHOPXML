using System;
using System.Security.Cryptography;
using System.Text;

namespace QUANLYCOFFEESHOP.Utils
{
    public static class Helper
    {
        public static string MD5Hash(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string FormatCurrency(int amount)
        {
            return amount.ToString("#,##0") + " ?";
        }

        public static string GenerateID(string prefix, int currentMax)
        {
            int nextNumber = currentMax + 1;
            return prefix + nextNumber.ToString("D2");
        }
    }
}
