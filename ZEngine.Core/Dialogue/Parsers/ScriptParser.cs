using System;
using System.Collections.Generic;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Utils;

namespace ZEngine.Core.Dialogue.Parsers
{
    public class V1ScriptParser : ScriptParser
    {
        // public override IEnumerable<T> Deserialize<T>(IEnumerable<(string, int)> lines)
        // {
        //     Type type = typeof(T);
        //     
        //     IDefParser rootDefParser = ParserTypeRegistry[type];
        //     
        //     var stack = new Stack<IDefParser>();
        //     stack.Push(rootDefParser);
        //
        //     // yield break;
        //     return Deserialize<T>(stack, lines);
        // }

        public IEnumerable<T> Deserialize<T>(string filePath) where T : new()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Deserialize<T>(IEnumerable<LineMeta> lines)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Deserialize(IEnumerable<LineMeta> lines)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Deserialize<T>(Stack<IDefParser> stack, IEnumerable<(string, int)> lines) where T : new()
        {
            IDefParser defParser = stack.Peek();
            Type type = typeof(T);
            
            // IParser<T> rootParser = (IParser<T>) mParserTypeRegistry[type];

            // IParser<T> parser = rootParser;
            T def = new T();

            defParser.Def = def;

            string endCueToken = ParseTokens.CUE_END_TOKEN + defParser;

            // FieldInfo[] fields = type.GetFields();

            bool isFirstLine = true;
            foreach ((string line, int lineNumber) data in lines)
            {
                string line = data.line.Trim();
                int lineNumber = data.lineNumber;
                
                ZLog.Info($"{lineNumber:000}: [RAW] {line}");
                
                // handle cues
                if (line.StartsWith(ParseTokens.CUE_CHAR, out string cueLine))
                {
                    string cueToken = ScriptConstants.ParseCueToken(cueLine, out string cueOptionStr);

                    if (cueToken == endCueToken)
                    {
                        yield return def;
                        // stack.Pop();
                    }

                    if (ParserKeywordRegistry.TryGetValue(cueToken, out IDefParser subParser))
                    {
                        // parser = InitNewParser(subParser);
                        // parser = subParser;
                    }
                    
                    if (isFirstLine)
                    {
                        if (cueToken != defParser.Keyword)
                        {
                            throw new InvalidOperationException($"Expected first cue token to denote type keyword: {defParser.Keyword}, {lineNumber:000}:{line}");
                        }
                        
                        if (cueOptionStr != null)
                        {
                            ZLog.Info($"{lineNumber:000}: [SECTION_START] {defParser.Keyword}, options: {cueOptionStr}");
                            string[] options = cueOptionStr.Split(ParseTokens.CUE_SEPARATOR);
                            defParser.ParseOptions(options);
                        }
                        
                        isFirstLine = false;
                    }
                    else
                    {
                        // if (mParserKeywordRegistry.TryGetValue(cueToken, out object subParser))
                        // {
                        //     object parse = HandleSubParser(subParser);
                        //     // ZLog.Info($"{lineNumber:000}: [CUE] {cueToken}, options: {cueOptionStr}");
                        //     // parser.parse(ref def, cueToken, options);
                        // }
                        
                        if (cueOptionStr != null)
                        {
                            // string[] options = cueOptionStr.Split(ParseTokens.CUE_SEPARATOR);
                            
                            ZLog.Info($"{lineNumber:000}: [CUE] {cueToken}, options: {cueOptionStr}");
                            defParser.ParseCue(cueToken, cueOptionStr);
                        }
                        else
                        {
                            ZLog.Info($"{lineNumber:000}: [CUE] {cueToken}");
                            defParser.ParseCue(cueToken, null);
                        }
                    }
                }
                else
                {
                    ZLog.Warn($"{lineNumber:000}: [MISC] {line}");
                    defParser.ParseMisc(line);
                }
            }

            yield return def;
        }

        // private IParser InitNewParser(IParser subParser)
        // {
        //    
        // }

        private object HandleSubParser(object subParser)
        {
            // IParser parser = (IParser) subParser;

            return null;
        }
    }
}