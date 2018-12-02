/**
* 命名空间: Common
*
* 功 能： N/A
* 类 名： CryptoHelper
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/2 13:33:44 熊仔其人 xxh 4.0.30319.42000 初版
*
* Copyright (c) 2018 熊仔其人 Corporation. All rights reserved.
*┌─────────────────────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．   │
*│　版权所有：熊仔其人 　　　　　　　　　　　　　　　　　　　　　　　 │
*└─────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class CryptoHelper
    {
        public static string md5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for(int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }
        public static string GetMD5(string s, string sEncoding = "GB2312")
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(sEncoding).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for(int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        private static byte[] _IV_64 = { 0x05, 0x07, 0x00, 0x09, 0x00, 0xF0, 0xEE, 0x13 };
        private static string _KEY_64 = "mcfrkknd";
        // DES加密
        public static byte[] EDES(string s, string KEY_64 = null, byte[] IV_64 = null)
        {
            if(string.IsNullOrEmpty(KEY_64)) KEY_64 = _KEY_64;
            if(IV_64 == null) IV_64 = _IV_64;
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.BlockSize = 64;
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, IV_64), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cst);
            sw.Write(s);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();
            byte[] BB = new byte[ms.Length];
            Array.Copy(ms.GetBuffer(), 0, BB, 0, BB.Length);
            return BB;
        }
        // DES解密
        public static string DDES(byte[] byEnc, string KEY_64 = null, byte[] IV_64 = null)
        {
            if(byEnc == null) return "";
            if(string.IsNullOrEmpty(KEY_64)) KEY_64 = _KEY_64;
            if(IV_64 == null) IV_64 = _IV_64;
            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            string s = "";
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            cryptoProvider.BlockSize = 64;
            MemoryStream ms = new MemoryStream(byEnc);
            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, IV_64), CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cst);
            s = sr.ReadToEnd();
            return s;
        }
        public static string EDES_BASE64(string s, string KEY_64 = null, byte[] IV_64 = null)
        {
            byte[] BB = EDES(s, KEY_64, IV_64);
            return Convert.ToBase64String(BB);

        }
        public static string DDES_BASE64(string s, string KEY_64 = null, byte[] IV_64 = null)
        {
            byte[] BB = Convert.FromBase64String(s);
            return DDES(BB, KEY_64, IV_64);

        }

        /// <summary>
        /// 将明文生成密码
        /// </summary>
        /// <param name="input">明文字符串</param>
        /// <returns></returns>
        public static string GeneratePassword(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
                return string.Empty;

            /*
             * 明文：123456
             * 密文：34a4c18156a61d43b68ab17f5aef306a
             */
            return md5(EDES_BASE64(input));
        }

    }
}
