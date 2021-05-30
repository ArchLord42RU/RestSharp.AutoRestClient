using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AutoRest.Client.Tests.Asserts
{
    public static class CustomAsserts
    {
        public static void DictionaryContainsKeyAndValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            var kv = dictionary.FindByKey(key);
            Assert.AreEqual(key, kv.Key);
            Assert.AreEqual(value, kv.Value);
        }

        public static void DictionaryContainsKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key)
        {
            Assert.False(dictionary.FindByKey(key).Equals(default(KeyValuePair<TKey, TValue>)), "Dictionary does not contains key");
        }
        
        public static void DictionaryDoesNotContainsKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key)
        {
            Assert.True(dictionary.FindByKey(key).Equals(default(KeyValuePair<TKey, TValue>)), "Dictionary contains key");
        }

        private static KeyValuePair<TKey, TValue> FindByKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.FirstOrDefault(x => Equals(x.Key, key));
        }
    }
}