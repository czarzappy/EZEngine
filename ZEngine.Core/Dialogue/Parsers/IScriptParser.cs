using System;
using System.Collections.Generic;
using ZEngine.Core.Dialogue.Models;

namespace ZEngine.Core.Dialogue.Parsers
{
    public abstract class ScriptParser : IScriptParser
    {
        protected readonly Dictionary<string, IDefParser> ParserKeywordRegistry = new Dictionary<string, IDefParser>();
        protected readonly Dictionary<Type, IDefParser> ParserTypeRegistry = new Dictionary<Type, IDefParser>();

        public void Register(IDefParser defParser)
        {
            ParserKeywordRegistry[defParser.Keyword] = defParser;
            ParserTypeRegistry[defParser.DefType] = defParser;
        }

        public void GetInfo(object def)
        {
            throw new NotImplementedException();
        }

        // public abstract IEnumerable<T> Deserialize<T>(IEnumerable<(string line, int lineNumber)> lines) where T : new();

        public abstract IEnumerable<T> Deserialize<T>(IEnumerable<LineMeta> lines) where T : new();
        public abstract IEnumerable<object> Deserialize(IEnumerable<LineMeta> lines);
    }
    
    public interface IScriptParser
    {
        void Register(IDefParser defParser);
        void GetInfo(object def);
        // IEnumerable<T> Deserialize<T>(IEnumerable<(string line, int lineNumber)> lines) where T : new();
        IEnumerable<T> Deserialize<T>(IEnumerable<LineMeta> lines) where T : new();
        IEnumerable<object> Deserialize(IEnumerable<LineMeta> lines);
    }
}