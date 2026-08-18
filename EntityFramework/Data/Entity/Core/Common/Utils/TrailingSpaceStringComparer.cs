using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000335 RID: 821
	internal class TrailingSpaceStringComparer : IEqualityComparer<string>
	{
		// Token: 0x06001C71 RID: 7281 RVA: 0x0008B4C4 File Offset: 0x000896C4
		private TrailingSpaceStringComparer()
		{
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0008B4CC File Offset: 0x000896CC
		public bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(TrailingSpaceStringComparer.NormalizeString(x), TrailingSpaceStringComparer.NormalizeString(y));
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0008B4E4 File Offset: 0x000896E4
		public int GetHashCode(string obj)
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(TrailingSpaceStringComparer.NormalizeString(obj));
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0008B4F8 File Offset: 0x000896F8
		internal static string NormalizeString(string value)
		{
			if (value == null || !value.EndsWith(" ", StringComparison.Ordinal))
			{
				return value;
			}
			return value.TrimEnd(new char[]
			{
				' '
			});
		}

		// Token: 0x040009CF RID: 2511
		internal static readonly TrailingSpaceStringComparer Instance = new TrailingSpaceStringComparer();
	}
}
