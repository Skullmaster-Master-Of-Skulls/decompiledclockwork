using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C2 RID: 194
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CFA3F59F-334D-46bf-A5A5-5D11BB2D7EBC")]
	[ComImport]
	internal interface IDeploymentMetadataEntry
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002BC RID: 700
		DeploymentMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002BD RID: 701
		string DeploymentProviderCodebase { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002BE RID: 702
		string MinimumRequiredVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002BF RID: 703
		ushort MaximumAge { [SecurityCritical] get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002C0 RID: 704
		byte MaximumAge_Unit { [SecurityCritical] get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002C1 RID: 705
		uint DeploymentFlags { [SecurityCritical] get; }
	}
}
