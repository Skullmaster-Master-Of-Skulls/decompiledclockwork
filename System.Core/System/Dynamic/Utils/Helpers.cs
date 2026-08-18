using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x020000D7 RID: 215
	internal static class Helpers
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x00015AF4 File Offset: 0x00013CF4
		internal static T CommonNode<T>(T first, T second, Func<T, T> parent) where T : class
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (@default.Equals(first, second))
			{
				return first;
			}
			Set<T> set = new Set<T>(@default);
			for (T t = first; t != null; t = parent(t))
			{
				set.Add(t);
			}
			for (T t2 = second; t2 != null; t2 = parent(t2))
			{
				if (set.Contains(t2))
				{
					return t2;
				}
			}
			return default(T);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00015B60 File Offset: 0x00013D60
		internal static void IncrementCount<T>(T key, Dictionary<T, int> dict)
		{
			int num;
			dict.TryGetValue(key, out num);
			dict[key] = num + 1;
		}
	}
}
