using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class ActionChoiceDef
    {
        public int NumberOfChoicesToSelect = -1;
        
        public readonly List<StageBranchDef> Choices = new List<StageBranchDef>();
    }
}