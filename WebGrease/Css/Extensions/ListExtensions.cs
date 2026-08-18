using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WebGrease.Css.Extensions
{
	// Token: 0x02000187 RID: 391
	public static class ListExtensions
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x00077FFF File Offset: 0x000761FF
		public static ReadOnlyCollection<T> AsSafeReadOnly<T>(this List<T> list)
		{
			if (list == null)
			{
				return null;
			}
			return list.AsReadOnly();
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0007800C File Offset: 0x0007620C
		public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
		{
			if (list == null || action == null)
			{
				return;
			}
			foreach (T obj in list)
			{
				action(obj);
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0007805C File Offset: 0x0007625C
		public static void ForEach<T>(this IEnumerable<T> list, Action<T, int> action)
		{
			if (list == null || action == null)
			{
				return;
			}
			int num = 0;
			foreach (T arg in list)
			{
				action(arg, num);
				num++;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000780B4 File Offset: 0x000762B4
		public static void ForEach<T>(this IList<T> list, Action<T, bool> action)
		{
			if (list == null || action == null)
			{
				return;
			}
			int num = 0;
			foreach (T arg in list)
			{
				action(arg, num >= list.Count - 1);
				num++;
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00078128 File Offset: 0x00076328
		public static ReadOnlyCollection<T> ToSafeReadOnlyCollection<T>(this IEnumerable<T> enumerable) where T : class
		{
			if (enumerable == null)
			{
				return null;
			}
			List<T> list = new List<T>(from _ in enumerable
			where _ != null
			select _);
			return list.AsReadOnly();
		}
	}
}
