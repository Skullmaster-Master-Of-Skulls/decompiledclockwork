using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x0200064C RID: 1612
	internal static class DictionaryHelpers
	{
		// Token: 0x06004F91 RID: 20369 RVA: 0x00114705 File Offset: 0x00112905
		public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			TValue value;
			if (dictionary.TryGetValue(prefix, out value))
			{
				yield return new KeyValuePair<string, TValue>(prefix, value);
			}
			foreach (KeyValuePair<string, TValue> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					char c = key[prefix.Length];
					if (c == '.' || c == '[')
					{
						yield return keyValuePair;
					}
				}
			}
			IEnumerator<KeyValuePair<string, TValue>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x0011471C File Offset: 0x0011291C
		public static bool DoesAnyKeyHavePrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
		{
			return DictionaryHelpers.FindKeysWithPrefix<TValue>(dictionary, prefix).Any<KeyValuePair<string, TValue>>();
		}
	}
}
