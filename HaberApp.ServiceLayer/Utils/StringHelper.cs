﻿using System.Text.RegularExpressions;

namespace HaberApp.ServiceLayer.Utils
{
    public static class StringHelper
    {
        public static string KarakterDuzelt(string s)
        {
            s = s.ToLower();
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            if (s.Length > 150)
            {
                s = s.Substring(0, 150);
            }

            s = s.Replace("ş", "s");
            s = s.Replace("Ş", "S");
            s = s.Replace("ğ", "g");
            s = s.Replace("Ğ", "G");
            s = s.Replace("İ", "I");
            s = s.Replace("ı", "i");
            s = s.Replace("ç", "c");
            s = s.Replace("Ç", "C");
            s = s.Replace("ö", "o");
            s = s.Replace("Ö", "O");
            s = s.Replace("ü", "u");
            s = s.Replace("Ü", "U");
            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            //if (r.IsMatch(s))
            s = r.Replace(s, "-");
            if (!string.IsNullOrEmpty(s))
            {
                while (s.IndexOf("--") > -1)
                {
                    s = s.Replace("--", "-");
                }
            }

            if (s.StartsWith("-"))
            {
                s = s.Substring(1);
            }

            if (s.EndsWith("-"))
            {
                s = s.Substring(0, s.Length - 1);
            }

            return s;

        }
    }
}

