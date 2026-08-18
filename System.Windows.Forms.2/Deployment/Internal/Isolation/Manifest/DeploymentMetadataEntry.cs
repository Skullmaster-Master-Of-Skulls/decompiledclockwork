using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Sequential)]
	internal class DeploymentMetadataEntry
	{
		// Token: 0x04000303 RID: 771
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DeploymentProviderCodebase;

		// Token: 0x04000304 RID: 772
		[MarshalAs(UnmanagedType.LPWStr)]
		public string MinimumRequiredVersion;

		// Token: 0x04000305 RID: 773
		public ushort MaximumAge;

		// Token: 0x04000306 RID: 774
		public byte MaximumAge_Unit;

		// Token: 0x04000307 RID: 775
		public uint DeploymentFlags;
	}
}
