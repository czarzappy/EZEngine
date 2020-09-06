using System;
using System.Collections.Generic;

namespace ZEngine.Core.Collections
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> valueFactory)
        {
            if (!dict.TryGetValue(key, out var result))
            {
                result = valueFactory();
                dict[key] = result;
            }

            return result;
        }
    }
}