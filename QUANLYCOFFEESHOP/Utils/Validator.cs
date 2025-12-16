using System;
using System.Text.RegularExpressions;

namespace QUANLYCOFFEESHOP.Utils
{
    public static class Validator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string pattern = @"^0\d{9,10}$";
            return Regex.IsMatch(phone, pattern);
        }

        public static bool IsPositiveNumber(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            if (int.TryParse(text, out int number))
            {
                return number > 0;
            }
            return false;
        }

        public static bool IsNotEmpty(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
