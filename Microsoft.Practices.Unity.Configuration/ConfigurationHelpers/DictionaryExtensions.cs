using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000017 RID: 23
	public static class DictionaryExtensions
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00004454 File Offset: 0x00002654
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static TValue GetOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			Guard.ArgumentNotNull(dictionary, "dictionary");
			TValue result;
			if (dictionary.TryGetValue(key, out result))
			{
				return result;
			}
			return default(TValue);
		}
	}
}
