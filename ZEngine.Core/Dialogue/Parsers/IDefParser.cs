using System;

namespace ZEngine.Core.Dialogue.Parsers
{
    public abstract class DefParser<T> : IDefParser where T : new()
    {
        protected T def;

        public object Def
        {
            get => def;
            set => def = (T) value;
        }

        private Type mDefType;
        public Type DefType
        {
            get
            {
                if (mDefType == null)
                {
                    mDefType = typeof(T);
                }

                return mDefType;
            }
        }

        public DefParser()
        {
            
        }
        
        public abstract string Keyword { get; }
        public abstract void ParseOptions(string[] options);
        public abstract void ParseCue(string cueToken, string cueData);
        public abstract void ParseMisc(string content);
        public abstract void ParseChildToken(object obj);

        public virtual void ParseBreak()
        {
            
        }

        public abstract IDefParser Clone();

        // public abstract void ParseNode(TokenNode rootToken);
    }
    
    public interface IDefParser
    {
        string Keyword { get; }
        object Def { get; set; }
        Type DefType { get; }
        void ParseOptions(string[] options);
        void ParseCue(string cueToken, string cueData);
        void ParseMisc(string line);
        void ParseChildToken(object obj);
        // void ParseNode(TokenNode rootToken);
        void ParseBreak();
        IDefParser Clone();
    }
}