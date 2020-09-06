using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class CheckFlagDef
    {
        public string FlagTag;
        public bool FlagExpectedValue;
        public readonly List<object> TrueBranch = new List<object>();
    }
}