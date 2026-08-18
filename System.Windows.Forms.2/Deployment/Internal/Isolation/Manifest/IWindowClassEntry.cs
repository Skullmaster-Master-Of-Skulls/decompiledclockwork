using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B0 RID: 176
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BA")]
	[ComImport]
	internal interface IWindowClassEntry
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600029C RID: 668
		WindowClassEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600029D RID: 669
		string ClassName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600029E RID: 670
		string HostDll { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600029F RID: 671
		bool fVersioned { [SecurityCritical] get; }
	}
}
