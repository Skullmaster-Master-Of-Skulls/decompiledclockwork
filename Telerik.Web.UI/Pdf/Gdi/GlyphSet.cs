using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001638 RID: 5688
	[StructLayout(LayoutKind.Sequential)]
	internal class GlyphSet
	{
		// Token: 0x04003E72 RID: 15986
		public int cbThis;

		// Token: 0x04003E73 RID: 15987
		public int flAccel;

		// Token: 0x04003E74 RID: 15988
		public int cGlyphsSupported;

		// Token: 0x04003E75 RID: 15989
		public int cRanges;

		// Token: 0x04003E76 RID: 15990
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20000)]
		public byte[] ranges;
	}
}
