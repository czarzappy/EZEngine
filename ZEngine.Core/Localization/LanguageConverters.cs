using System;
using System.IO;

namespace ZEngine.Core.Localization
{
    public static class LanguageConverters
    {
        public static string ToAsterisks(this string text)
        {
            StringWriter sw = new StringWriter();

            string[] parts = text.Split(new[]{Environment.NewLine}, StringSplitOptions.None);

            for (int partIdx = 0; partIdx < parts.Length; partIdx++)
            {
                string part = parts[partIdx];
                
                for (int i = 0; i < part.Length; i++)
                {
                    var character = text[i];
                
                    if (character == ' ')
                    {
                        sw.Write(' ');
                    }
                    else
                    {
                        sw.Write("*");
                    }
                }

                if (partIdx != parts.Length - 1)
                {
                    sw.Write(Environment.NewLine);
                }
            }

            return sw.ToString();
        }
    }
}