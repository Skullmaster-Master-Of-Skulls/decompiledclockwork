using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x0200009F RID: 159
	internal struct SecurityPackageInfo
	{
		// Token: 0x0400048E RID: 1166
		internal int Capabilities;

		// Token: 0x0400048F RID: 1167
		internal short Version;

		// Token: 0x04000490 RID: 1168
		internal short RPCID;

		// Token: 0x04000491 RID: 1169
		internal int MaxToken;

		// Token: 0x04000492 RID: 1170
		internal IntPtr Name;

		// Token: 0x04000493 RID: 1171
		internal IntPtr Comment;

		// Token: 0x04000494 RID: 1172
		internal static readonly int Size = Marshal.SizeOf(typeof(SecurityPackageInfo));

		// Token: 0x04000495 RID: 1173
		internal static readonly int NameOffest = (int)Marshal.OffsetOf(typeof(SecurityPackageInfo), "Name");
	}
}
