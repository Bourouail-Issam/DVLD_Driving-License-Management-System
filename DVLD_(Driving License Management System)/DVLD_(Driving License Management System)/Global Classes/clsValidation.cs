using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD__Driving_License_Management_System_.Global_Classes
{
    public class clsValidation
    {
        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(emailAddress);
        }

        public static bool ValidatePhoneNumber(string PhoneNumber)
        {
            string pattern = @"^\+?[1-9]\d{1,14}$";
            Regex regex = new Regex(pattern);
            
            return regex.IsMatch(PhoneNumber);
        }
    }
}
