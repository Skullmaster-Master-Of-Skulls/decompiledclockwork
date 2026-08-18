using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x02000008 RID: 8
	internal class PrefixContainer
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002A6E File Offset: 0x00000C6E
		internal PrefixContainer(ICollection<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this._originalValues = values;
			this._sortedValues = this._originalValues.ToArrayWithoutNulls<string>();
			Array.Sort<string>(this._sortedValues, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002AAC File Offset: 0x00000CAC
		internal bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (prefix.Length == 0)
			{
				return this._sortedValues.Length > 0;
			}
			PrefixContainer.PrefixComparer comparer = new PrefixContainer.PrefixComparer(prefix);
			bool flag = Array.BinarySearch<string>(this._sortedValues, prefix, comparer) > -1;
			if (!flag)
			{
				flag = (Array.BinarySearch<string>(this._sortedValues, prefix + "[", comparer) > -1);
			}
			return flag;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B14 File Offset: 0x00000D14
		internal IDictionary<string, string> GetKeysFromPrefix(string prefix)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in this._originalValues)
			{
				if (text != null && text.Length != prefix.Length)
				{
					if (prefix.Length == 0)
					{
						PrefixContainer.GetKeyFromEmptyPrefix(text, dictionary);
					}
					else if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					{
						PrefixContainer.GetKeyFromNonEmptyPrefix(prefix, text, dictionary);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B9C File Offset: 0x00000D9C
		private static void GetKeyFromEmptyPrefix(string entry, IDictionary<string, string> results)
		{
			int num = entry.IndexOf('.');
			int num2 = entry.IndexOf('[');
			int num3 = -1;
			if (num == -1)
			{
				if (num2 != -1)
				{
					num3 = num2;
				}
			}
			else if (num2 == -1)
			{
				num3 = num;
			}
			else
			{
				num3 = Math.Min(num, num2);
			}
			string text = (num3 == -1) ? entry : entry.Substring(0, num3);
			results[text] = text;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private static void GetKeyFromNonEmptyPrefix(string prefix, string entry, IDictionary<string, string> results)
		{
			int num = prefix.Length + 1;
			char c = entry[prefix.Length];
			string key;
			string value;
			if (c != '.')
			{
				if (c != '[')
				{
					return;
				}
				int num2 = entry.IndexOf(']', num);
				if (num2 == -1)
				{
					return;
				}
				key = entry.Substring(num, num2 - num);
				value = entry.Substring(0, num2 + 1);
			}
			else
			{
				int num3 = entry.IndexOf('.', num);
				if (num3 == -1)
				{
					num3 = entry.Length;
				}
				key = entry.Substring(num, num3 - num);
				value = entry.Substring(0, num3);
			}
			if (!results.ContainsKey(key))
			{
				results.Add(key, value);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C90 File Offset: 0x00000E90
		internal static bool IsPrefixMatch(string prefix, string testString)
		{
			if (testString == null)
			{
				return false;
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (prefix.Length > testString.Length)
			{
				return false;
			}
			if (!testString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (testString.Length == prefix.Length)
			{
				return true;
			}
			char c = testString[prefix.Length];
			return c == '.' || c == '[';
		}

		// Token: 0x04000005 RID: 5
		private readonly ICollection<string> _originalValues;

		// Token: 0x04000006 RID: 6
		private readonly string[] _sortedValues;

		// Token: 0x02000009 RID: 9
		private class PrefixComparer : IComparer<string>
		{
			// Token: 0x06000035 RID: 53 RVA: 0x00002CF2 File Offset: 0x00000EF2
			public PrefixComparer(string prefix)
			{
				this._prefix = prefix;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002D04 File Offset: 0x00000F04
			public int Compare(string x, string y)
			{
				string testString = object.ReferenceEquals(x, this._prefix) ? y : x;
				if (PrefixContainer.IsPrefixMatch(this._prefix, testString))
				{
					return 0;
				}
				return StringComparer.OrdinalIgnoreCase.Compare(x, y);
			}

			// Token: 0x04000007 RID: 7
			private string _prefix;
		}
	}
}
