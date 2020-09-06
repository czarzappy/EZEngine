using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class PreOpDefParser : DefParser<CutSceneDef>
    {
        public override string Keyword => "PREOP";

        public override IDefParser Clone()
        {
            return new PreOpDefParser();
        }
        
        public override void ParseOptions(string[] options)
        {
            ZLog.Info($"[PREOP] [ParseOptions] options: {options}");
        }


        private string mCurrentCharacterDevName;
        public override void ParseCue(string cueToken, string cueData)
        {
            switch (cueToken)
            {
                case "VICTIMS":
                    // def.NumVictims = int.Parse(cueData);
                    break;
                
                case "BG":
                    def.Actions.Add(StageActionFactory.NewBackground(cueData));
                    break;

                case "BGM":
                    def.Actions.Add(StageActionFactory.NewBackgroundMusic(cueData));
                    break;

                case "VO":
                    def.Actions.Add(StageActionFactory.NewVOAction(cueData));
                    break;

                default:
                    mCurrentCharacterDevName = cueToken;

                    foreach (var action in DialogueParser.ParseCue(mCurrentCharacterDevName, cueData))
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
                    throw new InvalidOperationException($"Unknown child: {obj}");
            }
        }
    }
}