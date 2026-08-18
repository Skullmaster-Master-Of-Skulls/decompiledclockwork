using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000054 RID: 84
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct SID_AND_ATTRIBUTES
	{
		// Token: 0x040002E2 RID: 738
		internal IntPtr Sid;

		// Token: 0x040002E3 RID: 739
		internal uint Attributes;

		// Token: 0x040002E4 RID: 740
		internal static readonly long SizeOf = (long)Marshal.SizeOf(typeof(SID_AND_ATTRIBUTES));
	}
}
