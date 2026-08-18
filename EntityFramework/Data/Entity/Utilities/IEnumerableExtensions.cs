using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000007 RID: 7
	[DebuggerStepThrough]
	internal static class IEnumerableExtensions
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003220 File Offset: 0x00001420
		public static string Uniquify(this IEnumerable<string> inputStrings, string targetString)
		{
			string uniqueString = targetString;
			int num = 0;
			while (inputStrings.Any((string n) => string.Equals(n, uniqueString, StringComparison.Ordinal)))
			{
				uniqueString = targetString + ++num;
			}
			return uniqueString;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003270 File Offset: 0x00001470
		public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action)
		{
			int num = 0;
			foreach (T arg in ts)
			{
				action(arg, num++);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032C0 File Offset: 0x000014C0
		public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
		{
			foreach (T obj in ts)
			{
				action(obj);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003308 File Offset: 0x00001508
		public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action)
		{
			foreach (T arg in ts)
			{
				action(arg);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003374 File Offset: 0x00001574
		public static string Join<T>(this IEnumerable<T> ts, Func<T, string> selector = null, string separator = ", ")
		{
			selector = (selector ?? ((T t) => t.ToString()));
			return string.Join(separator, (from t in ts
			where !object.ReferenceEquals(t, null)
			select t).Select(selector));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003560 File Offset: 0x00001760
		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			yield return value;
			foreach (TSource element in source)
			{
				yield return element;
			}
			yield break;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000373C File Offset: 0x0000193C
		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			foreach (TSource element in source)
			{
				yield return element;
			}
			yield return value;
			yield break;
		}
	}
}
