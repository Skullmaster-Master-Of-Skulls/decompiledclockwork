using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200008C RID: 140
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("397927f5-10f2-4ecb-bfe1-3c264212a193")]
	[ComImport]
	internal interface IMuiResourceMapEntry
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600023F RID: 575
		MuiResourceMapEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000240 RID: 576
		object ResourceTypeIdInt { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000241 RID: 577
		object ResourceTypeIdString { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
