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
    }
}
