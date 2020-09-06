using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class LoveMinDef
    {
        /// <summary>
        /// If not set, infer from scope
        /// </summary>
        public string CharacterDevName;
        
        /// <summary>
        /// Minimum amount of character love to see inner content
        /// </summary>
        public int MinAmount;

        public readonly List<object> TrueBranch = new List<object>();

        public static bool operator >=(LoveMinDef def, int value)
        {
            return def.MinAmount >= value;
        }
        
        public static bool operator <=(LoveMinDef def, int value)
        {
            return def.MinAmount <= value;
        }
        
        public static bool operator >=(int value, LoveMinDef def)
        {
            return def.MinAmount >= value;
        }
        
        public static bool operator <=(int value, LoveMinDef def)
        {
            return def.MinAmount <= value;
        }
        
        public static bool operator ==(int value, LoveMinDef def)
        {
            return def != null && def.MinAmount == value;
        }
        
        public static bool operator !=(int value, LoveMinDef def)
        {
            return def != null && def.MinAmount == value;
        }
        
        public static bool operator ==(LoveMinDef def, int value)
        {
            return def != null && def.MinAmount == value;
        }
        
        public static bool operator !=(LoveMinDef def, int value)
        {
            return def != null && def.MinAmount == value;
        }
    }
}