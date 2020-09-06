using System;
using System.Collections.Generic;
using System.Linq;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Utils;

namespace ZEngine.Core.Dialogue.Parsers
{
    public class HierarchicalScriptParser : ScriptParser
    {
        public override IEnumerable<T> Deserialize<T>(IEnumerable<LineMeta> lines)
        {
            List<TokenNode> rootTokens = GenerateTokens(lines);

            Type resultType = typeof(T);
            
            List<ParsingError> errors = new List<ParsingError>();
            foreach (TokenNode rootToken in rootTokens)
            {
                if (rootToken.Type == TokenType.BREAK)
                {
                    continue;
                }
                
                if (rootToken.Type != TokenType.KEYWORD)
                {
                    throw new InvalidOperationException($"Expected keyword token: {rootToken.Type}, found token, content {rootToken.Content}");
                }
                
                if (!ParserKeywordRegistry.TryGetValue(rootToken.Keyword, out IDefParser parser))
                {
                    throw new InvalidOperationException($"Unknown top-level token keyword: {rootToken.Keyword}");
                }

                T data = new T();

                if (parser.DefType != resultType)
                {
                    throw new InvalidOperationException($"Expected last parsed keyword: {rootToken.Keyword} to parse as {resultType}");
                }
                
                ZLog.Info("[ROOT]");
                ZLog.Info(rootToken.ToString());

                parser.Def = data;
                ParseNode(null, parser, rootToken, errors);
                
                CheckForErrors(rootToken, errors);

                yield return data;
            }
        }
        

        public override IEnumerable<object> Deserialize(IEnumerable<LineMeta> lines)
        {
            List<object> results = new List<object>();
            List<TokenNode> rootTokens = GenerateTokens(lines);

            // Type resultType = typeof(T);
            
            List<ParsingError> errors = new List<ParsingError>();
            foreach (TokenNode rootToken in rootTokens)
            {
                if (rootToken.Type == TokenType.BREAK)
                {
                    continue;
                }
                
                if (rootToken.Type != TokenType.KEYWORD)
                {
                    throw new InvalidOperationException($"Expected keyword token, found {rootToken.ToString_Internal(false)}");
                }
                
                if (!ParserKeywordRegistry.TryGetValue(rootToken.Keyword, out IDefParser parser))
                {
                    throw new InvalidOperationException($"Unknown top-level token keyword: {rootToken.ToString_Internal(false)}");
                }

                Type dataType = parser.DefType;
                
                object data = Activator.CreateInstance(dataType);

                // if (parser.DefType != resultType)
                // {
                //     throw new InvalidOperationException($"Expected last parsed keyword: {rootToken.Keyword} to parse as {resultType}");
                // }

                parser.Def = data;
                ParseNode(null, parser, rootToken, errors);

                CheckForErrors(rootToken, errors);

                // yield return data;
                results.Add(data);
            }

            return results;
        }

        public void CheckForErrors(TokenNode rootToken, List<ParsingError> errors)
        {
            if (errors.Count > 0)
            {
                foreach (ParsingError error in errors)
                {
                    ZLog.Error($"Error parsing root script node - {rootToken.ToString_Internal(false)}, error: {error.Exception.Message}");
                }

                ZLog.Error($"{rootToken}");
                throw new AggregateException($"Error parsing root script node - {rootToken.ToString_Internal(false)}", errors.Select((error => error.Exception)));
            }
        }
        
        public struct ParsingError
        {
            public TokenNode Node;
            public Exception Exception;
        }

