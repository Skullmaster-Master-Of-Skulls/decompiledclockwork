using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005E6 RID: 1510
	public static class ArrayAdapters
	{
		// Token: 0x060030BB RID: 12475 RVA: 0x0004233C File Offset: 0x0004053C
		public static T GetNearestItemAtRight<T>(this T[] items, T curItem, bool includeCurrent)
		{
			int num = Array.BinarySearch<T>(items, curItem);
			bool flag = num < 0;
			if (flag)
			{
				num = ~num % items.Length;
			}
			else
			{
				bool flag2 = !includeCurrent;
				if (flag2)
				{
					num = (num + 1) % items.Length;
				}
			}
			return items[num];
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x00042380 File Offset: 0x00040580
		public static string CommaSeparatedValues<T>(this IList<T> values)
		{
			string result;
			if (values != null && values.Count != 0)
			{
				result = string.Join(", ", values.Select(delegate(T s)
				{
					T t = s;
					return t.ToString();
				}).ToArray<string>());
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000423D8 File Offset: 0x000405D8
		public static IList<string> SplitValues(this string commaSeparatedValues)
		{
			string[] array = (commaSeparatedValues != null) ? commaSeparatedValues.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries) : null;
			IList<string> result;
			if (array == null)
			{
				result = null;
			}
			else
			{
				result = (from value in array
				select value.Trim()).ToList<string>();
			}
			return result;
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x00042434 File Offset: 0x00040634
		public static IList<string> SplitValues(this string commaSeparatedValues, char separator)
		{
			string[] array = (commaSeparatedValues != null) ? commaSeparatedValues.Split(new char[]
			{
				separator
			}, StringSplitOptions.RemoveEmptyEntries) : null;
			IList<string> result;
			if (array == null)
			{
				result = null;
			}
			else
			{
				result = (from value in array
				select value.Trim()).ToList<string>();
			}
			return result;
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x00042490 File Offset: 0x00040690
		public static List<int> SplitIntValues(this string commaSeparatedValues)
		{
			List<int> list = new List<int>();
			string[] array = commaSeparatedValues.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				int item;
				bool flag = int.TryParse(text.Trim(), out item);
				if (flag)
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000424F4 File Offset: 0x000406F4
		public static string CommaSeparatedValuesWithoutSpace<T>(this IList<T> values)
		{
			string result;
			if (values != null && values.Count != 0)
			{
				result = string.Join(",", values.Select(delegate(T s)
				{
					T t = s;
					return t.ToString();
				}).ToArray<string>());
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x0004254C File Offset: 0x0004074C
		public static T[] GetRangeByIndices<T>(this T[] array, int startIndex, int endInd)
		{
			return (array == null) ? new T[0] : array.GetRange(startIndex, endInd - startIndex + 1);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x00042578 File Offset: 0x00040778
		public static T[] GetRange<T>(this T[] array, int startIndex, int count)
		{
			bool flag = array == null || count < 1;
			T[] result;
			if (flag)
			{
				result = new T[0];
			}
			else
			{
				T[] array2 = new T[count];
				int num = startIndex + count - 1;
				for (int i = startIndex; i <= num; i++)
				{
					array2[i - startIndex] = array[i];
				}
				result = array2;
			}
			return result;
		}
	}
}
