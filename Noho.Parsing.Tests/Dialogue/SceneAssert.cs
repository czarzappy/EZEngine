using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;
using ZEngine.UnitTesting;

namespace Noho.Parsing.Tests.Dialogue
{
    public static class SceneAssert
    {
        public static Action<CharacterMove> MoveAssertFactory(string characterDevName, int newStagePos)
        {
            return move =>
            {
                Assert.AreEqual(characterDevName, move.CharacterDevName);
                Assert.AreEqual(newStagePos, move.NewStagePos);
            };
        }

        // public delegate void IsValidStageAction(StageAction action);
        // public static void IsValidStageActions(List<StageAction> actions, params IsValidItem<StageAction>[] actionAssertions)
        // {
        //     Assert.IsNotNull(actions, "Expected valid stage actions");
        //     Assert.AreEqual(actionAssertions.Length, actions.Count);
        //
        //     for (int i = 0; i < actions.Count; i++)
        //     {
        //         actionAssertions[i](actions[i]);
        //     }
        // }

        public static void IsValidDialogue(StageAction action, string speakerDevName, string dialogueText)
        {
            Assert.AreEqual(StageActionType.DIALOGUE, action.Type);
            Assert.AreEqual(speakerDevName, action.CharacterDevName);
            Assert.AreEqual(dialogueText, action.DialogueText);
        }

        public static void IsValidExpression(StageAction action, string characterDevName, string expressionType)
        {
            Assert.AreEqual(StageActionType.EXPRESSION, action.Type);
            Assert.AreEqual(characterDevName, action.CharacterDevName);
            Assert.AreEqual(expressionType, action.ExpressionType, $"Expected that {action.CharacterDevName} had the given expression");
        }

        public static void IsValidMove(StageAction action, params Action<CharacterMove>[] moveAssertions)
        {
            Assert.AreEqual(StageActionType.MOVE, action.Type);
            Assert.AreEqual(moveAssertions.Length, action.CharacterMoves.Count);
            for (int i = 0; i < action.CharacterMoves.Count; i++)
            {
                moveAssertions[i](action.CharacterMoves[i]);
            }
        }
        public static void IsValidGunkInit(StageAction action, string gunkType, int gunkAmount)
        {
            Assert.AreEqual(StageActionType.GUNK_INIT, action.Type);
            Assert.AreEqual(gunkType, action.GunkType);
            Assert.AreEqual(gunkAmount, action.GunkAmount);
        }

        public static void IsValidGunkIndices(StageAction action, string gunkType, int[] gunkIndices)
        {
            Assert.AreEqual(StageActionType.GUNK_INDICES, action.Type);
            Assert.AreEqual(gunkType, action.GunkType);
            ZAssert.AreEqual(gunkIndices, action.GunkIndices);
        }

        public static void IsValidBackground(StageAction action, string asset)
        {
            Assert.AreEqual(StageActionType.BACKGROUND, action.Type);
            Assert.AreEqual(asset, action.Asset);
        }

        public static void IsValidBackgroundMusic(StageAction action, string asset)
        {
            Assert.AreEqual(StageActionType.BACKGROUND_MUSIC, action.Type);
            Assert.AreEqual(asset, action.Asset);
        }

        public static void IsValidPhase(PhaseDef phaseDef, 
            string expectedSurgeryName, 
            bool assertSize,
            params ZAssert.IsValidItem<StageAction>[] actionAssertions)
        {
            Assert.AreEqual(expectedSurgeryName, phaseDef.SurgerySceneName);
            
            ZAssert.IsValidItems(phaseDef.Actions, assertSize, actionAssertions);
        }

        public static void IsValidPhase(PhaseDef phaseDef, 
            string expectedSurgeryName, 
            params ZAssert.IsValidItem<StageAction>[] actionAssertions)
        {
            IsValidPhase(phaseDef, expectedSurgeryName, true, actionAssertions);
        }

