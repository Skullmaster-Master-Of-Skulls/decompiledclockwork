using System;
using System.Collections.Generic;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200082D RID: 2093
	internal static class HashSetExtensions
	{
		// Token: 0x06005DC8 RID: 24008 RVA: 0x00195800 File Offset: 0x00193A00
		public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				set.Add(item);
			}
		}
	}
}
