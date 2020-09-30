using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.IO;
using System.Text;
namespace Nexus_Service_Marketing_System.Responsitory
{
    public class DataSecurity
    {
        private static byte[] Key = { 
            0x01,0x02,0x03,0x04,
            0x05,0x06,0x07,0x08,
            0x09,0x10,0x11,0x12,
            0x13,0x14,0x15,0x16 };
        private static byte[] IV = { 
            0x01,0x02,0x03,0x04,
            0x05,0x06,0x07,0x08,
            0x09,0x10,0x11,0x12,
            0x13,0x14,0x15,0x16 };
        public static string EncrytPassWord(string planText) 
        {
            byte[] planTextArr = Encoding.UTF8.GetBytes(planText);
            RijndaelManaged rm = new RijndaelManaged();
            MemoryStream ms = new MemoryStream();
            CryptoStream  cs = new CryptoStream(ms, rm.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            cs.Write(planTextArr, 0, planText.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }
        public static string DescrytPassWord(string planText)
        {
            byte[] planTextArr = Convert.FromBase64String(planText);//Change
            RijndaelManaged rm = new RijndaelManaged();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, rm.CreateDecryptor(Key, IV), CryptoStreamMode.Write);//Change
            cs.Write(planTextArr, 0, planText.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());//Change
        }
    }
}