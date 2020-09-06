using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Noho.Parsing.Factories;
using Noho.Parsing.Models;
using Noho.Parsing.Parsers;
using ZEngine.Core.Dialogue.Models;
using ZEngine.Core.Dialogue.Parsers;

namespace Noho.Parsing.Tests.Dialogue.Parsers
{
    [TestClass]
    public class TestPatientParser
    {
        // private PatientDefParser mDefParser;
        // private PhaseDefParser mPhaseDefParser;
        private IScriptParser mScriptParser;
        // private IParseProcess parseProcess;

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
                    "WOUND_CUT_SMALL",
                    "WOUND_CUT_LARGE",
                    "WOUND_GLASS",
                    "ULCER_SMALL",
                    "ULCER_LARGE",
                    "HEMORRHAGE",
                    "WOUND_STEEL_SMALL",
                    
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

            var parseSettings = new ParserSettings()
            {
                KnownSpeakerNames = speakers
            };
            parser.Register(new OperationDefParser());
            parser.Register(new PatientDefParser());
            parser.Register(new CutSceneDefParser(parseSettings));
            parser.Register(new BriefingDefParser());
            parser.Register(new ActionChoiceDefParser());
            parser.Register(new ChoiceDefParser());
            parser.Register(new PreOpDefParser());
            parser.Register(new NewPatientDefParser(parseSettings));

            mScriptParser = parser;
        }
        
        [TestMethod]
        public void TestParsePhaseTest1()
        {
            // var lines = LineUtil.GetLinesFromFile("PatientTest.txt").AsParserLines();
            
            var lines = File.ReadLines("PatientTest.txt").AsParserLines();
            
            PatientDef patientDef = mScriptParser.Deserialize<PatientDef>(lines).First();
            // PatientDef patientDef = mParseProcess.Deserialize<PatientDef>(lines).First();
            
            SceneAssert.IsValidPatient(patientDef,
                1,
                "Hank H.",
                new Vitals
            {
                Max = 60,
                Starting = 40,
            }, (phase) =>
                {
                    SceneAssert.IsValidPhase(phase, "chest_shirt_01", 
                        action =>
                    {
                        SceneAssert.IsValidGunkInit(action, "WOUND_GLASS", 5);
                    });
                },
                (phase) =>
                {
                    SceneAssert.IsValidPhase(phase, null,
                        action =>
                        {
                            SceneAssert.IsValidDialogue(action, "PATIENT", "My… chest…");
                        },
                        action =>
                        {
                            SceneAssert.IsValidDialogue(action, "DANTE", "Quickly cut off the shirt.\n" +
                                                                         "Let’s see what we’re dealing with here.");
                        },
                        action =>
                        {
                            SceneAssert.IsValidGunkIndices(action, "SCALPEL", new []{1});
                        });
                },
                (phase) =>
                {
                    SceneAssert.IsValidPhase(phase, "chest_01",
                        action =>
                        {
                            SceneAssert.IsValidGunkInit(action, "WOUND_STEEL_SMALL", 2);
                        },
                        action =>
                        {
                            SceneAssert.IsValidGunkInit(action, "WOUND_CUT_LARGE", 2);
                        },
                        action =>
                        {
                            SceneAssert.IsValidGunkInit(action, "WOUND_CUT_SMALL", 3);
                        });
                });
            // Assert.AreEqual(1, patientDef.Id);
            // Assert.AreEqual("Hank H.", patientDef.Name);
            // Assert.AreEqual(new Vitals
            // {
            //     Max = 60,
            //     Starting = 40,
            // }, patientDef.Vitals);
            //
            // ZAssert.IsValidItems(patientDef.PhaseDefs, (phase) =>
            //     {
            //         SceneAssert.IsValidPhase(phase, "chest_shirt_01", 
            //             action =>
            //         {
            //             SceneAssert.IsValidGunkInit(action, "WOUND_GLASS", 5);
            //         });
            //     },
            //     (phase) =>
            //     {
            //         SceneAssert.IsValidPhase(phase, null,
            //             action =>
            //             {
            //                 SceneAssert.IsValidDialogue(action, "PATIENT", "My… chest…");
            //             },
            //             action =>
            //             {
            //                 SceneAssert.IsValidDialogue(action, "DANTE", "Quickly cut off the shirt.\n" +
            //                                                              "Let’s see what we’re dealing with here.");
            //             },
            //             action =>
            //             {
            //                 SceneAssert.IsValidGunkIndices(action, "SCALPEL", new []{1});
            //             });
            //     },
            //     (phase) =>
            //     {
            //         SceneAssert.IsValidPhase(phase, "chest_01",
            //             action =>
            //             {
            //                 SceneAssert.IsValidGunkInit(action, "WOUND_STEEL_SMALL", 2);
            //             },
            //             action =>
            //             {
            //                 SceneAssert.IsValidGunkInit(action, "WOUND_CUT_LARGE", 2);
            //             },
            //             action =>
            //             {
            //                 SceneAssert.IsValidGunkInit(action, "WOUND_CUT_SMALL", 3);
            //             });
            //     });
        }
    }
}