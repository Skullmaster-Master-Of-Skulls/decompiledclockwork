using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000334 RID: 820
	internal class TrailingSpaceComparer : IEqualityComparer<object>
	{
		// Token: 0x06001C6D RID: 7277 RVA: 0x0008B439 File Offset: 0x00089639
		private TrailingSpaceComparer()
		{
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0008B444 File Offset: 0x00089644
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
			return TrailingSpaceComparer._template.Equals(x, y);
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0008B480 File Offset: 0x00089680
		int IEqualityComparer<object>.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text != null)
			{
				return TrailingSpaceStringComparer.Instance.GetHashCode(text);
			}
			return TrailingSpaceComparer._template.GetHashCode(obj);
		}

		// Token: 0x040009CD RID: 2509
		internal static readonly TrailingSpaceComparer Instance = new TrailingSpaceComparer();

		// Token: 0x040009CE RID: 2510
		private static readonly IEqualityComparer<object> _template = EqualityComparer<object>.Default;
	}
}
