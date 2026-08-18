using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D1 RID: 209
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BC")]
	[ComImport]
	internal interface IEventMapEntry
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002F2 RID: 754
		EventMapEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002F3 RID: 755
		string MapName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002F4 RID: 756
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002F5 RID: 757
		uint Value { [SecurityCritical] get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002F6 RID: 758
		bool IsValueMap { [SecurityCritical] get; }
	}
}