        public static void IsValidPatient(PatientDef patientDef, int expectedPatientId,
            string expectedPatientName,
            Vitals expectedVitals,
            bool assertSize,
            params ZAssert.IsValidItem<PhaseDef>[] phaseAsserts)
        {
            Assert.AreEqual(expectedPatientId, patientDef.Id);
            Assert.AreEqual(expectedPatientName, patientDef.Name);
            Assert.AreEqual(expectedVitals, patientDef.Vitals);
            
            ZAssert.IsValidItems(patientDef.PhaseDefs, assertSize, phaseAsserts);
        }

        public static void IsValidPatient(PatientDef patientDef, int expectedPatientId,
            string expectedPatientName,
            Vitals expectedVitals,
            params ZAssert.IsValidItem<PhaseDef>[] phaseAsserts)
        {
            IsValidPatient(patientDef, expectedPatientId, expectedPatientName, expectedVitals, true, phaseAsserts);
        }

        public static void IsValidCutScene(CutSceneDef cutSceneDef, 
            bool assertSize,
            params ZAssert.IsValidItem<StageAction>[] actionAsserts)
        {
            ZAssert.IsValidItems(cutSceneDef.Actions, assertSize, actionAsserts);
        }

        public static void IsValidCutScene(CutSceneDef cutSceneDef,
            params ZAssert.IsValidItem<StageAction>[] actionAsserts)
        {
            ZAssert.IsValidItems(cutSceneDef.Actions, true, actionAsserts);
        }
        

        public static void IsValidLoveMin(StageAction action, 
            string expectedCharacterDevName, 
            int expectedMinLove, 
            bool assertSize, 
            params ZAssert.IsValidItem<object>[] actionAsserts)
        {
            Assert.AreEqual(StageActionType.LOVE_MIN, action.Type);
            Assert.AreEqual(expectedCharacterDevName, action.CharacterDevName);
            Assert.AreEqual(expectedMinLove, action.LoveAmount);
            
            Assert.IsNotNull(action.TrueBranch, "Expected that love min action defined a true branch");
            
            ZAssert.IsValidItems(action.TrueBranch, assertSize, actionAsserts);
        }

        public static void IsValidLoveMin(StageAction action, 
            string expectedCharacterDevName, 
            int expectedMinLove,
            params ZAssert.IsValidItem<object>[] actionAsserts)
        {
            IsValidLoveMin(action, expectedCharacterDevName, expectedMinLove, true, actionAsserts);
        }

        public static void IsValidLoveDelta(StageAction action, string expectedCharacterDevName, int expectedLoveDelta)
        {
            Assert.AreEqual(StageActionType.LOVE_DELTA, action.Type);
            Assert.AreEqual(expectedCharacterDevName, action.CharacterDevName);
            Assert.AreEqual(expectedLoveDelta, action.LoveAmount);
            
        }

        public static void IsValidLoveFocus(StageAction action, string expectedCharacterDevName)
        {
            Assert.AreEqual(StageActionType.LOVE_FOCUS, action.Type);
            Assert.AreEqual(expectedCharacterDevName, action.CharacterDevName);
        }

        public static void IsValidCharacterStoryline(StageAction action, string expectedCharacterDevName)
        {
            Assert.AreEqual(StageActionType.NEW_CHARACTER_STORYLINE, action.Type);
            Assert.AreEqual(expectedCharacterDevName, action.CharacterDevName);
        }

        public static void IsValidSetFlag(StageAction action, string expectedFlagTag, bool expectedFlagValue)
        {
            Assert.AreEqual(StageActionType.SET_FLAG, action.Type);
            Assert.AreEqual(expectedFlagTag, action.FlagTag);
            Assert.AreEqual(expectedFlagValue, action.FlagValue);
        }

        public static void IsValidCheckFlag(StageAction action, string expectedFlagTag, bool expectedFlagValue)
        {
            Assert.AreEqual(StageActionType.CHECK_FLAG, action.Type);
            Assert.AreEqual(expectedFlagTag, action.FlagTag);
            Assert.AreEqual(expectedFlagValue, action.FlagValue);
        }
    }
}