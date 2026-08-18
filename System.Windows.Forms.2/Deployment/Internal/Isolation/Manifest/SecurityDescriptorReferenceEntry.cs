using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000DE RID: 222
	[StructLayout(LayoutKind.Sequential)]
	internal class SecurityDescriptorReferenceEntry
	{
		// Token: 0x04000392 RID: 914
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04000393 RID: 915
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;
	}
}
