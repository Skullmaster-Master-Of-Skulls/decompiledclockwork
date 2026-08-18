using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E0 RID: 224
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("a75b74e9-2c00-4ebb-b3f9-62a670aaa07e")]
	[ComImport]
	internal interface ISecurityDescriptorReferenceEntry
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000317 RID: 791
		SecurityDescriptorReferenceEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000318 RID: 792
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000319 RID: 793
		string BuildFilter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
