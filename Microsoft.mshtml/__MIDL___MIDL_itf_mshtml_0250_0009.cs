using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CA5 RID: 3237
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct __MIDL___MIDL_itf_mshtml_0250_0009
	{
		// Token: 0x040003D2 RID: 978
		public uint dwStyle;

		// Token: 0x040003D3 RID: 979
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public ushort[] szDescription;
	}
}
