using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x0200077C RID: 1916
	internal class OrdinalCaseInsensitiveComparer : IComparer
	{
		// Token: 0x06003B43 RID: 15171 RVA: 0x000FC2B0 File Offset: 0x000FB2B0
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return string.CompareOrdinal(text.ToUpperInvariant(), text2.ToUpperInvariant());
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x040033E4 RID: 13284
		internal static readonly OrdinalCaseInsensitiveComparer Default = new OrdinalCaseInsensitiveComparer();
	}
}
