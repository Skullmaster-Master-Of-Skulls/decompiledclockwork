using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000216 RID: 534
	internal struct SecurityPackageInfo
	{
		// Token: 0x040015C3 RID: 5571
		internal int Capabilities;

		// Token: 0x040015C4 RID: 5572
		internal short Version;

		// Token: 0x040015C5 RID: 5573
		internal short RPCID;

		// Token: 0x040015C6 RID: 5574
		internal int MaxToken;

		// Token: 0x040015C7 RID: 5575
		internal IntPtr Name;

		// Token: 0x040015C8 RID: 5576
		internal IntPtr Comment;

		// Token: 0x040015C9 RID: 5577
		internal static readonly int Size = Marshal.SizeOf(typeof(SecurityPackageInfo));

		// Token: 0x040015CA RID: 5578
		internal static readonly int NameOffest = (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Name");
	}
}
