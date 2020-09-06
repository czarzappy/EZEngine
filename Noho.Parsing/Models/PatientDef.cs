using System.Collections.Generic;
using ZEngine.Core.Dialogue.Attributes;

namespace Noho.Parsing.Models
{
    public class PatientDef
    {
        public int Id;
        
        [Keyword("NAME")]
        public string Name;
        
        [Keyword("VITALS")]
        public Vitals Vitals;
        
        public List<PhaseDef> PhaseDefs = new List<PhaseDef>();
    }

    public struct Vitals
    {
        public int Max;
        public int Starting;
    }
}