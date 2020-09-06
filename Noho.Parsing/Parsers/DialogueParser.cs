using System;
using System.Collections.Generic;
using System.Linq;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using ZEngine.Core.Dialogue.Models;

namespace Noho.Parsing.Parsers
{
    public static class DialogueParser
    {
        private delegate StageAction SingleLineCueParser(string cueData);
        private delegate StageAction SingleLineCueParserNoParam();
        
        private static readonly Dictionary<string, SingleLineCueParser> SINGLE_LINE_REGISTRY = new Dictionary<string, SingleLineCueParser>();
        private static readonly Dictionary<string, SingleLineCueParserNoParam> SINGLE_LINE_REGISTRY_NO_PARAM = new Dictionary<string, SingleLineCueParserNoParam>();

        static DialogueParser()
        {
            SINGLE_LINE_REGISTRY_NO_PARAM.Add("BREAK", StageActionFactory.GenerateBreakAction);
            
            SINGLE_LINE_REGISTRY.Add("VO", StageActionFactory.NewVOAction);
            SINGLE_LINE_REGISTRY.Add("ACTION_VITALS", ParseActionVitals);
            
            SINGLE_LINE_REGISTRY.Add("BG", StageActionFactory.NewBackground);
            SINGLE_LINE_REGISTRY.Add("BGM", StageActionFactory.NewBackgroundMusic);
            
            SINGLE_LINE_REGISTRY.Add("LOVE", ParseLove);
            SINGLE_LINE_REGISTRY.Add("LOVEFOCUS", StageActionFactory.NewLoveFocus);
            SINGLE_LINE_REGISTRY.Add("LOVE_FOCUS", StageActionFactory.NewLoveFocus);
            
            SINGLE_LINE_REGISTRY.Add("NEW_CHARACTER_STORYLINE", StageActionFactory.NewCharacterStoryline);
            
            SINGLE_LINE_REGISTRY.Add("SETFLAG", ParseSetFlag);
            SINGLE_LINE_REGISTRY.Add("SET_FLAG", ParseSetFlag);
        }

        private static StageAction ParseSetFlag(string cueData)
        {
            string[] cues = cueData.Split(ParseTokens.CUE_SEPARATOR);
                    
            return StageActionFactory.NewSetFlag(cues[0].Trim(), bool.Parse(cues[1].Trim()));
        }

        public static IEnumerable<StageAction> ParseStageActions(int a, string lastCharacterDevName, string[] cues)
        {
            List<StageAction> results = new List<StageAction>();
            List<CharacterMove> charMoves = new List<CharacterMove>();

            // int charIdx;
            // int actionIdx ;
            for (int charIdx = -1, actionIdx = 0; charIdx < a; charIdx += 2, actionIdx += 2)
            {
                string characterName;
                if (charIdx == -1)
                {
                    characterName = lastCharacterDevName;
                }
                else
                {
                    characterName = cues[charIdx].Trim();
                }
        
                string cueMeta = cues[actionIdx].Trim();
        
                string expression;

                if (int.TryParse(cueMeta, out int newStagePos))
                {
                    // ZLog.Info($"[MOVE] {characterName} to pos: {newStagePos}");
                    charMoves.Add(new CharacterMove(characterName, newStagePos));
                    continue;
                    // throw new ArgumentException($"Failed to parse: {cueMeta} as a numeric stage position");
                }
                
                expression = cueMeta;
                StageAction expressionAction = StageActionFactory.NewExpressionAction(characterName, expression);

                results.Add(expressionAction);
                return results;
                // yield return expressionAction;
                // yield break;
            }
        
            if (charMoves.Count == 0)
            {
                // yield break;
                return results;
            }
            StageAction stageAction = StageActionFactory.NewStageMoveAction(charMoves);

            results.Add(stageAction);

            return results;
            // yield return stageAction;
        }

