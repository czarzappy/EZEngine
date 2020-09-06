using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class EpisodeDefParser : DefParser<EpisodeDef>
    {
        public override string Keyword => "EPISODE";
        public override void ParseOptions(string[] options)
        {
            throw new System.NotImplementedException();
        }
        
        public override IDefParser Clone()
        {
            return new EpisodeDefParser();
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            switch (cueToken)
            {
                case "NAME":
                    def.Name = cueData;
                    break;
                
                case "DESC":
                    def.Desc = cueData;
                    break;

                case "THUMBNAIL":
                    def.Thumbnail = cueData;
                    break;

            }
        }

        public override void ParseMisc(string content)
        {
            throw new System.NotImplementedException();
        }

        public override void ParseChildToken(object obj)
        {
            def.SceneItems.Add(obj);
        }
    }
}