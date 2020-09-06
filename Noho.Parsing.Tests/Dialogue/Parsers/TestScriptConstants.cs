using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZEngine.Core.Dialogue.Models;

namespace Noho.Parsing.Tests.Dialogue.Parsers
{
    [TestClass]
    public class TestScriptConstants
    {
        // private IScriptParser mParser;


        // [TestInitialize]
        // public void OnTestInit()
        // {
        //     mParser = new V1ScriptParser();
        //     mParser.Register(new PatientDefParser());
        //     mParser.Register(new PhaseDefParser());
        // }

        // [TestMethod]
        // public void TestGetInfo()
        // {
        //     PatientDef patientDef = new PatientDef();
        //     // mParser.GetInfo(patientDef);
        // }
        
        [TestMethod]
        public void TestParseCueTokenWithOptions()
        {
            string cueLine = "PHASE: chest_shirt_01";

            string cueToken = ScriptConstants.ParseCueToken(cueLine, out string postCueLine);
            
            Assert.AreEqual("PHASE", cueToken);
            Assert.AreEqual("chest_shirt_01", postCueLine);
        }
        
        [TestMethod]
        public void TestParseCueTokenOtherOptions()
        {
            string cueLine = "WOUND_GLASS, 5";

            string cueToken = ScriptConstants.ParseCueToken(cueLine, out string postCueLine);
            
            Assert.AreEqual("WOUND_GLASS", cueToken);
            Assert.AreEqual("5", postCueLine);
        }
        
        [TestMethod]
        public void TestParseCueToken()
        {
            string cueLine = "PHASE";

            string cueToken = ScriptConstants.ParseCueToken(cueLine, out string postCueLine);
            
            Assert.AreEqual("PHASE", cueToken);
            Assert.AreEqual(null, postCueLine);
        }
    }
}