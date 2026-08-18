using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200058F RID: 1423
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600326E RID: 12910 RVA: 0x0011D685 File Offset: 0x0011C685
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x0011D6A0 File Offset: 0x0011C6A0
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

		// Token: 0x0400217E RID: 8574
		private CompareInfo m_compareInfo;

		// Token: 0x0400217F RID: 8575
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
