using System;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class NewPatientDefParser : DefParser<NewPatientDef>
    {
        private readonly ParserSettings mSettings;


        public NewPatientDefParser(ParserSettings settings)
        {
            mSettings = settings;
        }
        
        public override IDefParser Clone()
        {
            return new NewPatientDefParser(mSettings);
        }

        public override string Keyword => "NEW_PATIENT";
        
        public override void ParseOptions(string[] options)
        {
            int newPatientId = int.Parse(options[0].Trim());
            def.NewPatientId = newPatientId;
            // throw new System.NotImplementedException($"[NEW_PATIENT] options: {string.Join(",", options)}");
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            switch (cueToken)
            {
                case "APPEAR_TYPE":
                    string[] cues = cueData.Split(ParseTokens.CUE_SEPARATOR);
                    
                    string typeStr = cues[0].Trim();

                    AppearType type = (AppearType) Enum.Parse(typeof(AppearType), typeStr);
                    def.AppearType = type;
                    def.AppearData = int.Parse(cues[1].Trim());
                    break;
                
                default:
                    
                    if (!mSettings.KnownSpeakerNames.Contains(cueToken))
                    {
                        throw new InvalidOperationException($"[CUTSCENE] Unknown cue token: {cueToken}");
                        break;
                    }

                    break;
                    // throw new System.NotImplementedException($"[NEW_PATIENT] cue: {cueToken}, {cueData}");
            }
        }

        public override void ParseMisc(string content)
        {
            var dialogue = StageActionFactory.NewDialogueAction(null, content);
            
            def.Actions.Add(dialogue);
        }

        public override void ParseChildToken(object obj)
        {
            throw new System.NotImplementedException($"[NEW_PATIENT] child token: {obj}");
        }
    }
}