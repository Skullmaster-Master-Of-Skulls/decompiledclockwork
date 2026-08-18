using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x0200163A RID: 5690
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal class LogFont
	{
		// Token: 0x04003E7A RID: 15994
		public int lfHeight;

		// Token: 0x04003E7B RID: 15995
		public int lfWidth;

		// Token: 0x04003E7C RID: 15996
		public int lfEscapement;

		// Token: 0x04003E7D RID: 15997
		public int lfOrientation;

		// Token: 0x04003E7E RID: 15998
		public int lfWeight;

		// Token: 0x04003E7F RID: 15999
		public byte lfItalic;

		// Token: 0x04003E80 RID: 16000
		public byte lfUnderline;

		// Token: 0x04003E81 RID: 16001
		public byte lfStrikeOut;

		// Token: 0x04003E82 RID: 16002
		public byte lfCharSet;

		// Token: 0x04003E83 RID: 16003
		public byte lfOutPrecision;

		// Token: 0x04003E84 RID: 16004
		public byte lfClipPrecision;

		// Token: 0x04003E85 RID: 16005
		public byte lfQuality;

		// Token: 0x04003E86 RID: 16006
		public byte lfPitchAndFamily;

		// Token: 0x04003E87 RID: 16007
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string lfFaceName = string.Empty;
	}
}
