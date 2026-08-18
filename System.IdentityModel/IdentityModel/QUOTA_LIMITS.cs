using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x0200005E RID: 94
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct QUOTA_LIMITS
	{
		// Token: 0x0400030D RID: 781
		internal IntPtr PagedPoolLimit;

		// Token: 0x0400030E RID: 782
		internal IntPtr NonPagedPoolLimit;

		// Token: 0x0400030F RID: 783
		internal IntPtr MinimumWorkingSetSize;

		// Token: 0x04000310 RID: 784
		internal IntPtr MaximumWorkingSetSize;

		// Token: 0x04000311 RID: 785
		internal IntPtr PagefileLimit;

		// Token: 0x04000312 RID: 786
		internal IntPtr TimeLimit;
	}
}
