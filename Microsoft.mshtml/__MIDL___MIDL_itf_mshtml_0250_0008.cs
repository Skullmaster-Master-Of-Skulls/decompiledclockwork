using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CA4 RID: 3236
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct __MIDL___MIDL_itf_mshtml_0250_0008
	{
		// Token: 0x040003D0 RID: 976
		public uint dwStyle;

		// Token: 0x040003D1 RID: 977
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public byte[] szDescription;
	}
}
