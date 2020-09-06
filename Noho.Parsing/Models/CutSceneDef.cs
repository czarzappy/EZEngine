using System.Collections.Generic;
using ZEngine.Core.Dialogue.Models;

namespace Noho.Parsing.Models
{
    public class CutSceneDef
    {
        public string SceneTitle;
        public string SceneDescription;

        public readonly List<StageAction> Actions = new List<StageAction>();

        public override string ToString()
        {
            return $"{SceneTitle}, Desc: {SceneDescription}";
        }
    }
}