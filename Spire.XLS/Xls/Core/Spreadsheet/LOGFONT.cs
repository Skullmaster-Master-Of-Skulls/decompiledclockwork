using System;
using System.Runtime.InteropServices;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000610 RID: 1552
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class LOGFONT
	{
		// Token: 0x04002D4F RID: 11599
		public int lfHeight;

		// Token: 0x04002D50 RID: 11600
		public int lfWidth;

		// Token: 0x04002D51 RID: 11601
		public int lfEscapement;

		// Token: 0x04002D52 RID: 11602
		public int lfOrientation;

		// Token: 0x04002D53 RID: 11603
		public int lfWeight;

		// Token: 0x04002D54 RID: 11604
		public byte lfItalic;

		// Token: 0x04002D55 RID: 11605
		public byte lfUnderline;

		// Token: 0x04002D56 RID: 11606
		public byte lfStrikeOut;

		// Token: 0x04002D57 RID: 11607
		public byte lfCharSet;

		// Token: 0x04002D58 RID: 11608
		public byte lfOutPrecision;

		// Token: 0x04002D59 RID: 11609
		public byte lfClipPrecision;

		// Token: 0x04002D5A RID: 11610
		public byte lfQuality;

		// Token: 0x04002D5B RID: 11611
		public byte lfPitchAndFamily;

		// Token: 0x04002D5C RID: 11612
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string lfFaceName;
	}
}
