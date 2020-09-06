using System;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class OperationDefParser : DefParser<OperationDef>
    {
        public override string Keyword => "OPERATION";

        public override IDefParser Clone()
        {
            return new OperationDefParser();
        }

        public override void ParseOptions(string[] options)
        {
            throw new System.NotImplementedException();
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            switch (cueToken)
            {
                case "BG":
                    def.BackgroundPath = cueData;
                    break;
                
                case "BGM":
                    def.BackgroundMusicPath = cueData;
                    break;
                
                case "TOOL_TIPS":
                    def.ToolTipSpeakerDevName = cueData;
                    break;
                
                case "INITIAL_PATIENTS":
                    def.InitialPatientIds = cueData.AsIntArray();;
                    break;
                
                case "TITLE":
                    def.Title = cueData;
                    break;
                
                case "SUBTITLE":
                    def.Subtitle = cueData;
                    break;
                
                default:
                    throw new InvalidOperationException($"[OPERATION] Unhandled cue token: {cueToken}");
                    break;
            }
        }

        public override void ParseMisc(string content)
        {
            throw new System.NotImplementedException($"[OPERATION] Tried to parse misc content: {content}");
        }

        public override void ParseChildToken(object obj)
        {
            switch (obj)
            {
                case PatientDef patientDef:
                    
                    def.Patients.Add(patientDef);
                    break;
            }
        }
    }
}