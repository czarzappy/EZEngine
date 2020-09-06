using System;

namespace ZEngine.Core.Dialogue.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class KeywordAttribute : Attribute
    {
        public readonly string Keyword;
        
        public KeywordAttribute(string keyword)
        {
            Keyword = keyword;
        }
    }
}