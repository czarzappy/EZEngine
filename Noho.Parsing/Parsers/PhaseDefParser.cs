using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class PhaseDefParser : DefParser<PhaseDef>
    {
        private readonly PhaseParserSettings mSettings;
        public override string Keyword => "PHASE";
        public PhaseDefParser(PhaseParserSettings settings)
        {
            mSettings = settings;
        }

        public override IDefParser Clone()
        {
            return new PhaseDefParser(mSettings);
        }

        public override void ParseOptions(string[] options)
        {
            def.SurgerySceneName = options[0];
        }

        private StageAction mLastDialogueAction;

        private string mCurrentCharacterDevName;

        public override void ParseMisc(string content)
        {
            if (mLastDialogueAction != null)
            {
                if (mLastDialogueAction.DialogueText == "")
                {
                    mLastDialogueAction.DialogueText = content;
                }
                else
                {
                    mLastDialogueAction.DialogueText = $"{mLastDialogueAction.DialogueText}{Environment.NewLine}{content}";
                }
            }
            else
            {
                mLastDialogueAction = StageActionFactory.NewDialogueAction(mCurrentCharacterDevName, content);
                
                def.Actions.Add(mLastDialogueAction);
            }
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

        public override void ParseCue(string cueToken, string cueData)
        {
            if (mSettings.KnownInitGunks.Contains(cueToken))
            {
                int gunkAmount = int.Parse(cueData);
                    
                def.Actions.Add(StageActionFactory.NewGunkInit(cueToken, gunkAmount));
                return;
            }
            
            if (mSettings.KnownIndexedGunks.Contains(cueToken))
            {
                int[] data = cueData.AsIntArray();

                def.Actions.Add(StageActionFactory.NewGunkIndices(cueToken, data));
                return;
            }
            
            if (mSettings.KnownSpeakerNames.Contains(cueToken))
            {
                mCurrentCharacterDevName = cueToken;

                mLastDialogueAction = StageActionFactory.NewDialogueAction(mCurrentCharacterDevName, "");
                    
                def.Actions.Add(mLastDialogueAction);
                return;
            }

            switch (cueToken)
            {
                case "ACTION_NEW_PATIENT":
                    ZLog.Warn("Need to handle ACTION_NEW_PATIENT");
                    return;
                
                case "VO":
                    def.Actions.Add(StageActionFactory.NewVOAction(cueData));
                    return;
            }
            
            throw new InvalidOperationException($"[PHASE] Unknown cue token! token: {cueToken}, is this a speaker or surgery gunk?");
        }

        public override void ParseBreak()
        {
            // assuming we have some existing dialogue
            mLastDialogueAction = null;
        }
    }
}