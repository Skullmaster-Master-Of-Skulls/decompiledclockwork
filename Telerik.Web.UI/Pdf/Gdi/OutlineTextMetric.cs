using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200163C RID: 5692
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct OutlineTextMetric
	{
		// Token: 0x04003EA0 RID: 16032
		public int otmSize;

		// Token: 0x04003EA1 RID: 16033
		public TextMetric otmTextMetrics;

		// Token: 0x04003EA2 RID: 16034
		public byte otmFiller;

		// Token: 0x04003EA3 RID: 16035
		public Panose otmPanoseNumber;

		// Token: 0x04003EA4 RID: 16036
		public int otmfsSelection;

		// Token: 0x04003EA5 RID: 16037
		public int otmfsType;

		// Token: 0x04003EA6 RID: 16038
		public int otmsCharSlopeRise;

		// Token: 0x04003EA7 RID: 16039
		public int otmsCharSlopeRun;

		// Token: 0x04003EA8 RID: 16040
		public int otmItalicAngle;

		// Token: 0x04003EA9 RID: 16041
		public int otmEMSquare;

		// Token: 0x04003EAA RID: 16042
		public int otmAscent;

		// Token: 0x04003EAB RID: 16043
		public int otmDescent;

		// Token: 0x04003EAC RID: 16044
		public int otmLineGap;

		// Token: 0x04003EAD RID: 16045
		public int otmsCapEmHeight;

		// Token: 0x04003EAE RID: 16046
		public int otmsXHeight;

		// Token: 0x04003EAF RID: 16047
		public Rect otmrcFontBox;

		// Token: 0x04003EB0 RID: 16048
		public int otmMacAscent;

		// Token: 0x04003EB1 RID: 16049
		public int otmMacDescent;

		// Token: 0x04003EB2 RID: 16050
		public int otmMacLineGap;

		// Token: 0x04003EB3 RID: 16051
		public int otmusMinimumPPEM;

		// Token: 0x04003EB4 RID: 16052
		public Point otmptSubscriptSize;

		// Token: 0x04003EB5 RID: 16053
		public Point otmptSubscriptOffset;

		// Token: 0x04003EB6 RID: 16054
		public Point otmptSuperscriptSize;

		// Token: 0x04003EB7 RID: 16055
		public Point otmptSuperscriptOffset;

		// Token: 0x04003EB8 RID: 16056
		public int otmsStrikeoutSize;

		// Token: 0x04003EB9 RID: 16057
		public int otmsStrikeoutPosition;

		// Token: 0x04003EBA RID: 16058
		public int otmsUnderscoreSize;

		// Token: 0x04003EBB RID: 16059
		public int otmsUnderscorePosition;

		// Token: 0x04003EBC RID: 16060
		public int otmpFamilyName;

		// Token: 0x04003EBD RID: 16061
		public int otmpFaceName;

		// Token: 0x04003EBE RID: 16062
		public int otmpStyleName;

		// Token: 0x04003EBF RID: 16063
		public int otmpFullName;

		// Token: 0x04003EC0 RID: 16064
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public char[] nameBuffer;
	}
}
