using System;
using System.Collections.Generic;

namespace AutoMapper.Internal
{
	// Token: 0x0200009C RID: 156
	public static class EnumerableExtensions
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00012B24 File Offset: 0x00010D24
		public static void Each<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (T obj in items)
			{
				action(obj);
			}
		}
	}
}
