using System;

namespace ZEngine.Core.Dialogue.Attributes
{
    public class ParserAttribute : Attribute
    {
        public Type ParseType;
        public ParserAttribute(Type type)
        {
            ParseType = type;
        }
    }
}