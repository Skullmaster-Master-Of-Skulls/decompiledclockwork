using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B9 RID: 185
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("EBE5A1ED-FEBC-42c4-A9E1-E087C6E36635")]
	[ComImport]
	internal interface IPermissionSetEntry
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002AC RID: 684
		PermissionSetEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002AD RID: 685
		string Id { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002AE RID: 686
		string XmlSegment { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
