using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000020 RID: 32
	internal struct IDENTITY_ATTRIBUTE
	{
		// Token: 0x0400010D RID: 269
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Namespace;

		// Token: 0x0400010E RID: 270
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x0400010F RID: 271
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
