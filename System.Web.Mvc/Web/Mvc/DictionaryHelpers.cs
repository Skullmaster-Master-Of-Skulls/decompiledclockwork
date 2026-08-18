using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000181 RID: 385
	internal static class DictionaryHelpers
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x0001CDF8 File Offset: 0x0001AFF8
		public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			TValue exactMatchValue;
			if (dictionary.TryGetValue(prefix, out exactMatchValue))
			{
				yield return new KeyValuePair<string, TValue>(prefix, exactMatchValue);
			}
			foreach (KeyValuePair<string, TValue> entry in dictionary)
			{
				KeyValuePair<string, TValue> keyValuePair = entry;
				string key = keyValuePair.Key;
				if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					char charAfterPrefix = key[prefix.Length];
					char c = charAfterPrefix;
					if (c == '.' || c == '[')
					{
						yield return entry;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001CE1C File Offset: 0x0001B01C
		public static bool DoesAnyKeyHavePrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			return DictionaryHelpers.FindKeysWithPrefix<TValue>(dictionary, prefix).Any<KeyValuePair<string, TValue>>();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001CE2C File Offset: 0x0001B02C
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
		{
			TValue result;
			if (dict.TryGetValue(key, out result))
			{
				return result;
			}
			return @default;
		}
	}
}
