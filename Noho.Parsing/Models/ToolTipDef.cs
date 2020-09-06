using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class ToolTipDef
    {
        public const string DEFAULT_SPEAKER_DEV_NAME = "DEFAULT";
        
        // Tag of a wound
        public string Tag;
        
        public List<ToolTipSpeakerDef> Speakers = new List<ToolTipSpeakerDef>();

        public ToolTipSpeakerDef GetSpeaker(string characterDevName)
        {
            ToolTipSpeakerDef defaultDef = null;
            
            foreach (ToolTipSpeakerDef speakerDef in Speakers)
            {
                if (speakerDef.SpeakerDevName == characterDevName)
                {
                    return speakerDef;
                }

                if (speakerDef.SpeakerDevName == DEFAULT_SPEAKER_DEV_NAME)
                {
                    defaultDef = speakerDef;
                }
            }

            return defaultDef;
        }
    }

    public class ToolTipSpeakerDef
    {
        public string SpeakerDevName;
        
        public List<ToolTipProcedureStepsDef> Steps = new List<ToolTipProcedureStepsDef>();
    }

    public class ToolTipProcedureStepsDef
    {
        public int StepId;
        public string Dialogue;
    }
}