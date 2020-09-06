using System;
using System.Collections.Generic;
using System.IO;
using ZEngine.Core.Dialogue.Models;

namespace ZEngine.Core.Dialogue.Parsers
{
    public enum TokenType
    {
        KEYWORD,
        MISC,
        BREAK,
    }
    public class TokenNode
    {
        public TokenType Type;
        public string Keyword;
        public string Data;

        // 1-indexed
        public int LineNumber;
        // public int StartingLineNumber;

        public TokenNode()
        {
            
        }

        public string[] Options => Data?.Split(ParseTokens.CUE_SEPARATOR);

        public string Content;

        public List<TokenNode> Children;

        public bool IsLeaf
        {
            get { return Children == null || Children.Count == 0; }
        }

        public string ToString_Internal(bool recurse = true, string tab = "")
        {
            StringWriter sw = new StringWriter();
            switch (Type)
            {
                case TokenType.MISC:
                    
                    sw.Write($"Line {LineNumber:000}: {tab}[MISC] \"{Content}\"");
                    
                    return sw.ToString();
                
                case TokenType.BREAK:
                    
                    sw.Write($"Line {LineNumber:000}: {tab}[BREAK]");
                    
                    return sw.ToString();
                
                case TokenType.KEYWORD:
            
                    sw.Write($"Line {LineNumber:000}: {tab}[KEYWORD] {Keyword}");

                    if (Data != null)
                    {
                        sw.Write($" - {Data}");
                    }
                    
                    if (recurse && Children != null)
                    {
                        sw.WriteLine();
                        foreach (TokenNode child in Children)
                        {
                            sw.WriteLine(child.ToString_Internal(true, $"{tab}\t"));
                        }
                    }

                    return sw.ToString();
                
                default:
                    throw new InvalidOperationException($"Unknown token node type: {Type}");
            }
        }
        
        public override string ToString()
        {
            return ToString_Internal();
        }
    }
}