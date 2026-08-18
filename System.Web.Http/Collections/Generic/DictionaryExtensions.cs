using System;
using System.ComponentModel;

namespace System.Collections.Generic
{
	// Token: 0x02000003 RID: 3
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class DictionaryExtensions
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002410 File Offset: 0x00000610
		public static void RemoveFromDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> removeCondition)
		{
			dictionary.RemoveFromDictionary((KeyValuePair<TKey, TValue> entry, Func<KeyValuePair<TKey, TValue>, bool> innerCondition) => innerCondition(entry), removeCondition);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002428 File Offset: 0x00000628
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

		// Token: 0x06000010 RID: 16 RVA: 0x000024B4 File Offset: 0x000006B4
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

		// Token: 0x06000011 RID: 17 RVA: 0x00002784 File Offset: 0x00000984
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
