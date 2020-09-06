using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class CutSceneDefParser : DefParser<CutSceneDef>
    {
        public override string Keyword => "CUTSCENE";

        private readonly ParserSettings mSettings;
        
        public CutSceneDefParser(ParserSettings settings)
        {
            mSettings = settings;
        }

        public override IDefParser Clone()
        {
            return new CutSceneDefParser(mSettings);
        }
        
        public override void ParseOptions(string[] options)
        {
            throw new System.NotImplementedException();
        }

        private string mCurrentCharacterDevName;
        public override void ParseCue(string cueToken, string cueData)
        {
            if (DialogueParser.TryGetStageActionForCue(cueToken, cueData, out var stageAction))
            {
                def.Actions.Add(stageAction);
                return;
            }
            
            switch (cueToken)
            {
                case "TITLE":
                    def.SceneTitle = cueData;
                    break;

                case "SUBTITLE":
                    def.SceneDescription = cueData;
                    break;

                default:

                    if (!mSettings.KnownSpeakerNames.Contains(cueToken))
                    {
                        throw new InvalidOperationException($"[CUTSCENE] Unknown cue token: {cueToken}");
                    }
                    
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
            if (DialogueParser.TryParseDialogueObj(obj, out StageAction stageAction))
            {
                def.Actions.Add(stageAction);
                return;
            }
            
            switch (obj)
            {
                default:
                    throw new InvalidOperationException($"[CUTSCENE] Unknown child: {obj}");
            }
        }
    }
}