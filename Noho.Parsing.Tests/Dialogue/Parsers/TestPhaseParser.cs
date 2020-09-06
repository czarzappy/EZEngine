using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using Noho.Parsing.Parsers;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;
using ZEngine.UnitTesting;

namespace Noho.Parsing.Tests.Dialogue.Parsers
{
    [TestClass]
    public class TestPhaseParser
    {
        private IScriptParser mScriptParser;
        
        [TestInitialize]
        public void OnTestInit()
        {
            var parser = new HierarchicalScriptParser();

            var speakers = new List<string>
            {
                "BIGTEX",
                "PROTAG",
                "PATIENT",
                "DANTE",
            };

            parser.Register(new PhaseDefParser(new PhaseParserSettings
            {
                KnownSpeakerNames = speakers,
                KnownInitGunks = new List<string>
                {
                    "WOUND_CUT_LARGE",
                    "WOUND_GLASS",
                    "ULCER_SMALL",
                    "ULCER_LARGE",
                    "HEMORRHAGE",
                },
                KnownIndexedGunks = new List<string>
                {
                    "STITCH_GEL_BANDAGE",
                    "SCALPEL",
                    "SPAWNER_HEMORRHAGE",
                    "BROKEN_RIBS",
                    "SUTURE",
                }
            }));
            
            var settings = new ParserSettings
            {
                KnownSpeakerNames = speakers
            };
            parser.Register(new OperationDefParser());
            parser.Register(new PatientDefParser());
            parser.Register(new CutSceneDefParser(settings));
            parser.Register(new BriefingDefParser());
            parser.Register(new ActionChoiceDefParser());
            parser.Register(new ChoiceDefParser());
            parser.Register(new PreOpDefParser());
            parser.Register(new NewPatientDefParser(settings));

            mScriptParser = parser;
        }
        
        [TestMethod]
        public void TestParsePhaseTest1()
        {
            var lines = File.ReadLines("PhaseTest1.txt").AsParserLines();
            
            var defs = mScriptParser.Deserialize<PhaseDef>(lines);

            PhaseDef firstDef = null; 
            foreach (PhaseDef def in defs)
            {
                firstDef = def;
            }
            // PhaseDef phaseDef = 
            //
            // SceneAssert.IsValidPhase(phaseDef,
            //     "chest_shirt_01",
            //     action =>
            // {
            //     SceneAssert.IsValidGunkInit(action, "WOUND_GLASS", 5);
            // });
        }
        
        [TestMethod]
        public void TestParsePhaseTest2()
        {
            var lines = File.ReadLines("PhaseTest2.txt").AsParserLines();
            
            var defs = mScriptParser.Deserialize<PhaseDef>(lines).ToArray();

            ZAssert.IsValidItems(defs, item =>
            {
                SceneAssert.IsValidPhase(item, null, action =>
                {
                    SceneAssert.IsValidDialogue(action, "PATIENT", "My… chest…");
                }, action =>
                {
                    SceneAssert.IsValidDialogue(action, "DANTE", "Quickly cut off the shirt.\nLet’s see what we’re dealing with here.");
                }, action =>
                {
                    SceneAssert.IsValidGunkIndices(action, "SCALPEL", new []{1});
                });
            });
            //
            // SceneAssert.IsValidPhase(phaseDef, 
            //     null,
            //     action =>
            //     {
            //         SceneAssert.IsValidDialogue(action, "PATIENT", "My… chest…");
            //     }, action =>
            //     {
            //         SceneAssert.IsValidDialogue(action, "DANTE", "Quickly cut off the shirt.\n" +
            //                                                      "Let’s see what we’re dealing with here.");
            //     }, action =>
            //     {
            //         SceneAssert.IsValidGunkIndices(action, "SCALPEL", new[]{1});
            //     });
        }
    }
}