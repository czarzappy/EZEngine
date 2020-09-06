using System;
using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    [Serializable]
    public class PhaseParserSettings
    {
        public List<string> KnownSpeakerNames;
        public List<string> KnownInitGunks;
        public List<string> KnownIndexedGunks;
    }
}