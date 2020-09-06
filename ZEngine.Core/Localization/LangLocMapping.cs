using System.Collections.Generic;

namespace ZEngine.Core.Localization
{
    public class LangLocMapping
    {
        private readonly Dictionary<LocId, string> mDict = new Dictionary<LocId, string>();
        
        public string this[LocId locId]
        {
            get => mDict[locId];
            set => mDict[locId] = value;
        }
    }
}