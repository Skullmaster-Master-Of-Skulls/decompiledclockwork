using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Reflection.Internal
{
	// Token: 0x02000151 RID: 337
	internal static class EnumerableExtensions
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0001E773 File Offset: 0x0001C973
		public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, IComparer<T> comparer)
		{
			return source.OrderBy(EnumerableExtensions.Functions<T>.Identity, comparer);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001E781 File Offset: 0x0001C981
		public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, Comparison<T> compare)
		{
			return source.OrderBy(new EnumerableExtensions.ComparisonComparer<T>(compare));
		}

		// Token: 0x020001D8 RID: 472
		private class ComparisonComparer<T> : Comparer<T>
		{
			// Token: 0x06000C59 RID: 3161 RVA: 0x00022711 File Offset: 0x00020911
			public ComparisonComparer(Comparison<T> compare)
			{
				this._compare = compare;
			}

			// Token: 0x06000C5A RID: 3162 RVA: 0x00022720 File Offset: 0x00020920
			public override int Compare(T x, T y)
			{
				return this._compare(x, y);
			}

			// Token: 0x04000B4D RID: 2893
			private readonly Comparison<T> _compare;
		}

		// Token: 0x020001D9 RID: 473
		private static class Functions<T>
		{
			// Token: 0x04000B4E RID: 2894
			public static readonly Func<T, T> Identity = (T t) => t;
		}
	}
}
