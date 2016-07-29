using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class TextManipulation
    {
        public static object toDBNULLfromEmpty(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return DBNull.Value;
            }
            else if (s.Trim().Equals("0"))
            {
                return DBNull.Value;
            }
            return s;
        }
        public static string FixDateFormat(this string str)
        {
            DateTime date;
            if (DateTime.TryParse(str, out date))
            {
                return date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                return null;
            }
        }
        public static string toPlanType(this string i)
        {
            return i.Equals("1") ? "Medical Plan" : "COBRA Plan";
        }

        public static string planTypetoNumber(this string s)
        {
            return s.Equals("Medical Plan") ? "1" : "0";
        }

        public static string toWaitingPeriod(this string i)
        {
            return i.Equals("1") ? "First of the Month Following" : "Next Day Following";
        }

        public static string WaitingPeriodtoNumber(this string s)
        {
            return s.Equals("First of the Month Following") ? "1" : "0";
        }
        public static string toYesNo(this string i)
        {
            return i.Equals("1") ? "Yes" : "No";
        }

        public static string convertYesNotoNumber(this string s)
        {
            return s.Equals("Yes") ? "1" : "0";
        }
    }
}
