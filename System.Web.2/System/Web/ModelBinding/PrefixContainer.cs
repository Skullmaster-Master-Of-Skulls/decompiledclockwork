using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding
{
	// Token: 0x02000629 RID: 1577
	internal sealed class PrefixContainer
	{
		// Token: 0x06004ED5 RID: 20181 RVA: 0x001124C4 File Offset: 0x001106C4
		internal PrefixContainer(IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this._sortedValues = (from val in values
			where val != null
			select val).ToArray<string>();
			Array.Sort<string>(this._sortedValues, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00112525 File Offset: 0x00110725
		internal bool ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			if (prefix.Length == 0)
			{
				return this._sortedValues.Length != 0;
			}
			return Array.BinarySearch<string>(this._sortedValues, prefix, new PrefixContainer.PrefixComparer(prefix)) > -1;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00112560 File Offset: 0x00110760
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

		// Token: 0x04002A52 RID: 10834
		private readonly string[] _sortedValues;

		// Token: 0x02000A15 RID: 2581
		private sealed class PrefixComparer : IComparer<string>
		{
			// Token: 0x06006DEB RID: 28139 RVA: 0x00188F03 File Offset: 0x00187103
			public PrefixComparer(string prefix)
			{
				this._prefix = prefix;
			}

			// Token: 0x06006DEC RID: 28140 RVA: 0x00188F14 File Offset: 0x00187114
			public int Compare(string x, string y)
			{
				string testString = (x == this._prefix) ? y : x;
				if (PrefixContainer.IsPrefixMatch(this._prefix, testString))
				{
					return 0;
				}
				return StringComparer.OrdinalIgnoreCase.Compare(x, y);
			}

			// Token: 0x04003A95 RID: 14997
			private string _prefix;
		}
	}
}
