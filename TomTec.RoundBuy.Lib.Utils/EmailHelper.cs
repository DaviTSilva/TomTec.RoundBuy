using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace TomTec.RoundBuy.Lib.Utils
{
    public static class EmailHelper
    {
        public static bool IsEmail(this string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
