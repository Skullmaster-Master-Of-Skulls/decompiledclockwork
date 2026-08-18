using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A1 RID: 161
	internal struct NegotiationInfo
	{
		// Token: 0x04000499 RID: 1177
		internal IntPtr PackageInfo;

		// Token: 0x0400049A RID: 1178
		internal uint NegotiationState;

		// Token: 0x0400049B RID: 1179
		internal static readonly int Size = Marshal.SizeOf(typeof(NegotiationInfo));

		// Token: 0x0400049C RID: 1180
		internal static readonly int NegotiationStateOffset = (int)Marshal.OffsetOf(typeof(NegotiationInfo), "NegotiationState");
	}
}
