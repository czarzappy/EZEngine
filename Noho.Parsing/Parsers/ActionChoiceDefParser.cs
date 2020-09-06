using System;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class ActionChoiceDefParser : DefParser<ActionChoiceDef>
    {
        public override string Keyword => "ACTION_CHOICE";

        public override IDefParser Clone()
        {
            return new ActionChoiceDefParser();
        }
        public override void ParseOptions(string[] options)
        {
            if (options.Length == 0)
            {
                return;
            }
            
            
            def.NumberOfChoicesToSelect = int.Parse(options[0]);
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            switch (cueToken)
            {
                default:
                    throw new System.InvalidOperationException($"Unknown cue token: {cueToken}, data: {cueData}");
            }
        }

        public override void ParseMisc(string content)
        {
            throw new InvalidOperationException();
        }

        public override void ParseChildToken(object obj)
        {
            switch (obj)
            {
                case StageBranchDef choiceDef:
                    def.Choices.Add(choiceDef);
                    break;
                
                default:
                    throw new InvalidOperationException($"Object: {obj}");
            }
        }
    }
}