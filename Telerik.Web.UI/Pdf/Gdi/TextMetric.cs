using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001640 RID: 5696
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct TextMetric
	{
		// Token: 0x04003ED1 RID: 16081
		public int tmHeight;

		// Token: 0x04003ED2 RID: 16082
		public int tmAscent;

		// Token: 0x04003ED3 RID: 16083
		public int tmDescent;

		// Token: 0x04003ED4 RID: 16084
		public int tmInternalLeading;

		// Token: 0x04003ED5 RID: 16085
		public int tmExternalLeading;

		// Token: 0x04003ED6 RID: 16086
		public int tmAveCharWidth;

		// Token: 0x04003ED7 RID: 16087
		public int tmMaxCharWidth;

		// Token: 0x04003ED8 RID: 16088
		public int tmWeight;

		// Token: 0x04003ED9 RID: 16089
		public int tmOverhang;

		// Token: 0x04003EDA RID: 16090
		public int tmDigitizedAspectX;

		// Token: 0x04003EDB RID: 16091
		public int tmDigitizedAspectY;

		// Token: 0x04003EDC RID: 16092
		public char tmFirschar;

		// Token: 0x04003EDD RID: 16093
		public char tmLaschar;

		// Token: 0x04003EDE RID: 16094
		public char tmDefaulchar;

		// Token: 0x04003EDF RID: 16095
		public char tmBreakChar;

		// Token: 0x04003EE0 RID: 16096
		public byte tmItalic;

		// Token: 0x04003EE1 RID: 16097
		public byte tmUnderlined;

		// Token: 0x04003EE2 RID: 16098
		public byte tmStruckOut;

		// Token: 0x04003EE3 RID: 16099
		public byte tmPitchAndFamily;

		// Token: 0x04003EE4 RID: 16100
		public byte tmCharSet;
	}
}
