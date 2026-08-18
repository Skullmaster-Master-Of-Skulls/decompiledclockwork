using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000549 RID: 1353
	internal struct SecurityPackageInfo
	{
		// Token: 0x0400282E RID: 10286
		internal int Capabilities;

		// Token: 0x0400282F RID: 10287
		internal short Version;

		// Token: 0x04002830 RID: 10288
		internal short RPCID;

		// Token: 0x04002831 RID: 10289
		internal int MaxToken;

		// Token: 0x04002832 RID: 10290
		internal IntPtr Name;

		// Token: 0x04002833 RID: 10291
		internal IntPtr Comment;

		// Token: 0x04002834 RID: 10292
		internal static readonly int Size = Marshal.SizeOf(typeof(SecurityPackageInfo));

		// Token: 0x04002835 RID: 10293
		internal static readonly int NameOffest = (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Name");
	}
}
