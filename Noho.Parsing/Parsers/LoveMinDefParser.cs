using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class LoveMinDefParser : DefParser<LoveMinDef>
    {
        public override string Keyword => "LOVE_MIN";

        public override IDefParser Clone()
        {
            return new LoveMinDefParser();
        }
        public override void ParseOptions(string[] options)
        {
            switch (options.Length)
            {
                case 1:
                    def.MinAmount = int.Parse(options[0]);
                    break;
                
                case 2:
                    def.CharacterDevName = options[0];
                    def.MinAmount = int.Parse(options[1]);
                    break;
            }
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            foreach (var action in DialogueParser.ParseCueNoName( cueToken, cueData))
            {
                def.TrueBranch.Add(action);
            }
        }

        public override void ParseMisc(string content)
        {
            var dialogue = StageActionFactory.NewDialogueAction(null, content);
            
            def.TrueBranch.Add(dialogue);
            
        }

        public override void ParseChildToken(object obj)
        {
            if (DialogueParser.TryParseDialogueObj(obj, out var action))
            {
                def.TrueBranch.Add(action);
                return;
            }
            
            switch (obj)
            {
                default:
                    throw new InvalidOperationException($"[LOVE_MIN] Obj: {obj}");
            }
        }
    }
}