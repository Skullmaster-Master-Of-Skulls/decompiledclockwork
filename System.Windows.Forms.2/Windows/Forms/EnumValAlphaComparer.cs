using System;
using System.Collections;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x0200024A RID: 586
	internal class EnumValAlphaComparer : IComparer
	{
		// Token: 0x06002518 RID: 9496 RVA: 0x000AD7CD File Offset: 0x000AB9CD
		internal EnumValAlphaComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000AD7E5 File Offset: 0x000AB9E5
		public int Compare(object a, object b)
		{
			return this.m_compareInfo.Compare(a.ToString(), b.ToString());
		}

		// Token: 0x04000F6D RID: 3949
		private CompareInfo m_compareInfo;

		// Token: 0x04000F6E RID: 3950
		internal static readonly EnumValAlphaComparer Default = new EnumValAlphaComparer();
	}
}
