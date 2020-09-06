using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

namespace ZEngine.Core.Utils
{
    public static class StringUtil
    {
        public static bool TryIndexOf(this string s, char value, out int index)
        {
            return (index = s.IndexOf(value)) != -1;
        }
        
        public static bool TryIndexOf(this string s, string value, StringComparison comparisonType, out int index)
        {
            return (index = s.IndexOf(value, comparisonType)) != -1;
        }
        
        public static bool TryIndexOf(this string s, string value, out int index)
        {
            return (index = s.IndexOf(value, StringComparison.Ordinal)) != -1;
        }

        public static string SubstringAndTrim(this string s, int startIndex)
        {
            // ReadOnlyCollection<char> c = new ReadOnlyCollection<char>(s.ToCharArray());
            
            // int slen = s.Length;
            // if (slen == 0)
            // {
            //     return string.Empty;
            // }
            //
            // StringWriter sw = new StringWriter();
            // for (int sid = startIndex; sid < slen; sid++)
            // {
            //     char c = s[sid];
            //
            //     if (char.IsWhiteSpace(c))
            //     {
            //         continue;
            //     }
            //     
            //     sw.Write(c);
            // }
            //
            // return sw.ToString();
            return s.Substring(startIndex).Trim();
        }

        public static string SubstringAndTrim(this string s, int startIndex, int len)
        {
            int slen = s.Length;
            if (slen == 0)
            {
                return string.Empty;
            }

            int sublen = slen - startIndex;

            if (len > sublen)
            {
                throw new IndexOutOfRangeException("Attempting to substring");
            }
            
            StringWriter sw = new StringWriter();
            for (int i = 0, sid = startIndex; i < len; i++, sid++)
            {
                char c = s[sid];

                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                
                sw.Write(c);
            }

            return sw.ToString();
        }

        public static bool StartsWith(this string s, string sub, out string trimmedSubstring)
        {
            if (s.StartsWith(sub))
            {
                trimmedSubstring = s.SubstringAndTrim(sub.Length);
                return true;
            }

            trimmedSubstring = null;
            return false;
        }

        public static bool StartsWith(this string s, char c, out string trimmedSubstring)
        {
            if (s.Length == 0)
            {
                trimmedSubstring = null;
                return false;
            }
            
            if (s[0] == c)
            {
                trimmedSubstring = s.SubstringAndTrim(1);
                return true;
            }

            trimmedSubstring = null;
            return false;
        }

        public static string SubstringBefore(this string s, int toIdx)
        {
            return s.Substring(0, toIdx);
        }
    }
}