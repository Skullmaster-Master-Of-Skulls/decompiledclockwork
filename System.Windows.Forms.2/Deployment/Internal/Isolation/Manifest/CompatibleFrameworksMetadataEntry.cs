using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C6 RID: 198
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworksMetadataEntry
	{
		// Token: 0x0400031D RID: 797
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;
	}
}
