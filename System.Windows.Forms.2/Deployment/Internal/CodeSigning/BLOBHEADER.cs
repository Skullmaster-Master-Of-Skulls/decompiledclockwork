using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000013 RID: 19
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct BLOBHEADER
	{
		// Token: 0x040000E1 RID: 225
		internal byte bType;

		// Token: 0x040000E2 RID: 226
		internal byte bVersion;

		// Token: 0x040000E3 RID: 227
		internal short reserved;

		// Token: 0x040000E4 RID: 228
		internal uint aiKeyAlg;
	}
}
