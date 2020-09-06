using System.Collections.Generic;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;

namespace Noho.Parsing.Factories
{
    public static class SceneFactory
    {
        public static CutSceneDef GenerateEmpty()
        {
            CutSceneDef cutSceneDef = new CutSceneDef();

            return cutSceneDef;
        }
    }

    public static class StageActionFactory
    {
        public static StageAction GenerateBreakAction()
        {
            return new StageAction
            {
                Type = StageActionType.BREAK,
            };
        }

        public static StageAction GenerateEmpty()
        {
            StageAction action = new StageAction();

            return action;
        }

        public static StageAction NewDialogueAction(string speakerDevName,
            string dialogueText)
        {
            return new StageAction
            {
                Type = StageActionType.DIALOGUE,
                CharacterDevName = speakerDevName,
                DialogueText = dialogueText
            };
        }

        public static StageAction NewStageMoveAction(List<CharacterMove> characterMoves)
        {
            return new StageAction
            {
                Type = StageActionType.MOVE,
                CharacterMoves = characterMoves
            };
        }

        public static StageAction NewExpressionAction(string characterName, string expression)
        {
            return new StageAction
            {
                Type = StageActionType.EXPRESSION,
                CharacterDevName = characterName,
                ExpressionType = expression
            };
        }

        public static StageAction NewGunkInit(string gunkType, int amount)
        {
            return new StageAction
            {
                Type = StageActionType.GUNK_INIT,
                GunkType = gunkType,
                GunkAmount = amount,
            };
        }

        public static StageAction NewGunkIndices(string gunkType, int[] indices)
        {
            return new StageAction
            {
                Type = StageActionType.GUNK_INDICES,
                GunkType = gunkType,
                GunkIndices = indices
            };
        }

        public static StageAction NewChoicesAction(ActionChoiceDef actionChoiceDef)
        {
            return new StageAction
            {
                Type = StageActionType.CHOICE,
                Choices = actionChoiceDef.Choices,
                NumberOfChoicesToSelect = actionChoiceDef.NumberOfChoicesToSelect
            };
        }

        public static StageAction NewLoveMinAction(LoveMinDef loveMinDef)
        {
            return new StageAction
            {
                Type = StageActionType.LOVE_MIN,
                LoveAmount = loveMinDef.MinAmount,
                CharacterDevName = loveMinDef.CharacterDevName,
                TrueBranch = loveMinDef.TrueBranch,
            };
        }

        public static StageAction NewVitalAction(string vitalActionType, int amount)
        {
            return new StageAction
            {
                Type = StageActionType.VITAL,
                VitalActionType = vitalActionType,
                VitalAmount = amount,
            };
        }

        public static StageAction NewBackground(string bgAsset)
        {
            return new StageAction
            {
                Type = StageActionType.BACKGROUND,
                Asset = bgAsset,
            };
        }

        public static StageAction NewBackgroundMusic(string bgMusicAsset)
        {
            return new StageAction
            {
                Type = StageActionType.BACKGROUND_MUSIC,
                Asset = bgMusicAsset,
            };
        }

        public static StageAction NewLoveDelta(string characterDevName, int loveDelta)
        {
            return new StageAction
            {
                Type = StageActionType.LOVE_DELTA,
                CharacterDevName = characterDevName,
                LoveAmount = loveDelta
            };
        }

        public static StageAction NewLoveFocus(string characterDevName)
        {
            return new StageAction
            {
                Type = StageActionType.LOVE_FOCUS,
                CharacterDevName = characterDevName,
            };
        }

        public static StageAction NewVOAction(string voAsset)
        {
            return new StageAction
            {
                Type = StageActionType.VO,
                Asset = voAsset,
            };
        }

        public static StageAction NewCharacterStoryline(string characterDevName)
        {
            return new StageAction
            {
                Type = StageActionType.NEW_CHARACTER_STORYLINE,
                CharacterDevName = characterDevName,
            };
        }

        public static StageAction NewSetFlag(string flagTag, bool flagValue)
        {
            return new StageAction
            {
                Type = StageActionType.SET_FLAG,
                FlagTag = flagTag,
                FlagValue = flagValue
            };
        }

        public static StageAction NewCheckFlagAction(CheckFlagDef checkFlagDef)
        {
            return new StageAction
            {
                Type = StageActionType.CHECK_FLAG,
                FlagTag = checkFlagDef.FlagTag,
                FlagValue = checkFlagDef.FlagExpectedValue,
                TrueBranch = checkFlagDef.TrueBranch
            };
        }
    }
}