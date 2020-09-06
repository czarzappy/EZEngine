using System.Collections.Generic;
using System.IO;
using ZEngine.Core.Dialogue.Models;

namespace Noho.Parsing.Models
{
    public enum StageActionType
    {
        DIALOGUE,
        MOVE,
        EXPRESSION,
        CHOICE,
        GUNK_INIT,
        GUNK_INDICES,
        VITAL,
        BACKGROUND,
        BACKGROUND_MUSIC,
        LOVE_MIN,
        LOVE_DELTA,
        LOVE_FOCUS,
        VO,
        NEW_CHARACTER_STORYLINE,
        BREAK,
        SET_FLAG,
        CHECK_FLAG
    }
    
    public class StageAction
    {
        public StageActionType Type;

        /// <summary>
        /// Usage: LOVE_MIN, LOVE_DELTA
        /// </summary>
        public string CharacterDevName;
        public string ExpressionType;
        public string DialogueText;

        public List<StageBranchDef> Choices;

        public List<CharacterMove> CharacterMoves;
        
        public int GunkAmount;
        
        /// <summary>
        /// 1-based
        /// </summary>
        public int[] GunkIndices;
        public string GunkType;
        
        public string VitalActionType;
        public int VitalAmount;
        public string Asset;
        
        /// <summary>
        /// Usage: LOVE_MIN, LOVE_DELTA
        /// </summary>
        public int LoveAmount;

        /// <summary>
        /// Usage: LOVE_MIN
        /// </summary>
        public List<object> TrueBranch;

        public int NumberOfChoicesToSelect;
        public string FlagTag;
        public bool FlagValue;

        public override string ToString()
        {
            var sw = new StringWriter();
            
            sw.Write($"[{Type}] ");
            
            switch (Type)
            {
                case StageActionType.MOVE:
                    sw.Write(string.Join(", ", CharacterMoves));
                    break;
                
                case StageActionType.BACKGROUND:
                case StageActionType.BACKGROUND_MUSIC:
                    sw.Write(Asset);
                    break;
                
                case StageActionType.DIALOGUE:
                    sw.Write($"Speaker: {CharacterDevName}");
                    break;
            }

            return sw.ToString();
        }
    }
}