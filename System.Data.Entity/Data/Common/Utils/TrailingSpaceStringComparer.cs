using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils
{
	// Token: 0x02000390 RID: 912
	internal class TrailingSpaceStringComparer : IEqualityComparer<string>
	{
		// Token: 0x06003286 RID: 12934 RVA: 0x00002050 File Offset: 0x00000250
		private TrailingSpaceStringComparer()
		{
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000C5640 File Offset: 0x000C3840
		public bool Equals(string x, string y)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(TrailingSpaceStringComparer.NormalizeString(x), TrailingSpaceStringComparer.NormalizeString(y));
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000C5658 File Offset: 0x000C3858
		public int GetHashCode(string obj)
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(TrailingSpaceStringComparer.NormalizeString(obj));
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000C566A File Offset: 0x000C386A
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

		// Token: 0x0400165A RID: 5722
		internal static readonly TrailingSpaceStringComparer Instance = new TrailingSpaceStringComparer();
	}
}
