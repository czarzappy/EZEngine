using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class CheckFlagDefParser : DefParser<CheckFlagDef>
    {
        public override string Keyword => "CHECK_FLAG";

        public CheckFlagDefParser()
        {
        }
        public override void ParseOptions(string[] options)
        {
            if (options.Length != 2)
            {
                throw new InvalidOperationException("[CHECK_FLAG] Expected two parameters");
            }

            def.FlagTag = options[0];
            def.FlagExpectedValue = bool.Parse(options[1]);
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
                    throw new InvalidOperationException($"[CHECK_FLAG] Obj: {obj}");
            }
        }

        public override IDefParser Clone()
        {
            return new CheckFlagDefParser();
        }
    }
}