using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E3 RID: 227
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8CD3FC85-AFD3-477a-8FD5-146C291195BB")]
	[ComImport]
	internal interface ICounterSetEntry
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600031B RID: 795
		CounterSetEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600031C RID: 796
		Guid CounterSetGuid { [SecurityCritical] get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600031D RID: 797
		Guid ProviderGuid { [SecurityCritical] get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600031E RID: 798
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600031F RID: 799
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000320 RID: 800
		bool InstanceType { [SecurityCritical] get; }
	}
}
