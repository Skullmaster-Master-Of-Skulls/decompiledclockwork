using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils
{
	// Token: 0x0200038F RID: 911
	internal class TrailingSpaceComparer : IEqualityComparer<object>
	{
		// Token: 0x06003282 RID: 12930 RVA: 0x00002050 File Offset: 0x00000250
		private TrailingSpaceComparer()
		{
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000C55C0 File Offset: 0x000C37C0
		bool IEqualityComparer<object>.Equals(object x, object y)
		{
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return TrailingSpaceStringComparer.Instance.Equals(text, text2);
				}
			}
			return TrailingSpaceComparer.s_template.Equals(x, y);
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000C55FC File Offset: 0x000C37FC
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text != null)
			{
				return TrailingSpaceStringComparer.Instance.GetHashCode(text);
			}
			return TrailingSpaceComparer.s_template.GetHashCode(obj);
		}

		// Token: 0x04001658 RID: 5720
		internal static readonly TrailingSpaceComparer Instance = new TrailingSpaceComparer();

		// Token: 0x04001659 RID: 5721
		private static readonly IEqualityComparer<object> s_template = EqualityComparer<object>.Default;
	}
}
