using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000052 RID: 82
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct CREDUI_INFO
	{
		// Token: 0x040002D2 RID: 722
		public int cbSize;

		// Token: 0x040002D3 RID: 723
		public IntPtr hwndParent;

		// Token: 0x040002D4 RID: 724
		public string pszMessageText;

		// Token: 0x040002D5 RID: 725
		public string pszCaptionText;

		// Token: 0x040002D6 RID: 726
		public IntPtr hbmBanner;
	}
}
