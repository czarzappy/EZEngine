using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class PreOpDef
    {
        public string BackgroundAsset;
        public string BackgroundMusicAsset;
        public int NumVictims;
        public readonly List<StageAction> Actions = new List<StageAction>();
    }
}