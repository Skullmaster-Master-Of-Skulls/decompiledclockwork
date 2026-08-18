using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B6 RID: 182
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1583EFE9-832F-4d08-B041-CAC5ACEDB948")]
	[ComImport]
	internal interface IEntryPointEntry
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002A5 RID: 677
		EntryPointEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002A6 RID: 678
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002A7 RID: 679
		string CommandLine_File { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002A8 RID: 680
		string CommandLine_Parameters { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002A9 RID: 681
		IReferenceIdentity Identity { [SecurityCritical] get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002AA RID: 682
		uint Flags { [SecurityCritical] get; }
	}
}
