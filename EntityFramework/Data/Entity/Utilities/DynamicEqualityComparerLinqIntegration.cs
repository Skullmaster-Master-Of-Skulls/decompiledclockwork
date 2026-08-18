using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000723 RID: 1827
	internal static class DynamicEqualityComparerLinqIntegration
	{
		// Token: 0x06004B1E RID: 19230 RVA: 0x0016105E File Offset: 0x0015F25E
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> func) where T : class
		{
			return source.Distinct(new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x0016106F File Offset: 0x0015F26F
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static IEnumerable<IGrouping<TSource, TSource>> GroupBy<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> func) where TSource : class
		{
			return source.GroupBy((TSource t) => t, new DynamicEqualityComparer<TSource>(func));
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x00161089 File Offset: 0x0015F289
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> func) where T : class
		{
			return first.Intersect(second, new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x00161098 File Offset: 0x0015F298
		public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> func) where T : class
		{
			return first.Except(second, new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x001610A7 File Offset: 0x0015F2A7
		public static bool Contains<T>(this IEnumerable<T> source, T value, Func<T, T, bool> func) where T : class
		{
			return source.Contains(value, new DynamicEqualityComparer<T>(func));
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x001610B6 File Offset: 0x0015F2B6
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TSource, bool> func) where TSource : class
		{
			return source.SequenceEqual(other, new DynamicEqualityComparer<TSource>(func));
		}
	}
}
