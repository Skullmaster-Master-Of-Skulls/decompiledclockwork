using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000DD RID: 221
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9f27c750-7dfb-46a1-a673-52e53e2337a9")]
	[ComImport]
	internal interface IDirectoryEntry
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000311 RID: 785
		DirectoryEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000312 RID: 786
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000313 RID: 787
		uint Protection { [SecurityCritical] get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000314 RID: 788
		string BuildFilter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000315 RID: 789
		object SecurityDescriptor { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
