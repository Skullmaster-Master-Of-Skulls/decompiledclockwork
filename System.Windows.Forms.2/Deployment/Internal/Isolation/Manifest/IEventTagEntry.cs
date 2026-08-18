using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D4 RID: 212
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BD")]
	[ComImport]
	internal interface IEventTagEntry
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002F8 RID: 760
		EventTagEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002F9 RID: 761
		string TagData { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002FA RID: 762
		uint EventID { [SecurityCritical] get; }
	}
}
