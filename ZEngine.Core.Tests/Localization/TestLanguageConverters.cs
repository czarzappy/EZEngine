using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZEngine.Core.Localization;

namespace ZEngine.Core.Tests.Localization
{
    [TestClass]
    public class TestLanguageConverters
    {
        [TestMethod]
        public void TestToAsterisks()
        {
            string result = "hello, \nworld".ToAsterisks();
            
            Assert.AreEqual("****** \n*****", result);
        }
    }
}