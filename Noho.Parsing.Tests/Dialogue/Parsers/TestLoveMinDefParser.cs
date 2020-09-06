using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Noho.Parsing.Models;
using Noho.Parsing.Parsers;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Tests.Dialogue.Parsers
{
    [TestClass]
    public class TestLoveMinDefParser
    {
        private IScriptParser mParser;

        [TestInitialize]
        public void OnTestInit()
        {
            mParser = new HierarchicalScriptParser();
            
            mParser.Register(new LoveMinDefParser());
            mParser.Register(new CutSceneDefParser(new ParserSettings()
            {
                KnownSpeakerNames = new List<string>
                {
                    "DANTE",
                    "PROTAG"
                }
            }));
        }
        
        [TestMethod]
        public void TestParse()
        {
            var lines = ZParse.File("sample-conditional-cutscene.txt");

            var cutscenes = mParser.Deserialize<CutSceneDef>(lines).ToArray();

            SceneAssert.IsValidCutScene(cutscenes[0],
                item =>
                {
                    SceneAssert.IsValidLoveFocus(item, "DANTE");
                },
                item =>
                {
                    SceneAssert.IsValidDialogue(item, "DANTE", "What's up?");
                }, 
                item =>
                {
                    SceneAssert.IsValidLoveDelta(item, "DANTE", 1);
                }, 
                item =>
                {
                    SceneAssert.IsValidLoveDelta(item, null, 1);
                }, 
                item => { 
                    SceneAssert.IsValidLoveMin(item, "DANTE", 2, 
                        item =>
                        {
                            SceneAssert.IsValidDialogue((StageAction) item, null, "Cutie? :D");
                        },
                        item =>
                        {
                            SceneAssert.IsValidExpression((StageAction) item, "DANTE", "blushing");
                        }, 
                        item =>
                        {
                            SceneAssert.IsValidDialogue((StageAction) item, null, "I mean...");
                        },
                        item =>
                        {
                            SceneAssert.IsValidMove((StageAction) item,
                                SceneAssert.MoveAssertFactory("DANTE", 5));
                        }, 
                        item =>
                        {
                            SceneAssert.IsValidDialogue((StageAction) item, null, "Not like I meant it");
                        }, 
                        item =>
                        {
                            SceneAssert.IsValidLoveDelta((StageAction) item, "DANTE", -1);
                        }, 
                        item =>
                        {
                            SceneAssert.IsValidLoveDelta((StageAction) item, null, -1);
                        }
                    );
                },
                item => { 
                    SceneAssert.IsValidLoveMin(item, null, 1, 
                        item =>
                        {
                            SceneAssert.IsValidDialogue((StageAction) item, null, "Oh");
                        }
                    );
                },
                item =>
                {
                    SceneAssert.IsValidDialogue(item, "PROTAG", "Nothing much");
                }
            );
        }
    }
}