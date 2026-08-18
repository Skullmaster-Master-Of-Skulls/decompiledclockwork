using System;
using System.Collections.Generic;

namespace System.Web
{
	// Token: 0x02000005 RID: 5
	internal class PrefixContainer
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002619 File Offset: 0x00000819
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

		// Token: 0x06000019 RID: 25 RVA: 0x00002658 File Offset: 0x00000858
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

		// Token: 0x0600001A RID: 26 RVA: 0x000026C0 File Offset: 0x000008C0
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002748 File Offset: 0x00000948
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

		// Token: 0x0600001C RID: 28 RVA: 0x000027A0 File Offset: 0x000009A0
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

		// Token: 0x0600001D RID: 29 RVA: 0x0000283C File Offset: 0x00000A3C
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

		// Token: 0x04000003 RID: 3
		private readonly ICollection<string> _originalValues;

		// Token: 0x04000004 RID: 4
		private readonly string[] _sortedValues;

		// Token: 0x02000006 RID: 6
		private class PrefixComparer : IComparer<string>
		{
			// Token: 0x0600001E RID: 30 RVA: 0x0000289E File Offset: 0x00000A9E
			public PrefixComparer(string prefix)
			{
				this._prefix = prefix;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x000028B0 File Offset: 0x00000AB0
			public int Compare(string x, string y)
			{
				string testString = object.ReferenceEquals(x, this._prefix) ? y : x;
				if (PrefixContainer.IsPrefixMatch(this._prefix, testString))
				{
					return 0;
				}
				return StringComparer.OrdinalIgnoreCase.Compare(x, y);
			}

			// Token: 0x04000005 RID: 5
			private string _prefix;
		}
	}
}
