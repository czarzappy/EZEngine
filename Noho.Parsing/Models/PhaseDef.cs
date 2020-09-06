using System.Collections.Generic;
using ZEngine.Core.Dialogue.Attributes;
using ZEngine.Core.Dialogue.Models;

namespace Noho.Parsing.Models
{
    public class PhaseDef
    {
        [Parameter(0)]
        public string SurgerySceneName;

        public readonly List<StageAction> Actions = new List<StageAction>();

        public override string ToString()
        {
            return $"Phase, actions: {Actions.Count}, scene: {SurgerySceneName}";
        }
    }
}