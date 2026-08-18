using System;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000380 RID: 896
	internal sealed class StringSorter
	{
		// Token: 0x06003A85 RID: 14981 RVA: 0x00102280 File Offset: 0x00100480
		private StringSorter(CultureInfo culture, string[] keys, object[] items, int options)
		{
			if (keys == null)
			{
				if (items is string[])
				{
					keys = (string[])items;
					items = null;
				}
				else
				{
					keys = new string[items.Length];
					for (int i = 0; i < items.Length; i++)
					{
						object obj = items[i];
						if (obj != null)
						{
							keys[i] = obj.ToString();
						}
					}
				}
			}
			this.keys = keys;
			this.items = items;
			this.lcid = ((culture == null) ? SafeNativeMethods.GetThreadLocale() : culture.LCID);
			this.options = (options & 200711);
			this.descending = ((options & int.MinValue) != 0);
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x00102316 File Offset: 0x00100516
		internal static int ArrayLength(object[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x00102320 File Offset: 0x00100520
		public static int Compare(string s1, string s2)
		{
			return StringSorter.Compare(SafeNativeMethods.GetThreadLocale(), s1, s2, 0);
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x0010232F File Offset: 0x0010052F
		public static int Compare(string s1, string s2, int options)
		{
			return StringSorter.Compare(SafeNativeMethods.GetThreadLocale(), s1, s2, options);
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x0010233E File Offset: 0x0010053E
		public static int Compare(CultureInfo culture, string s1, string s2, int options)
		{
			return StringSorter.Compare(culture.LCID, s1, s2, options);
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x0010234E File Offset: 0x0010054E
		private static int Compare(int lcid, string s1, string s2, int options)
		{
			if (s1 == null)
			{
				if (s2 != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (s2 == null)
				{
					return 1;
				}
				return string.Compare(s1, s2, false, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x0010236C File Offset: 0x0010056C
		private int CompareKeys(string s1, string s2)
		{
			int num = StringSorter.Compare(this.lcid, s1, s2, this.options);
			if (!this.descending)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x0010239C File Offset: 0x0010059C
		private void QuickSort(int left, int right)
		{
			do
			{
				int num = left;
				int num2 = right;
				string text = this.keys[num + num2 >> 1];
				for (;;)
				{
					if (this.CompareKeys(this.keys[num], text) >= 0)
					{
						while (this.CompareKeys(text, this.keys[num2]) < 0)
						{
							num2--;
						}
						if (num > num2)
						{
							break;
						}
						if (num < num2)
						{
							string text2 = this.keys[num];
							this.keys[num] = this.keys[num2];
							this.keys[num2] = text2;
							if (this.items != null)
							{
								object obj = this.items[num];
								this.items[num] = this.items[num2];
								this.items[num2] = obj;
							}
						}
						num++;
						num2--;
						if (num > num2)
						{
							break;
						}
					}
					else
					{
						num++;
					}
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.QuickSort(left, num2);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.QuickSort(num, right);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x0010247E File Offset: 0x0010067E
		public static void Sort(object[] items)
		{
			StringSorter.Sort(null, null, items, 0, StringSorter.ArrayLength(items), 0);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x00102490 File Offset: 0x00100690
		public static void Sort(object[] items, int index, int count)
		{
			StringSorter.Sort(null, null, items, index, count, 0);
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x0010249D File Offset: 0x0010069D
		public static void Sort(string[] keys, object[] items)
		{
			StringSorter.Sort(null, keys, items, 0, StringSorter.ArrayLength(items), 0);
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x001024AF File Offset: 0x001006AF
		public static void Sort(string[] keys, object[] items, int index, int count)
		{
			StringSorter.Sort(null, keys, items, index, count, 0);
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x001024BC File Offset: 0x001006BC
		public static void Sort(object[] items, int options)
		{
			StringSorter.Sort(null, null, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x001024CE File Offset: 0x001006CE
		public static void Sort(object[] items, int index, int count, int options)
		{
			StringSorter.Sort(null, null, items, index, count, options);
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x001024DB File Offset: 0x001006DB
		public static void Sort(string[] keys, object[] items, int options)
		{
			StringSorter.Sort(null, keys, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x001024ED File Offset: 0x001006ED
		public static void Sort(string[] keys, object[] items, int index, int count, int options)
		{
			StringSorter.Sort(null, keys, items, index, count, options);
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x001024FB File Offset: 0x001006FB
		public static void Sort(CultureInfo culture, object[] items, int options)
		{
			StringSorter.Sort(culture, null, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x0010250D File Offset: 0x0010070D
		public static void Sort(CultureInfo culture, object[] items, int index, int count, int options)
		{
			StringSorter.Sort(culture, null, items, index, count, options);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x0010251B File Offset: 0x0010071B
		public static void Sort(CultureInfo culture, string[] keys, object[] items, int options)
		{
			StringSorter.Sort(culture, keys, items, 0, StringSorter.ArrayLength(items), options);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x00102530 File Offset: 0x00100730
		public static void Sort(CultureInfo culture, string[] keys, object[] items, int index, int count, int options)
		{
			if (items == null || (keys != null && keys.Length != items.Length))
			{
				throw new ArgumentException(SR.GetString("ArraysNotSameSize", new object[]
				{
					"keys",
					"items"
				}));
			}
			if (count > 1)
			{
				StringSorter stringSorter = new StringSorter(culture, keys, items, options);
				stringSorter.QuickSort(index, index + count - 1);
			}
		}

		// Token: 0x04002319 RID: 8985
		public const int IgnoreCase = 1;

		// Token: 0x0400231A RID: 8986
		public const int IgnoreKanaType = 65536;

		// Token: 0x0400231B RID: 8987
		public const int IgnoreNonSpace = 2;

		// Token: 0x0400231C RID: 8988
		public const int IgnoreSymbols = 4;

		// Token: 0x0400231D RID: 8989
		public const int IgnoreWidth = 131072;

		// Token: 0x0400231E RID: 8990
		public const int StringSort = 4096;

		// Token: 0x0400231F RID: 8991
		public const int Descending = -2147483648;

		// Token: 0x04002320 RID: 8992
		private const int CompareOptions = 200711;

		// Token: 0x04002321 RID: 8993
		private string[] keys;

		// Token: 0x04002322 RID: 8994
		private object[] items;

		// Token: 0x04002323 RID: 8995
		private int lcid;

		// Token: 0x04002324 RID: 8996
		private int options;

		// Token: 0x04002325 RID: 8997
		private bool descending;
	}
}
