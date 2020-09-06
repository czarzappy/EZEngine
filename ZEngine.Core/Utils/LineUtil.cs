using System.Collections.Generic;
using System.IO;

namespace ZEngine.Core.Utils
{
    public static class LineUtil
    {
        public static IEnumerable<(string line, int lineNumber)> GetLinesFromFile(string filepath)
        {
            var lines = File.ReadLines(filepath);
            
            int lineNumber = 0;
            foreach (string line in lines)
            {
                lineNumber++;
                yield return (line, lineNumber);
            }
        }

        public static IEnumerable<(string line, int lineNumber)> GetLines(IEnumerable<string> lines)
        {
            int lineNumber = 0;
            foreach (string line in lines)
            {
                yield return (line, lineNumber++);
            }
        }
        
        public static IEnumerable<(string line, int lineNumber)> GetLinesWithNumbers(string text)
        {
            using (StringReader reader = new StringReader(text))
            {
                int lineNumber = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return (line, lineNumber++);
                }
            }
        }
        
        public static IEnumerable<string> GetLines(string text)
        {
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
        
    }
}