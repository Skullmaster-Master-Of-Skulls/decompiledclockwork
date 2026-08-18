using System;
using System.Collections;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001632 RID: 5682
	public class GdiUnicodeRanges
	{
		// Token: 0x0600DCEC RID: 56556 RVA: 0x00304794 File Offset: 0x00302994
		public GdiUnicodeRanges(GdiDeviceContent dc)
		{
			this.LoadRanges(dc);
		}

		// Token: 0x170043AB RID: 17323
		// (get) Token: 0x0600DCED RID: 56557 RVA: 0x003047A3 File Offset: 0x003029A3
		public int Count
		{
			get
			{
				return this.unicodeRanges.Length;
			}
		}

		// Token: 0x0600DCEE RID: 56558 RVA: 0x003047B0 File Offset: 0x003029B0
		private void LoadRanges(GdiDeviceContent dc)
		{
			GlyphSet glyphSet = new GlyphSet();
			if (NativeMethods.GetFontUnicodeRanges(dc.Handle, glyphSet) == 0)
			{
				throw new Exception("Unable to retrieve unicode ranges.");
			}
			this.unicodeRanges = new UnicodeRange[glyphSet.cRanges];
			int i = 0;
			int num = 0;
			while (i < glyphSet.cRanges)
			{
				int num2 = (int)glyphSet.ranges[num++] + ((int)glyphSet.ranges[num++] << 8);
				int num3 = (int)glyphSet.ranges[num++] + ((int)glyphSet.ranges[num++] << 8);
				this.unicodeRanges[i] = new UnicodeRange(dc, num2, num2 + num3 - 1);
				i++;
			}
		}

		// Token: 0x0600DCEF RID: 56559 RVA: 0x00304854 File Offset: 0x00302A54
		internal UnicodeRange GetRange(char c)
		{
			int num = Array.BinarySearch(this.unicodeRanges, 0, this.unicodeRanges.Length, c, GdiUnicodeRanges.SearchComparer);
			if (num >= 0)
			{
				return this.unicodeRanges[num];
			}
			return null;
		}

		// Token: 0x0600DCF0 RID: 56560 RVA: 0x00304890 File Offset: 0x00302A90
		public int MapCharacter(char c)
		{
			UnicodeRange range = this.GetRange(c);
			if (range != null)
			{
				return range.MapCharacter(c);
			}
			return 0;
		}

		// Token: 0x04003E69 RID: 15977
		private static readonly IComparer SearchComparer = new UnicodeRangeComparer();

		// Token: 0x04003E6A RID: 15978
		private UnicodeRange[] unicodeRanges;
	}
}
