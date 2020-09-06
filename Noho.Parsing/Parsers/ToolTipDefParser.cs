using System;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class ToolTipDefParser : DefParser<ToolTipDef>
    {
        // private readonly ParserSettings mParserSettings;

        // public ToolTipDefParser(ParserSettings parserSettings)
        // {
        //     mParserSettings = parserSettings;
        // }

        public override string Keyword => "TOOLTIP";
        
        public ToolTipDefParser()
        {
        }

        public override IDefParser Clone()
        {
            return new ToolTipDefParser();
        }
        public override void ParseOptions(string[] options)
        {
            def.Tag = options[0];
        }

        private ToolTipSpeakerDef mLastSpeakerDef;
        public override void ParseCue(string cueToken, string cueData)
        {
            if (int.TryParse(cueToken, out int stepId))
            {
                mLastSpeakerDef.Steps.Add(new ToolTipProcedureStepsDef
                {
                    StepId = stepId,
                    Dialogue = cueData
                });
                return;
            }
            
            mLastSpeakerDef = new ToolTipSpeakerDef
            {
                SpeakerDevName = cueToken,
            };
                
            def.Speakers.Add(mLastSpeakerDef);
        }

        public override void ParseMisc(string content)
        {
            throw new System.NotImplementedException();
        }

        public override void ParseChildToken(object obj)
        {
            throw new System.NotImplementedException();
        }
    }
}