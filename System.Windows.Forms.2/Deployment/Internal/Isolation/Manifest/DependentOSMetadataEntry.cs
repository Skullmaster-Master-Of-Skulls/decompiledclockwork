using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C3 RID: 195
	[StructLayout(LayoutKind.Sequential)]
	internal class DependentOSMetadataEntry
	{
		// Token: 0x0400030E RID: 782
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x0400030F RID: 783
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x04000310 RID: 784
		public ushort MajorVersion;

		// Token: 0x04000311 RID: 785
		public ushort MinorVersion;

		// Token: 0x04000312 RID: 786
		public ushort BuildNumber;

		// Token: 0x04000313 RID: 787
		public byte ServicePackMajor;

		// Token: 0x04000314 RID: 788
		public byte ServicePackMinor;
	}
}
