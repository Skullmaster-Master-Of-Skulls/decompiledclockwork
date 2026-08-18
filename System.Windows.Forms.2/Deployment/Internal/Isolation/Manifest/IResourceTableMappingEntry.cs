using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B3 RID: 179
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("70A4ECEE-B195-4c59-85BF-44B6ACA83F07")]
	[ComImport]
	internal interface IResourceTableMappingEntry
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002A1 RID: 673
		ResourceTableMappingEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A2 RID: 674
		string id { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002A3 RID: 675
		string FinalStringMapped { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
