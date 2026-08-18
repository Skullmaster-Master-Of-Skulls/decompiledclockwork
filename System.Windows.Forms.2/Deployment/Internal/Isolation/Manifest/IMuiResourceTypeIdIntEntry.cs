using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000089 RID: 137
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("55b2dec1-d0f6-4bf4-91b1-30f73ad8e4df")]
	[ComImport]
	internal interface IMuiResourceTypeIdIntEntry
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000238 RID: 568
		MuiResourceTypeIdIntEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000239 RID: 569
		object StringIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023A RID: 570
		object IntegerIds { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}