        private void ParseNode(IDefParser parentParser, IDefParser defParser, TokenNode rootToken, List<ParsingError> errors)
        {
            if (rootToken.Data != null)
            {
                try
                {
                    defParser.ParseOptions(rootToken.Options);
                }
                catch (Exception e)
                {
                    errors.Add(new ParsingError
                    {
                        Node = rootToken,
                        Exception = e,
                    });
                }
            }

            if (rootToken.Children == null)
            {
                throw new InvalidOperationException($"Expected keyword token to contain children: {rootToken}, parentParser: {parentParser}");
            }

            foreach (TokenNode childToken in rootToken.Children)
            {
                try
                {
                    switch (childToken.Type)
                    {
                        case TokenType.KEYWORD:
                            if (childToken.IsLeaf)
                            {
                                try
                                {
                                    defParser.ParseCue(childToken.Keyword, childToken.Data);
                                }
                                catch (Exception e)
                                {
                                    errors.Add(new ParsingError
                                    {
                                        Node = childToken,
                                        Exception = e,
                                    });
                                }
                            }
                            else
                            {
                                if (!GetParser(childToken.Keyword, out IDefParser subParser))
                                {
                                    throw new InvalidOperationException($"Unknown top-level token keyword: {childToken.Keyword}");
                                }

                                Type subtype = subParser.DefType;

                                object subdata = Activator.CreateInstance(subtype);

                                subParser.Def = subdata;
                                ParseNode(defParser, subParser, childToken, errors);
                                subParser.Def = null;
                                
                                // object data = //n
                    
                                // childToken.Keyword
                                try
                                {
                                    defParser.ParseChildToken(subdata);
                                }
                                catch (Exception e)
                                {
                                    errors.Add(new ParsingError
                                    {
                                        Node = childToken,
                                        Exception = e,
                                    });
                                }
                            }
                            break;
                    
                        case TokenType.MISC:

                            if (childToken.Content != null)
                            {
                                defParser.ParseMisc(childToken.Content);
                            }
                            break;
                    
                        case TokenType.BREAK:

                            defParser.ParseBreak();
                            break;
                    
                        default:
                            ZLog.Warn($"Unhandled token type: {childToken.Type}");
                            break;
                    }
                }
                catch (Exception e)
                {
                    errors.Add(new ParsingError
                    {
                        Node = childToken,
                        Exception = e,
                    });
                }
            }
        }

        private bool GetParser(string childTokenKeyword, out IDefParser defParser)
        {
            // uses parsers in place
            bool result = ParserKeywordRegistry.TryGetValue(childTokenKeyword, out IDefParser keywordParser);
            if (!result)
            {
                defParser = default;
                return false;
            }

            defParser = keywordParser.Clone();
            return true;
        }

        private static List<TokenNode> GenerateTokens(IEnumerable<LineMeta> lines)
        {
            var capturedTokens = new Stack<TokenNode>();
            foreach (LineMeta meta in lines)
            {
                string line = meta.Line;
                // ZLog.Info($"[PASS 1] {line}, tokens: {capturedTokens.Count}");

                if (!line.StartsWith(ParseTokens.CUE_CHAR, out string cueLine))
                {
                    TokenNode node;
                    if (line == "")
                    {
                        node = new TokenNode
                        {
                            Type = TokenType.BREAK,
                            LineNumber = meta.LineNumber,
                        };
                    }
                    else
                    {
                        node = new TokenNode
                        {
                            Type = TokenType.MISC,
                            LineNumber = meta.LineNumber,
                            Content = line
                        };
                    }

                    capturedTokens.Push(node);
                    // TokenNode previousNode = capturedTokens.Peek();
                    //
                    // if (previousNode.Content == null)
                    // {
                    //     previousNode.Content = line;
                    // }
                    // else
                    // {
                    //     previousNode.Content += $"{Environment.NewLine}{line}";
                    // }
                }
                else
                {
                    string cueToken = ScriptConstants.ParseCueToken(cueLine, out string cueData);
                    // ZLog.Info($"[TOKEN] {cueToken}");

                    if (cueToken.StartsWith(ParseTokens.CUE_END_TOKEN, out string endCueToken))
                    {
                        // ZLog.Info($"Found end token: {cueToken}");

                        if (capturedTokens.Count == 0)
                        {
                            throw new InvalidOperationException($"No captured items for token group: {endCueToken}, line: {meta.LineNumber}");
                        }

                        List<TokenNode> nodes = new List<TokenNode>();
                        TokenNode captureGroup;
                        while ((captureGroup = capturedTokens.Pop()) != null)
                        {
                            if (captureGroup.Keyword == endCueToken)
                            {
                                if (captureGroup.Children == null)
                                {
                                    // found matching start token, has empty children
                                    break;
                                }
                            }
                            
                            // ZLog.Info($"{captureGroup.Keyword} part of capture group, remaining: {capturedTokens.Count}");
                            nodes.Add(captureGroup);

                            if (capturedTokens.Count == 0)
                            {
                                throw new InvalidOperationException(
                                    $"No start token to capture group found: {endCueToken}, line: {meta.LineNumber}");
                            }
                        }

                        nodes.Reverse();

                        captureGroup.Children = nodes;

                        capturedTokens.Push(captureGroup);


                        continue;
                    }

                    TokenNode node = new TokenNode
                    {
                        Type = TokenType.KEYWORD,
                        LineNumber = meta.LineNumber,
                        Keyword = cueToken,
                        Data = cueData
                    };

                    capturedTokens.Push(node);
                    //
                    // ZLog.Info($"[NODE] {node}");
                }
            }

            // since this is built up in reverse, we have to reverse it again to negate the effect
            var result = capturedTokens.ToList();
            result.Reverse();
            return result;
        }
    }
}