using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Parsers
{
    public class PatientDefParser : DefParser<PatientDef>
    {
        public override string Keyword => "PATIENT";

        public override IDefParser Clone()
        {
            return new PatientDefParser();
        }
        public override void ParseOptions(string[] options)
        {
            def.Id = int.Parse(options[0]);
        }

        public override void ParseCue(string cueToken, string cueData)
        {
            // if (cueToken == PhaseParser.Keyword)
            // {
            //     def.PhaseDefs = 
            // }
            
            switch (cueToken)
            {
                case "NAME":
                    def.Name = cueData;
                    break;
                
                case "VITALS":

                    int[] data = cueData.AsIntArray();
                    
                    def.Vitals = new Vitals
                    {
                        Max = data[0],
                        Starting = data[1],
                    };
                    break;
                
                
                
                
                // case "WOUND_GLASS":
                //     def.Vitals = cueData[0];
                //     break;
            }
        }

        public override void ParseChildToken(object obj)
        {
            switch (obj)
            {
                case PhaseDef phase:
                    
                    def.PhaseDefs.Add(phase);
                    break;
                
            }
        }

        public override void ParseMisc(string content)
        {
            // throw new System.NotImplementedException();
        }
    }
}