using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZEngine.Core.Utils;

namespace ZEngine.Core.Tests.Utils
{
    [TestClass]
    public class TestStringUtil
    {
        [TestMethod]
        public void TestSubstringBefore()
        {
            string testStr = "TEST:";

            int idx = testStr.IndexOf(":", StringComparison.Ordinal);
            
            string result = "TEST:".SubstringBefore(idx);
            
            Assert.AreEqual("TEST", result);
        }
    }
}