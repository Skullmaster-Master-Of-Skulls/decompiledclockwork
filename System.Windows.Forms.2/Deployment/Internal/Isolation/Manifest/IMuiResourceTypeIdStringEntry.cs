using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000086 RID: 134
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("11df5cad-c183-479b-9a44-3842b71639ce")]
	[ComImport]
	internal interface IMuiResourceTypeIdStringEntry
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000231 RID: 561
		MuiResourceTypeIdStringEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000232 RID: 562
		object StringIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000233 RID: 563
		object IntegerIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
