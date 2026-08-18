using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000214 RID: 532
	internal struct NegotiationInfo
	{
		// Token: 0x040015BA RID: 5562
		internal IntPtr PackageInfo;

		// Token: 0x040015BB RID: 5563
		internal uint NegotiationState;

		// Token: 0x040015BC RID: 5564
		internal static readonly int Size = Marshal.SizeOf(typeof(NegotiationInfo));

		// Token: 0x040015BD RID: 5565
		internal static readonly int NegotiationStateOffest = (int)Marshal.OffsetOf(typeof(NegotiationInfo), "NegotiationState");
	}
}
