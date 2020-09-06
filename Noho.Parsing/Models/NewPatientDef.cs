using System.Collections.Generic;

namespace Noho.Parsing.Models
{
    public class NewPatientDef
    {
        public int NewPatientId;
        public AppearType AppearType;
        public int AppearData;
        public List<StageAction> Actions = new List<StageAction>();
    }

    public enum AppearType
    {
        ACTIONS,
    }
}