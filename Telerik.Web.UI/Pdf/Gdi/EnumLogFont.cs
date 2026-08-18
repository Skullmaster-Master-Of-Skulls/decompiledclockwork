using System;
using System.Runtime.InteropServices;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001637 RID: 5687
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct EnumLogFont
	{
		// Token: 0x04003E6F RID: 15983
		public LogFont elfLogFont;

		// Token: 0x04003E70 RID: 15984
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public char[] elfFullName;

		// Token: 0x04003E71 RID: 15985
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public char[] elfStyle;
	}
}
