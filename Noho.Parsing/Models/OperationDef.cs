using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class OperationDef
    {
        public string BackgroundPath;
        public string BackgroundMusicPath;
        public string ToolTipSpeakerDevName;
        public int[] InitialPatientIds;
        public string Title;
        public string Subtitle;
        public List<PatientDef> Patients = new List<PatientDef>();
    }
}