        private static IEnumerable<StageAction> ParseStageActions(string currentCharacterDevName, string[] cues)
        {
            List<StageAction> results = new List<StageAction>();
            var charMoves = new List<CharacterMove>();
            
            string characterName = currentCharacterDevName;

            foreach (string cue in cues)
            {
                if (!int.TryParse(cue, out int newStagePos))
                {
                    string expression = cue;
                    StageAction expressionAction = StageActionFactory.NewExpressionAction(characterName, expression);
                    results.Add(expressionAction);
                    // yield return expressionAction;
                }
                else
                {
                    charMoves.Add(new CharacterMove(characterName, newStagePos));
                    StageAction stageAction = StageActionFactory.NewStageMoveAction(charMoves);
                    // yield return stageAction;
                    results.Add(stageAction);
                }
            }

            return results;
        }

        public static IEnumerable<StageAction> ParseCue(string currentCharacterDevName, string cueData)
        {
            var results = new List<StageAction>();
            if (cueData == null)
            {
                return results;
            }
            
            string[] cues = cueData.Split(ParseTokens.CUE_SEPARATOR);

            try
            {
                switch (cues.Length)
                {
                    case 2:
                    {
                        return ParseStageActions(currentCharacterDevName, cues);
                    }

                    case int a when (a + 1) % 2 == 0:
                    {
                        return ParseStageActions(a, currentCharacterDevName, cues);
                    }
                
                    default:
                        throw new ArgumentException($"Could not interpret cues, {currentCharacterDevName}: {cues}");
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to parse cue and data: {currentCharacterDevName}, {cueData}", e);
            }
        }

        public static StageAction ParseLove(string cueData)
        {
            string[] cues = cueData.Split(ParseTokens.CUE_SEPARATOR);

            int loveDelta = 0;
            string characterDevName = null;
            switch (cues.Length)
            {
                case 1:
                    loveDelta = int.Parse(cues[0].Trim());
                    break;
                        
                case 2:
                    characterDevName = cues[0].Trim();
                    loveDelta = int.Parse(cues[1].Trim());
                    break;
            }

            var action = StageActionFactory.NewLoveDelta(characterDevName, loveDelta);

            return action;
        }

        public static StageAction ParseActionVitals(string cueData)
        {
            string[] cues = cueData.Split(ParseTokens.CUE_SEPARATOR);
            string vitalType = cues[0].Trim();
            int vitalAmount = int.Parse(cues[1].Trim());
                    
            var action = StageActionFactory.NewVitalAction(vitalType, vitalAmount);

            return action;
        }

        public static bool TryGetStageActionForCue(string cueToken, string cueData, out StageAction stageAction)
        {
            if (SINGLE_LINE_REGISTRY.TryGetValue(cueToken, out var entry))
            {
                stageAction = entry(cueData);
                return true;
            }

            if (SINGLE_LINE_REGISTRY_NO_PARAM.TryGetValue(cueToken, out var entryNoParam))
            {
                stageAction = entryNoParam();
                return true;
            }

            stageAction = null;
            return false;
        }

        public static IEnumerable<StageAction> ParseCueNoName(string cueToken, string cueData)
        {
            var results = new List<StageAction>();

            if (TryGetStageActionForCue(cueToken, cueData, out StageAction singleLineAction))
            {
                results.Add(singleLineAction);
                return results;
            }
            
            foreach (var action in ParseCue(cueToken, cueData))
            {
                results.Add(action);
            }

            return results;
        }

        public static bool TryParseDialogueObj(object obj, out StageAction stageAction)
        {
            switch (obj)
            {
                case ActionChoiceDef actionChoiceDef:

                    stageAction = StageActionFactory.NewChoicesAction(actionChoiceDef);
                    return true;
                
                case LoveMinDef loveMinDef:

                    stageAction = StageActionFactory.NewLoveMinAction(loveMinDef);
                    return true;
                
                case CheckFlagDef checkFlagDef:

                    stageAction = StageActionFactory.NewCheckFlagAction(checkFlagDef);
                    return true;
            }

            stageAction = null;
            return false;
        }
    }
}