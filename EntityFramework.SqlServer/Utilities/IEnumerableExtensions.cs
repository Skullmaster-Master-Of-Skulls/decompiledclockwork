using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200000B RID: 11
	[DebuggerStepThrough]
	internal static class IEnumerableExtensions
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003958 File Offset: 0x00001B58
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

		// Token: 0x06000074 RID: 116 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static void Each<T>(this IEnumerable<T> ts, Action<T, int> action)
		{
			int num = 0;
			foreach (T arg in ts)
			{
				action(arg, num++);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000039F8 File Offset: 0x00001BF8
		public static void Each<T>(this IEnumerable<T> ts, Action<T> action)
		{
			foreach (T obj in ts)
			{
				action(obj);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003A40 File Offset: 0x00001C40
		public static void Each<T, S>(this IEnumerable<T> ts, Func<T, S> action)
		{
			foreach (T arg in ts)
			{
				action(arg);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003AAC File Offset: 0x00001CAC
		public static string Join<T>(this IEnumerable<T> ts, Func<T, string> selector = null, string separator = ", ")
		{
			selector = (selector ?? ((T t) => t.ToString()));
			return string.Join(separator, (from t in ts
			where !object.ReferenceEquals(t, null)
			select t).Select(selector));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C98 File Offset: 0x00001E98
		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			yield return value;
			foreach (TSource element in source)
			{
				yield return element;
			}
			yield break;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E74 File Offset: 0x00002074
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
