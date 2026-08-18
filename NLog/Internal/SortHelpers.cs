using System;
using System.Collections.Generic;

namespace NLog.Internal
{
	// Token: 0x020000AE RID: 174
	internal static class SortHelpers
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x0000C268 File Offset: 0x0000A468
		public static Dictionary<TKey, List<TValue>> BucketSort<TValue, TKey>(this IEnumerable<TValue> inputs, SortHelpers.KeySelector<TValue, TKey> keySelector)
		{
			Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>();
			foreach (TValue tvalue in inputs)
			{
				TKey key = keySelector(tvalue);
				List<TValue> list;
				if (!dictionary.TryGetValue(key, out list))
				{
					list = new List<TValue>();
					dictionary.Add(key, list);
				}
				list.Add(tvalue);
			}
			return dictionary;
		}

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x06000561 RID: 1377
		internal delegate TKey KeySelector<TValue, TKey>(TValue value);
	}
}
