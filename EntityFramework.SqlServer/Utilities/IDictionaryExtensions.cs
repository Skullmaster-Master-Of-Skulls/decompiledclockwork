using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200002F RID: 47
	internal static class IDictionaryExtensions
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000B8C8 File Offset: 0x00009AC8
		internal static void Add<TKey, TValue>(this IDictionary<TKey, IList<TValue>> map, TKey key, TValue value)
		{
			IList<TValue> list;
			if (!map.TryGetValue(key, out list))
			{
				list = new List<TValue>();
				map[key] = list;
			}
			list.Add(value);
		}
	}
}
