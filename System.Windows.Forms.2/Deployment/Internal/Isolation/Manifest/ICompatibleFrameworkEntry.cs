using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E9 RID: 233
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("C98BFE2A-62C9-40AD-ADCE-A9037BE2BE6C")]
	[ComImport]
	internal interface ICompatibleFrameworkEntry
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600032C RID: 812
		CompatibleFrameworkEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600032D RID: 813
		uint index { [SecurityCritical] get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600032E RID: 814
		string TargetVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600032F RID: 815
		string Profile { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000330 RID: 816
		string SupportedRuntime { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
