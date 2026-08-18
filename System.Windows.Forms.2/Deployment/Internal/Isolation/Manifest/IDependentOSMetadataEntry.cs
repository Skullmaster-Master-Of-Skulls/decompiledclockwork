using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C5 RID: 197
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CF168CF4-4E8F-4d92-9D2A-60E5CA21CF85")]
	[ComImport]
	internal interface IDependentOSMetadataEntry
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002C3 RID: 707
		DependentOSMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002C4 RID: 708
		string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C5 RID: 709
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C6 RID: 710
		ushort MajorVersion { [SecurityCritical] get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002C7 RID: 711
		ushort MinorVersion { [SecurityCritical] get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002C8 RID: 712
		ushort BuildNumber { [SecurityCritical] get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002C9 RID: 713
		byte ServicePackMajor { [SecurityCritical] get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002CA RID: 714
		byte ServicePackMinor { [SecurityCritical] get; }
	}
}
