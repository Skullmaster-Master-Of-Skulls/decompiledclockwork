using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000547 RID: 1351
	internal struct NegotiationInfo
	{
		// Token: 0x04002825 RID: 10277
		internal IntPtr PackageInfo;

		// Token: 0x04002826 RID: 10278
		internal uint NegotiationState;

		// Token: 0x04002827 RID: 10279
		internal static readonly int Size = Marshal.SizeOf(typeof(NegotiationInfo));

		// Token: 0x04002828 RID: 10280
		internal static readonly int NegotiationStateOffest = (int)Marshal.OffsetOf(typeof(NegotiationInfo), "NegotiationState");
	}
}
