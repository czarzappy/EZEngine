using System;

namespace ZEngine.Core.Dialogue.Attributes
{
    public class ParameterAttribute : Attribute
    {
        public int ParamIdx;
        
        public ParameterAttribute(int paramIdx)
        {
            ParamIdx = paramIdx;
        }
    }
}