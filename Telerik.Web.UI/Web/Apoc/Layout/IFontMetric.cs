using System;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015EC RID: 5612
	internal interface IFontMetric
	{
		// Token: 0x1700431E RID: 17182
		// (get) Token: 0x0600DAA1 RID: 55969
		int Ascender { get; }

		// Token: 0x1700431F RID: 17183
		// (get) Token: 0x0600DAA2 RID: 55970
		int Descender { get; }

		// Token: 0x17004320 RID: 17184
		// (get) Token: 0x0600DAA3 RID: 55971
		int CapHeight { get; }

		// Token: 0x17004321 RID: 17185
		// (get) Token: 0x0600DAA4 RID: 55972
		int FirstChar { get; }

		// Token: 0x17004322 RID: 17186
		// (get) Token: 0x0600DAA5 RID: 55973
		int LastChar { get; }

		// Token: 0x17004323 RID: 17187
		// (get) Token: 0x0600DAA6 RID: 55974
		IFontDescriptor Descriptor { get; }

		// Token: 0x0600DAA7 RID: 55975
		int GetWidth(int charIndex);

		// Token: 0x17004324 RID: 17188
		// (get) Token: 0x0600DAA8 RID: 55976
		int[] Widths { get; }
	}
}
