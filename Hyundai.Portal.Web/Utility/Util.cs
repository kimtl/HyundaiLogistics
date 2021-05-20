using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace HyundaiPortal.Business.Utility
{
    public class JsonObject
    {
        public string FullUrl { get; set; }
        public string SmallUrl { get; set; }
        public bool isDesktop { get; set; }
    }
    public class clsEncryption
    {
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string EncryptionKey = "!5623a#de";

        public static string Encrypt(string Input)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes
                (EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(Input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Decrypt(string Input)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes
                (EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Convert.FromBase64String(Input);
                MemoryStream ms = new MemoryStream(inputByteArray);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Read);

                byte[] plainTextBytes = new byte[inputByteArray.Length];
                int decryptedByteCount = cs.Read(plainTextBytes, 0, plainTextBytes.Length);

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }


    public static class clsBase64
    {
        public static string Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
        public static string Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
    }

    public class Util
    {
        //public static IList<SelectListItem> SelectListConverter(IList<ListSelectItem> list)
        //{
        //    IList<SelectListItem> lst = new List<SelectListItem>();
        //    foreach (var selectListItem in list)
        //    {
        //        SelectListItem org = new SelectListItem()
        //        {
        //            Selected = selectListItem.Selected,
        //            Text = selectListItem.Text,
        //            Value = selectListItem.Value
        //        };
        //        lst.Add(org);
        //    }
        //    return lst;
        //}

        //public static IList<SelectListItem> SelectListConverter(IList<ListSelectItem> list, string removeKey)
        //{
        //    IList<SelectListItem> lst = new List<SelectListItem>();
        //    foreach (var selectListItem in list)
        //    {
        //        if (removeKey != selectListItem.Value)
        //        {
        //            SelectListItem org = new SelectListItem()
        //                                     {
        //                                         Selected = selectListItem.Selected,
        //                                         Text = selectListItem.Text,
        //                                         Value = selectListItem.Value
        //                                     };
        //            lst.Add(org);
        //        }
        //    }
        //    return lst;
        //}

        public static void CreateFolder(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }
        public static string returnWaluta(string varS, string varSymbol)
        {
            decimal varD = decimal.Parse(varS);
            if (varSymbol == "EUR")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR", false);
                return String.Format("{0:c}", varD);
            }
            else if (varSymbol == "PLN")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL", false);
                return String.Format("{0:c}", varD);
            }
            else if (varSymbol == "USD")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                return String.Format("{0:c}", varD);
            }
            else
            {
                // Not handled currency
                return varS.ToString();
            }
        }
        public static string returnWaluta(decimal varS, string varSymbol)
        {
            if (varSymbol == "EUR")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR", false);
                return String.Format("{0:c}", varS);
            }
            else if (varSymbol == "PLN")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL", false);
                return String.Format("{0:c}", varS);
            }
            else if (varSymbol == "USD")
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
                return String.Format("{0:c}", varS);
            }
            else
            {
                // Not handled currency
                return varS.ToString();
            }
        }

        public static string returnWaluta(string varS)
        {
            decimal varD = decimal.Parse(varS);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
            return String.Format("{0:c}", varD);
        }
        public static string returnWaluta(decimal varS)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
            return String.Format("{0:c}", varS);
        }

        public static string returnWaluta(decimal varS, int number)
        {
            string formatValue = string.Empty;

            if (number == 1)
                formatValue = String.Format("{0:$#,##0.0;($#,##0.000);0.0}", varS);
            else if (number == 2)
                formatValue = String.Format("{0:$#,##0.00;($#,##0.00);0.00}", varS);
            else if (number == 3)
                formatValue = String.Format("{0:$#,##0.000;($#,##0.000);0.000}", varS);
            else if (number == 4)
                formatValue = String.Format("{0:$#,##0.0000;($#,##0.0000);0.0000}", varS);
            else if (number == 0)
                formatValue = String.Format("{0:$#,##0;($#,##0);0}", varS);
            return formatValue;
        }

        public static String nvl(String value)
        {
            String val = "";
            if (value == null || DBNull.Value.Equals(value) || value.Equals(string.Empty))
                val = "";
            else
                val = value;

            return val;
        }
    }

    public static class commonConst
    {
        public static string[] US_STATES = {"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware",
            "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa",
            "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota",
            "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico",
            "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island",
            "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington",
            "West Virginia", "Wisconsin", "Wyoming" };
    }
}
