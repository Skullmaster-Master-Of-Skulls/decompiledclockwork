using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x020007A3 RID: 1955
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x06003C35 RID: 15413 RVA: 0x00101545 File Offset: 0x00100545
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x00101560 File Offset: 0x00100560
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

		// Token: 0x0400351F RID: 13599
		private CompareInfo m_compareInfo;

		// Token: 0x04003520 RID: 13600
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
