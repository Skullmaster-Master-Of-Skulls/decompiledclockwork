using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04000184 RID: 388
		private CompareInfo m_compareInfo;

		// Token: 0x04000185 RID: 389
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
