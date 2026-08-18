using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003466 File Offset: 0x00001666
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003480 File Offset: 0x00001680
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

		// Token: 0x04000069 RID: 105
		private CompareInfo m_compareInfo;

		// Token: 0x0400006A RID: 106
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
