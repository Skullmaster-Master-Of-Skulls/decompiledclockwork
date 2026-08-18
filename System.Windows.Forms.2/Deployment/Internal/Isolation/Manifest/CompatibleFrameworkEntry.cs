using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E7 RID: 231
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworkEntry
	{
		// Token: 0x040003B1 RID: 945
		public uint index;

		// Token: 0x040003B2 RID: 946
		[MarshalAs(UnmanagedType.LPWStr)]
		public string TargetVersion;

		// Token: 0x040003B3 RID: 947
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Profile;

		// Token: 0x040003B4 RID: 948
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportedRuntime;
	}
}
