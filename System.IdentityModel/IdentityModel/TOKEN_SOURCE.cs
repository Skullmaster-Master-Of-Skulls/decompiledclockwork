using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x0200005C RID: 92
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct TOKEN_SOURCE
	{
		// Token: 0x040002FE RID: 766
		private const int TOKEN_SOURCE_LENGTH = 8;

		// Token: 0x040002FF RID: 767
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal char[] Name;

		// Token: 0x04000300 RID: 768
		internal LUID SourceIdentifier;
	}
}
