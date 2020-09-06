using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class BriefingDefParser : DefParser<BriefingDef>
    {
        public override string Keyword
        {
            get => "BRIEFING";
        }
        public override void ParseOptions(string[] options)
        {
            // throw new System.NotImplementedException();
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            // throw new System.NotImplementedException();
        }

        public override void ParseMisc(string content)
        {
            // throw new System.NotImplementedException();
        }

        public override void ParseChildToken(object obj)
        {
            // throw new System.NotImplementedException();
        }

        public override IDefParser Clone()
        {
            return new BriefingDefParser();
        }
    }
}