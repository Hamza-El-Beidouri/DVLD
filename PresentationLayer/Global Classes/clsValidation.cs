using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DVLD.Global_Classes
{
    public static class clsValidation
    {

        public static bool IsValidName(string name)
        {
            string pattern = @"^[\p{L}]+(?:[-\s'][a-zA-Z\p{L}]+)*$";
            return Regex.IsMatch(name, pattern);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^[\+\s\-\(\)\.\/\d]{5,30}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            return Regex.IsMatch(emailAddress, pattern);
        }

        public static bool IsValidStreetAddress(string streetAddress)
        {
            string pattern = @"^[\p{L}\p{N}\s\-\,\.\/#&'@()]{5,200}$";
            return Regex.IsMatch(streetAddress, pattern);
        }

        public static bool IsIntNumber(string Number)
        {
            if (int.TryParse(Number, out int value))
                return true;
            else
                return false;
        }

        public static bool IsFloatNumber(string Number)
        {
            if (float.TryParse(Number, out float value))
                return true;
            else
                return false;
        }

        public static bool IsNumber(string Number)
        {
            if (IsIntNumber(Number) && IsFloatNumber(Number))
                return true;
            else
                return false;
        }

    }
}
