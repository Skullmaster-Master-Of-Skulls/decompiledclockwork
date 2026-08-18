using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004F4 RID: 1268
	internal class OrdinalCaseInsensitiveComparer : IComparer
	{
		// Token: 0x06003028 RID: 12328 RVA: 0x000D9C40 File Offset: 0x000D7E40
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04002885 RID: 10373
		internal static readonly OrdinalCaseInsensitiveComparer Default = new OrdinalCaseInsensitiveComparer();
	}
}
