using System;
using System.Collections;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001642 RID: 5698
	internal class UnicodeRangeComparer : IComparer
	{
		// Token: 0x0600DD0F RID: 56591 RVA: 0x00304A40 File Offset: 0x00302C40
		public int Compare(object x, object y)
		{
			UnicodeRange unicodeRange = (UnicodeRange)x;
			char c = (char)y;
			if (unicodeRange.End < (int)c)
			{
				return -1;
			}
			if (unicodeRange.Start > (int)c)
			{
				return 1;
			}
			return 0;
		}
	}
}
