using System.Collections.Generic;
using ZEngine.Core.Dialogue.Models;

namespace ZEngine.Core.Dialogue.Parsers
{
    public static class ZParse
    {
        public static IEnumerable<LineMeta> File(string filepath)
        {
            return System.IO.File.ReadLines(filepath).AsParserLines();
        }
    }
}