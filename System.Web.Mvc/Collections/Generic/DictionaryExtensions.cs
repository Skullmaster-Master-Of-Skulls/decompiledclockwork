using System;
using System.ComponentModel;

namespace System.Collections.Generic
{
	// Token: 0x02000003 RID: 3
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class DictionaryExtensions
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000225F File Offset: 0x0000045F
		public static void RemoveFromDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> removeCondition)
		{
			dictionary.RemoveFromDictionary((KeyValuePair<TKey, TValue> entry, Func<KeyValuePair<TKey, TValue>, bool> innerCondition) => innerCondition(entry), removeCondition);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002274 File Offset: 0x00000474
		public static void RemoveFromDictionary<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, TState, bool> removeCondition, TState state)
		{
			int num = 0;
			TKey[] array = new TKey[dictionary.Count];
			foreach (KeyValuePair<TKey, TValue> arg in dictionary)
			{
				if (removeCondition(arg, state))
				{
					array[num] = arg.Key;
					num++;
				}
			}
			for (int i = 0; i < num; i++)
			{
				dictionary.Remove(array[i]);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002300 File Offset: 0x00000500
		public static bool TryGetValue<T>(this IDictionary<string, object> collection, string key, out T value)
		{
			object obj;
			if (collection.TryGetValue(key, out obj) && obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025D0 File Offset: 0x000007D0
		internal static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix)
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
					if (prefix.Length == 0)
					{
						yield return entry;
					}
					else
					{
						char charAfterPrefix = key[prefix.Length];
						char c = charAfterPrefix;
						if (c == '.' || c == '[')
						{
							yield return entry;
						}
					}
				}
			}
			yield break;
		}
	}
}
