using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaret.BL.Services
{
    static public class MetinDuzenleyici
    {
        static public string MetinDuzelt(string text)
        {
            return text
                  .Replace("ı", "i")
                  .Replace("İ", "i")
                  .Replace("ğ", "g")
                  .Replace("Ğ", "g")
                  .Replace("ü", "u")
                  .Replace("Ü", "u")
                  .Replace("ş", "s")
                  .Replace("Ş", "s")
                  .Replace("Ö", "o")
                  .Replace("ö", "o")
                  .Replace("ç", "c")
                  .Replace("Ç", "c")
                  .Replace("!", "")
                  .Replace("'", "")
                  .Replace("^", "")
                  .Replace("+", "")
                  .Replace("%", "")
                  .Replace("&", "")
                  .Replace("/", "")
                  .Replace("(", "")
                  .Replace(")", "")
                  .Replace("=", "")
                  .Replace("?", "")
                  .Replace("_", "")
                  .Replace("-", "")
                  .Replace(" ", "-")
                  .Replace("@", "")
                  .Replace("€", "")
                  .Replace("₺", "")
                  .Replace("~", "")
                  .Replace("$", "")
                  .Replace(">", "")
                  .Replace("£", "")
                  .Replace("#", "")
                  .Replace("½", "")
                  .Replace("{", "")
                  .Replace("[", "")
                  .Replace("]", "")
                  .Replace("}", "")
                  .Replace("\\", "")
                  .Replace("|", "")
                  .Replace("*", "").ToLower();
        }
    }
}
