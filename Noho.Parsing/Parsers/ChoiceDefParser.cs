using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class ChoiceDefParser : DefParser<StageBranchDef>
    {
        public override string Keyword => "CHOICE";

        public override IDefParser Clone()
        {
            return new ChoiceDefParser();
        }
        public override void ParseOptions(string[] options)
        {
            throw new InvalidOperationException($"[CHOICE] options: {string.Join(",", options)}");
        }

        private string mCurrentCharacterDevName = null;
        public override void ParseCue(string cueToken, string cueData)
        {
            switch (cueToken)
            {
                case "BREAK":
                    
                    def.Actions.Add(StageActionFactory.GenerateBreakAction());
                    
                    break;
                default:
                    mCurrentCharacterDevName = cueToken;

                    foreach (var action in DialogueParser.ParseCueNoName(cueToken, cueData))
                    {
                        def.Actions.Add(action);
                    }

                    break;
            }
        }

        public override void ParseMisc(string content)
        {
            var dialogue = StageActionFactory.NewDialogueAction(mCurrentCharacterDevName, content);
            
            def.Actions.Add(dialogue);
        }

        public override void ParseChildToken(object obj)
        {
            if (DialogueParser.TryParseDialogueObj(obj, out var action))
            {
                def.Actions.Add(action);
                return;
            }
            
            switch (obj)
            {
                default:
                    throw new InvalidOperationException($"[CHOICE] Obj: {obj}");
            }
        }
    }
}