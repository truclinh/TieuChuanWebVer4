﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TieuChuanWebVer4.Models
{
    public class Encryptor
    {
        public static string MDSHash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //computer hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            //get hash result after compute it
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for(int i=0; i<result.Length;i++)
            {
                //change it into 2 hexadecial digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}