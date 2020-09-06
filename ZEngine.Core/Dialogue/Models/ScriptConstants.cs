using System;
using System.Collections.Generic;
using System.Linq;
using ZEngine.Core.Dialogue.Parsers;
using ZEngine.Core.Utils;

namespace ZEngine.Core.Dialogue.Models
{
    public static class ScriptConstants
    {
        public static int[] AsIntArray(this string cueData)
        {
            if (string.IsNullOrEmpty(cueData))
            {
                return null;
            }

            var parts = cueData.Split(ParseTokens.CUE_SEPARATOR);

            int len = parts.Length;
            int[] results = new int[len];
            for (int pidx = 0; pidx < len; pidx++)
            {
                results[pidx] = int.Parse(parts[pidx]);
            }

            return results;
        }
        public static string TrimComments(string s)
        {
            foreach (string commentPrefix in ParseTokens.COMMENT_PREFIXES)
            {
                int commentIdx = s.IndexOf(commentPrefix, StringComparison.Ordinal);
                if (commentIdx == -1)
                {
                    continue;
                }
                // if (!s.StartsWith(commentPrefix, StringComparison.Ordinal))
                // {
                //     continue;
                // }

                // return string.Empty;
                // int commentIdx = 0;

                s = s.SubstringBefore(commentIdx);

                if (s.Length == 0)
                {
                    return string.Empty;
                }
            }

            return s;
        }

        public static string ParseCueToken(string cueLine, out string cueData)
        {
            int optionIdx = cueLine.IndexOf(ParseTokens.CUE_OPTIONS);
            if (optionIdx != -1)
            {
                cueData = cueLine.SubstringAndTrim(optionIdx + 1);
                return cueLine.SubstringBefore(optionIdx);
            }

            optionIdx = cueLine.IndexOf(ParseTokens.CUE_SEPARATOR);

            if (optionIdx != -1)
            { 
                cueData = cueLine.SubstringAndTrim(optionIdx + 1);
                return cueLine.SubstringBefore(optionIdx);
            }

            cueData = null;
            return cueLine; 
        }

        public static IEnumerable<LineMeta> AsParserLines(this IEnumerable<string> lines)
        {
            List<LineMeta> results = new List<LineMeta>();
            bool isBlockComment = false;
            int lineNumber = 1;
            foreach (string rawLine in lines)
            {
                string cleanLine = rawLine.Trim();
                
                if (cleanLine == ParseTokens.BLOCK_COMMENT_START)
                {
                    isBlockComment = true;
                    continue;
                }

                if (cleanLine == ParseTokens.BLOCK_COMMENT_END)
                {
                    isBlockComment = false;
                    continue;
                }

                if (isBlockComment)
                {
                    continue;
                }
                
                cleanLine = TrimComments(cleanLine);
                
                // if (string.IsNullOrWhiteSpace(cleanLine))
                // {
                //     yield return cleanLine;
                // }
                //
                // if (string.IsNullOrEmpty(cleanLine))
                // {
                //     continue;
                // }
                
                // ZLog.Info($"[LINE: {lineNumber}] {cleanLine}");
                
                var newMeta = new LineMeta
                {
                    Line = cleanLine,
                    LineNumber = lineNumber,
                };
                // yield return newMeta;
                results.Add(newMeta);
                lineNumber++;
            }

            return results;
        }
    }
}