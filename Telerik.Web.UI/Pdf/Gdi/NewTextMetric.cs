using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200163B RID: 5691
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct NewTextMetric
	{
		// Token: 0x04003E88 RID: 16008
		public long tmHeight;

		// Token: 0x04003E89 RID: 16009
		public long tmAscent;

		// Token: 0x04003E8A RID: 16010
		public long tmDescent;

		// Token: 0x04003E8B RID: 16011
		public long tmInternalLeading;

		// Token: 0x04003E8C RID: 16012
		public long tmExternalLeading;

		// Token: 0x04003E8D RID: 16013
		public long tmAvecharWidth;

		// Token: 0x04003E8E RID: 16014
		public long tmMaxcharWidth;

		// Token: 0x04003E8F RID: 16015
		public long tmWeight;

		// Token: 0x04003E90 RID: 16016
		public long tmOverhang;

		// Token: 0x04003E91 RID: 16017
		public long tmDigitizedAspectX;

		// Token: 0x04003E92 RID: 16018
		public long tmDigitizedAspectY;

		// Token: 0x04003E93 RID: 16019
		public char tmFirstchar;

		// Token: 0x04003E94 RID: 16020
		public char tmLastchar;

		// Token: 0x04003E95 RID: 16021
		public char tmDefaultchar;

		// Token: 0x04003E96 RID: 16022
		public char tmBreakchar;

		// Token: 0x04003E97 RID: 16023
		public byte tmItalic;

		// Token: 0x04003E98 RID: 16024
		public byte tmUnderlined;

		// Token: 0x04003E99 RID: 16025
		public byte tmStruckOut;

		// Token: 0x04003E9A RID: 16026
		public byte tmPitchAndFamily;

		// Token: 0x04003E9B RID: 16027
		public byte tmcharSet;

		// Token: 0x04003E9C RID: 16028
		public ulong ntmFlags;

		// Token: 0x04003E9D RID: 16029
		public int ntmSizeEM;

		// Token: 0x04003E9E RID: 16030
		public int ntmCellHeight;

		// Token: 0x04003E9F RID: 16031
		public int ntmAvgWidth;
	}
}
