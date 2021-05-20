using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyundaiPortal.Business.Util
{
    public static class StringUtil
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) { return value; }
            value = value.Trim();

            return value.Substring(0, Math.Min(value.Length, maxLength));
        }

        public static string TruncateNumeric(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) { return value; }
            value = value.Trim();
            return value.Substring(value.Length > maxLength ? value.Length - maxLength : 0);
        }

        public static string RemoveSpecial(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return value; }

            return value.Replace("'", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(",", "").Replace(".","").ToUpper();
        }

        public static decimal Ceiling(decimal value)
        {
            if (value < 1)
                value = 1;
            else
            {
                value = Math.Ceiling(value);
            }
            return value;
        }

        public static string ConvertState(string value)
        {
            Dictionary<string, string> states = new Dictionary<string, string>();
            states.Add("AL", "Alabama");
            states.Add("AK", "Alaska");
            states.Add("AZ", "Arizona");
            states.Add("AR", "Arkansas");
            states.Add("CA", "California");
            states.Add("CO", "Colorado");
            states.Add("CT", "Connecticut");
            states.Add("DE", "Delaware");
            states.Add("DC", "District of Columbia");
            states.Add("FL", "Florida");
            states.Add("GA", "Georgia");
            states.Add("HI", "Hawaii");
            states.Add("ID", "Idaho");
            states.Add("IL", "Illinois");
            states.Add("IN", "Indiana");
            states.Add("IA", "Iowa");
            states.Add("KS", "Kansas");
            states.Add("KY", "Kentucky");
            states.Add("LA", "Louisiana");
            states.Add("ME", "Maine");
            states.Add("MD", "Maryland");
            states.Add("MA", "Massachusetts");
            states.Add("MI", "Michigan");
            states.Add("MN", "Minnesota");
            states.Add("MS", "Mississippi");
            states.Add("MO", "Missouri");
            states.Add("MT", "Montana");
            states.Add("NE", "Nebraska");
            states.Add("NV", "Nevada");
            states.Add("NH", "New Hampshire");
            states.Add("NJ", "New Jersey");
            states.Add("NM", "New Mexico");
            states.Add("NY", "New York");
            states.Add("NC", "North Carolina");
            states.Add("ND", "North Dakota");
            states.Add("OH", "Ohio");
            states.Add("OK", "Oklahoma");
            states.Add("OR", "Oregon");
            states.Add("PA", "Pennsylvania");
            states.Add("RI", "Rhode Island");
            states.Add("SC", "South Carolina");
            states.Add("SD", "South Dakota");
            states.Add("TN", "Tennessee");
            states.Add("TX", "Texas");
            states.Add("UT", "Utah");
            states.Add("VT", "Vermont");
            states.Add("VA", "Virginia");
            states.Add("WA", "Washington");
            states.Add("WV", "West Virginia");
            states.Add("WI", "Wisconsin");
            states.Add("WY", "Wyoming");

            if (value.Length == 2)
            {
                return value.ToUpper();
            }
            else
            {
                return states.Where(s => s.Value.ToUpper() == value.ToUpper()).FirstOrDefault().Key;
            }
        }
    }
}
