using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A2 RID: 162
	[StructLayout(LayoutKind.Sequential)]
	internal class ProgIdRedirectionEntry
	{
		// Token: 0x040002B3 RID: 691
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgId;

		// Token: 0x040002B4 RID: 692
		public Guid RedirectedGuid;
	}
}
