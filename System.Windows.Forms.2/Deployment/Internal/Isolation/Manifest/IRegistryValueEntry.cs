using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D7 RID: 215
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("49e1fe8d-ebb8-4593-8c4e-3e14c845b142")]
	[ComImport]
	internal interface IRegistryValueEntry
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002FC RID: 764
		RegistryValueEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002FD RID: 765
		uint Flags { [SecurityCritical] get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002FE RID: 766
		uint OperationHint { [SecurityCritical] get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002FF RID: 767
		uint Type { [SecurityCritical] get; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000300 RID: 768
		string Value { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000301 RID: 769
		string BuildFilter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
