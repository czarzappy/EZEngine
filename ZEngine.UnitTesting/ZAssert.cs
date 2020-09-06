using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZEngine.UnitTesting
{
    public static class ZAssert
    {
        public static void AreEqual<T>(T[] expected, T[] actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        
        public delegate void IsValidItem<T>(T item);
        
        public static void IsValidItems<T>(List<T> actions, bool checkSize, params IsValidItem<T>[] actionAssertions)
        {
            Assert.IsNotNull(actions, "Expected valid items");
            if (checkSize)
            {
                Assert.AreEqual(actionAssertions.Length, actions.Count);
            }
            

            for (int i = 0; i < actionAssertions.Length; i++)
            {
                actionAssertions[i](actions[i]);
            }
        }
        public static void IsValidItems<T>(List<T> actions, params IsValidItem<T>[] actionAssertions)
        {
            IsValidItems(actions, true, actionAssertions);
        }
        
        public static void IsValidItems<T>(T[] actions, bool assertSize, params IsValidItem<T>[] actionAssertions)
        {
            Assert.IsNotNull(actions, "Expected valid items");
            if (assertSize)
            {
                Assert.AreEqual(actionAssertions.Length, actions.Length);
            }

            for (int i = 0; i < actions.Length; i++)
            {
                actionAssertions[i](actions[i]);
            }
        }
        
        public static void IsValidItems<T>(T[] actions, params IsValidItem<T>[] actionAssertions)
        {
            IsValidItems(actions, true, actionAssertions);
        }
    }
}